using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportsManagment.Shared.DTOs;

namespace SportsManagment.Blazor.Client.Pages.Players;

public partial class CreateAPlayer
{
    [Inject] HttpClient Http { get; set; }
    [Inject] NavigationManager Navigation { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    private CreatePlayerDTO player = new();
    private string? errorMessage;
    private DateTime? tempBirthDate = new DateTime(1988,5,6);

    private async Task HandleValidSubmit()
    {
        try
        {
            Snackbar.Add("Creating Player", Severity.Info);
            player.DateOfBirth = DateOnly.FromDateTime(tempBirthDate.Value);
            var response = await Http.PostAsJsonAsync("/Player", player);
            if (response.IsSuccessStatusCode)
            {
                // Optionally navigate to a success page or player list
                Snackbar.Add("Player successfuly created.", Severity.Success);
                Navigation.NavigateTo("/players");
            }
            else
            {
                errorMessage = "Failed to create player.";
                Snackbar.Add(errorMessage, Severity.Error);
            }
        }
        catch (Exception ex)
        {
            errorMessage = $"Error creating player: {ex.Message}";
            Snackbar.Add(errorMessage, Severity.Error);
        }
    }

    public void ResetForm()
    {
        player = new CreatePlayerDTO();
        errorMessage = string.Empty;
    }

}