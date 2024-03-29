using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportsManagment.Shared.Domain;
using SportsManagment.Shared.DTOs;
using System.Net.Http.Json;

namespace SportsManagment.Blazor.Client.Pages.Selections;

public partial class CreateASelection
{
    [Inject] HttpClient Http { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    private CreateSelectionDTO selection = new();
    private string? errorMessage;


    private async Task HandleValidSubmit()
    {
        try 
        {
            Snackbar.Add("Dodajam selekcijo!", Severity.Info);
            var response = await Http.PostAsJsonAsync("/Selection", selection);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Selekcija uspe�no dodana", Severity.Success);
                NavigationManager.NavigateTo("/selections");
            }
            else
            {
                errorMessage = "Selekcija ni bila dodana.";
                Snackbar.Add(errorMessage, Severity.Error);
            }
        }
        catch (Exception)
        {
            errorMessage = $"Napaka pri dodajanju selekcije. Poizkusite kasneje.";
            Snackbar.Add(errorMessage, Severity.Error);
        }
    }

    public void ResetForm()
    {
        selection = new CreateSelectionDTO();
    }

    private void GoToSelections()
    {
        NavigationManager.NavigateTo("/selections");
    }

}