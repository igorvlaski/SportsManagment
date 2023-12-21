using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportsManagment.Shared.Domain;
using System.Net.Http.Json;

namespace SportsManagment.Blazor.Client.Pages.MeasurementInformations;

public partial class MeasurementInformations
{
    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] NavigationManager? NavigationManager { get; set; }
    [Inject] IDialogService? DialogService { get; set; }
    [Inject] HttpClient Http { get; set; }

    private List<MeasurementInformation> measurementInformations = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            measurementInformations = await Http.GetFromJsonAsync<List<MeasurementInformation>>("MeasurementInformation");
            if (measurementInformations == null || !measurementInformations.Any())
            {
                Snackbar.Add("V podatkovni bazi ni informacij o meritvah, prosim ustvarite Meritev.", Severity.Info);

            }
        }
        catch (Exception)
        {
            Snackbar.Add("Napaka pri pridobivanju podatkov !", Severity.Error);

        }
    }
}