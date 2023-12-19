using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportsManagment.Shared.Domain;
using System.Net.Http.Json;

namespace SportsManagment.Blazor.Client.Shared;

public partial class PaymentDialog
{
    [Inject] HttpClient Http { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    [Parameter] public Guid PlayerId { get; set; }
    [Parameter] public PaymentInformation ExistingPaymentInformation { get; set; }

    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    private PaymentInformation paymentInformation = new();
    private DateTime? tempPaymentDate = DateTime.Today;

    protected override void OnInitialized()
    {
        if (ExistingPaymentInformation != null)
        {
            paymentInformation = ExistingPaymentInformation;
            tempPaymentDate = ExistingPaymentInformation.DateOfPayment.ToDateTime(TimeOnly.MinValue);
        }
        else
        {
            paymentInformation = new PaymentInformation { PlayerId = PlayerId };
            tempPaymentDate = DateTime.Today;
        }
    }

    private async void Submit()
    {
        paymentInformation.DateOfPayment = DateOnly.FromDateTime(tempPaymentDate.Value);

        HttpResponseMessage response;
        if (ExistingPaymentInformation == null)
        {
            response = await Http.PostAsJsonAsync("/PaymentInformation", paymentInformation);
        }
        else
        {
            response = await Http.PutAsJsonAsync($"/PaymentInformation/{paymentInformation.Id}", paymentInformation);
        }

        if (response.IsSuccessStatusCode)
        {
            var message = ExistingPaymentInformation == null ? "Plačilo uspešno dodano!" : "Plačilo uspešno posodobljeno!";
            Snackbar.Add(message, Severity.Success);
        }
        else
        {
            Snackbar.Add("Napaka pri dodajanju/posodabljanju plačila!", Severity.Error);
        }

        MudDialog.Close(DialogResult.Ok(paymentInformation));
    }
    private void Cancel() => MudDialog.Cancel();

}