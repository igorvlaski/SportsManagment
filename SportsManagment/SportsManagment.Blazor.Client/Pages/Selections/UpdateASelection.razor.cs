using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using SportsManagment.Shared.DTOs;
using MudBlazor;

namespace SportsManagment.Blazor.Client.Pages.Selections;

public partial class UpdateASelection
{
    [Parameter] public Guid SelectionId { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private HttpClient Http { get; set; }

    private UpdateSelectionDTO updateSelection;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            updateSelection = await Http.GetFromJsonAsync<UpdateSelectionDTO>($"Selection/{SelectionId}");
        }
        catch (Exception)
        {
            Snackbar.Add("Napaka pri pridobivanju podatkov!", Severity.Error);
        }
    }

    private void GoToSelectionDetails(Guid selectionId)
    {
        NavigationManager.NavigateTo($"/selection/{selectionId}");
    }

    private void GoToSelections()
    {
        NavigationManager.NavigateTo($"/selections");
    }

    private async Task HandleValidSubmit()
    {
        try
        {
            var response = await Http.PutAsJsonAsync($"Selection/{SelectionId}", updateSelection);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Selekcija uspešno posodobljena.", Severity.Success);
                GoToSelectionDetails(SelectionId);
            }
            else
            {

                Snackbar.Add("Selekcija ni bila posodobljena, poskusite kasneje.", Severity.Error);
            }
        }
        catch (Exception)
        {
            Snackbar.Add("Napaka pri posodabljanju selekcije!", Severity.Error);
        }
    }
}