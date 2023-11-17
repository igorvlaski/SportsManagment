using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using SportsManagment.Shared.DTOs;
using MudBlazor;
using SportsManagment.Shared.Domain;

namespace SportsManagment.Blazor.Client.Pages.Players;

public partial class UpdateAPlayer
{
    [Parameter]
    public Guid PlayerId { get; set; }
    [Inject] HttpClient Http { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    private UpdatePlayerDTO updatePlayer;
    private DateTime? tempBirthDate = DateTime.MinValue;
    private string? errorMessage;
    protected override async Task OnInitializedAsync()
    {
        try
        {
            updatePlayer = await Http.GetFromJsonAsync<UpdatePlayerDTO>($"Player/{PlayerId}");
            tempBirthDate = updatePlayer.DateOfBirth.ToDateTime(TimeOnly.MinValue);
        }
        catch (Exception ex)
        {
            errorMessage = $"Error fetching player data: {ex.Message}";
            Snackbar.Add(errorMessage, Severity.Error);
        }
    }

    private void GoToPlayerDetails(Guid playerId)
    {
        NavigationManager.NavigateTo($"/player/{playerId}");
    }
    private void GoToPlayers(Guid playerId)
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
                errorMessage = await response.Content.ReadAsStringAsync();
                Snackbar.Add(errorMessage, Severity.Error);
            }
        }
        catch (Exception ex)
        {
            errorMessage = $"Error editing player: {ex.Message}";
            Snackbar.Add(errorMessage, Severity.Error);
        }
    }
}