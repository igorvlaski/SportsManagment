using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportsManagment.Blazor.Client.Shared;
using SportsManagment.Shared.Domain;
using System.Net.Http.Json;

namespace SportsManagment.Blazor.Client.Pages.MeasurementInformations;

public partial class MeasurementInformations
{
    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] NavigationManager? NavigationManager { get; set; }
    [Inject] IDialogService? DialogService { get; set; }
    [Inject] HttpClient Http { get; set; }

    private List<MeasurementInformation> measurementInformation = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadMeasurementInformation();
    }

    private async Task LoadMeasurementInformation()
    {
        try
        {
            measurementInformation = await Http.GetFromJsonAsync<List<MeasurementInformation>>("MeasurementInformation");
            if (measurementInformation == null || !measurementInformation.Any())
            {
                Snackbar.Add("V podatkovni bazi ni informacij o meritvah, prosim ustvarite Meritev.", Severity.Info);
                await OpenAddMeasurementInformationDialog();
                StateHasChanged();
            }
        }
        catch (Exception)
        {
            Snackbar.Add("Napaka pri pridobivanju podatkov !", Severity.Error);

        }
    }


    private async Task OpenAddMeasurementInformationDialog()
    {
        var result = await DialogService.Show<MasurementInformationDialog>("Dodaj informacije meritev", new DialogParameters()).Result;

            await LoadMeasurementInformation();
            StateHasChanged();
        
    }

    private async Task OpenEditMeasurmentInformationDialog(MeasurementInformation measurementInformation)
    {
        var result = await DialogService.Show<MasurementInformationDialog>("Uredi informacije meritve", new DialogParameters
        {
            ["existingMeasurementInformation"] = measurementInformation
        }).Result;

        await LoadMeasurementInformation();
        StateHasChanged();
    }

    private async Task DeleteMeasurmentInformation(MeasurementInformation measurementInformation)
    {
        var result = await DialogService.Show<DeleteConfirmationDialog>("Potrdi odstranitev meritve",
            new DialogParameters { ["DeleteItemName"] = "meritev " + measurementInformation.Description + " za dan in čas " + measurementInformation.Date }).Result;

        if (!result.Canceled)
        {
            var response = await Http.DeleteAsync($"MeasurementInformation/{measurementInformation.Id}");
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Meritve uspešno odstranjene!", Severity.Success);
                await LoadMeasurementInformation();
                StateHasChanged();
            }
            else
            {
                Snackbar.Add("Napaka pri odstranjevanju meritev.", Severity.Error);
            }
        }
    }
}