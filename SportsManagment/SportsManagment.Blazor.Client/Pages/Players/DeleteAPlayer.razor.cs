using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace SportsManagment.Blazor.Client.Pages.Players;

public partial class DeleteAPlayer
{
    private string? deletePlayerId;
    private string? successMessage;
    private string? errorMessage;
    private async Task HandleValidSubmit()
    {
        try
        {
            var response = await Http.DeleteAsync($"Player/{deletePlayerId}");

            if (response.IsSuccessStatusCode)
            {
                Navigation.NavigateTo("/players");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                errorMessage = "This player is not on the roster.";
            }
            else
            {
                errorMessage = "Failed to delete player. Please try again later.";
            }
        }
        catch (Exception ex)
        {
            errorMessage = $"Error deleting player: {ex.Message}";
        }
    }
}