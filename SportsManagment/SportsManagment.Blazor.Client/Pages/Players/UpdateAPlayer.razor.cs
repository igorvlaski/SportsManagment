using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using SportsManagment.Shared.DTOs;
using SportsManagment.Shared.Domain;
using MudBlazor;

namespace SportsManagment.Blazor.Client.Pages.Players;

public partial class UpdateAPlayer
{
    [Parameter]
    public Guid PlayerId { get; set; }
    [Inject] HttpClient Http { get; set; }
    [Inject] NavigationManager Navigation { get; set; }
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

    private async Task HandleValidSubmit()
    {
        try
        {
            updatePlayer.DateOfBirth = DateOnly.FromDateTime(tempBirthDate.Value);
            var response = await Http.PutAsJsonAsync($"Player/{PlayerId}", updatePlayer);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Player successfully updated.", Severity.Success);
                Navigation.NavigateTo("/players");
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