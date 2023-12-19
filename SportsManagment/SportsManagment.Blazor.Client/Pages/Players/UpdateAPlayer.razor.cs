using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using SportsManagment.Shared.DTOs;
using MudBlazor;

namespace SportsManagment.Blazor.Client.Pages.Players;

public partial class UpdateAPlayer
{
    [Parameter] public Guid PlayerId { get; set; }
    [Inject] HttpClient Http { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    private UpdatePlayerDTO updatePlayer;
    private DateTime? tempBirthDate = DateTime.MinValue;
    protected override async Task OnInitializedAsync()
    {
        try
        {
            updatePlayer = await Http.GetFromJsonAsync<UpdatePlayerDTO>($"Player/{PlayerId}");
            tempBirthDate = updatePlayer.DateOfBirth.ToDateTime(TimeOnly.MinValue);
        }
        catch (Exception)
        {
            Snackbar.Add("Napaka pri pridobivanju podatkov!", Severity.Error);
        }
    }

    private void GoToPlayerDetails(Guid playerId)
    {
        NavigationManager.NavigateTo($"/player/{playerId}");
    }
    private void GoToPlayers()
    {
        NavigationManager.NavigateTo($"/players");
    }

    private async Task HandleValidSubmit()
    {
        try
        {
            updatePlayer.DateOfBirth = DateOnly.FromDateTime(tempBirthDate.Value);
            var response = await Http.PutAsJsonAsync($"Player/{PlayerId}", updatePlayer);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Igralec uspešno posodobljen.", Severity.Success);
                GoToPlayerDetails(PlayerId);
            }
            else
            {

                Snackbar.Add("Igralec ni bil posodobljen, poskusite kasneje.", Severity.Error);
            }
        }
        catch (Exception)
        {
            Snackbar.Add("Napaka pri posodabljanju igralca!", Severity.Error);
        }
    }
}