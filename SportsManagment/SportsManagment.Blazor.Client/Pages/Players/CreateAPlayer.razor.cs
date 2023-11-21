using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportsManagment.Shared.DTOs;

namespace SportsManagment.Blazor.Client.Pages.Players;

public partial class CreateAPlayer
{
    [Inject] HttpClient Http { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    private CreatePlayerDTO player = new();
    private string? errorMessage;
    private DateTime? tempBirthDate = new DateTime(1988,5,6);

    private async Task HandleValidSubmit()
    {
            try
            {
                Snackbar.Add("Dodajam igralca!", Severity.Info);
                player.DateOfBirth = DateOnly.FromDateTime(tempBirthDate.Value);
                var response = await Http.PostAsJsonAsync("/Player", player);
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Igralec uspešno dodan", Severity.Success);
                    NavigationManager.NavigateTo("/players");
                }
                else
                {
                    errorMessage = "Igralec ni bil dodan.";
                    Snackbar.Add(errorMessage, Severity.Error);
                }
            }
            catch (Exception)
            {
                errorMessage = $"Napaka pri dodajanju igralca. Poizkusite kasneje.";
                Snackbar.Add(errorMessage, Severity.Error);
            }
    }

    public void ResetForm()
    {
        player = new CreatePlayerDTO();
        errorMessage = string.Empty;
    }

    private void GoToPlayers()
    {
        NavigationManager.NavigateTo("/players");
    }

}