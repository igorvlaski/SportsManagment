using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportsManagment.Shared.Domain;
using SportsManagment.Blazor.Client.Shared;
using SportsManagment.Blazor.Client.Pages.Players;

namespace SportsManagment.Blazor.Client.Pages.Selections;

public partial class Selections
{
    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] HttpClient Http { get; set; }
    [Inject] NavigationManager? NavigationManager { get; set; }
    [Inject] IDialogService? DialogService { get; set; }

    private List<Selection> selections = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            selections = await Http.GetFromJsonAsync<List<Selection>>("Selection");
            if (selections == null || !selections.Any())
            {
                Snackbar.Add("V podatkovni bazi ni selekcij,prosim ustvarite Selekcijo.", Severity.Info);
                GoToCreateASelection();
            }
        }
        catch 
        {
            Snackbar.Add("Napaka pri pridobivanju podatkov !", Severity.Error);
        }
    }

    private async Task ConfirmDeleteSelection(Selection selection)
    {
        var result = await DialogService.Show<DeleteConfirmationDialog>("Potrdi odstranitev selekcije",
            new DialogParameters { ["DeleteItemName"] = $"{selection.SelectionName}" }).Result;

        if (!result.Canceled)
        {
            await DeleteSelection(selection.Id);
        }
    }

    private async Task DeleteSelection(Guid selectionId)
    {
        var response = await Http.DeleteAsync($"Selection/{selectionId}");
        if (response.IsSuccessStatusCode)
        {
            selections.RemoveAll(p => p.Id == selectionId);
            StateHasChanged();
            Snackbar.Add("Selekcija uspešno odstranjena!", Severity.Success);
        }
        else
        {
            Snackbar.Add("Napaka pri odstranjevanju podatkov. Prosim poskusite kasneje.", Severity.Error);
        }
    }

    private void GoToCreateASelection()
    {
        NavigationManager.NavigateTo("/selection");
    }
    private void GoToSelectionDetails(Guid selectionId)
    {
        NavigationManager.NavigateTo($"/selection/{selectionId}");
    }
    private void GoToEditSelection(Guid selectionId)
    {
        NavigationManager.NavigateTo($"/selection/{selectionId}/update");
    }
}