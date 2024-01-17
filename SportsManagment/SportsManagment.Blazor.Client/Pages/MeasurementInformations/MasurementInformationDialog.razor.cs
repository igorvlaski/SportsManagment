using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportsManagment.Shared.Domain;
using System.Net.Http.Json;

namespace SportsManagment.Blazor.Client.Pages.MeasurementInformations;

public partial class MasurementInformationDialog
{
    [Inject] HttpClient Http { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }
    [Parameter] public MeasurementInformation existingMeasurementInformation { get; set; }
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    private MeasurementInformation measurementInformation = new();
    private DateTime? tempDate = new DateTime(2024,1,1);
    private TimeSpan? tempTime;

    protected override void OnInitialized()
    {
        if (existingMeasurementInformation != null)
        {
            measurementInformation = existingMeasurementInformation;
        }
        else 
        {
            measurementInformation = new MeasurementInformation();
        }
    }

    private async void OnSubmit()
    {

        if (tempDate.HasValue && tempTime.HasValue)
        {
            DateTime tempUtcDate = tempDate.Value.ToUniversalTime();
            tempUtcDate = tempUtcDate.AddTicks(tempTime.Value.Ticks);
            measurementInformation.Date = tempUtcDate;

            HttpResponseMessage response;
            if (existingMeasurementInformation == null)
            {
                response = await Http.PostAsJsonAsync("/MeasurementInformation", measurementInformation);
            }
            else
            {
                response = await Http.PutAsJsonAsync($"/MeasurementInformation/{measurementInformation.Id}", measurementInformation);
            }

            if (response.IsSuccessStatusCode)
            {
                var message = existingMeasurementInformation == null ? "Informacije o meritvah uspešno dodane!" : "Informacije o meritvah uspešno posodobljne!";
                Snackbar.Add(message, Severity.Success);
            }
            else
            {
                Snackbar.Add("Napaka pri dodajanju/posodabljanju informacij o meritvah!", Severity.Error);
            }
            MudDialog.Close(DialogResult.Ok(measurementInformation));
        }
    }
    private void Cancel() => MudDialog.Cancel();
}