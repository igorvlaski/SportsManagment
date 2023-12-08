using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportsManagment.Shared.Domain;
using System.Net.Http.Json;

namespace SportsManagment.Blazor.Client.Shared;

public partial class AddPaymentDialog
{
    [Inject] HttpClient Http { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    [Parameter] public Guid PlayerId { get; set; }
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    private PaymentInformation paymentInformation = new();
    private DateTime? tempPaymentDate = DateTime.Today;


    private async void Submit()
    {
        paymentInformation.PlayerId = PlayerId;
        paymentInformation.DateOfPayment = DateOnly.FromDateTime(tempPaymentDate.Value);
        var response = await Http.PostAsJsonAsync("/PaymentInformation", paymentInformation);
        if (response.IsSuccessStatusCode)
        {
            Snackbar.Add("Plačilo uspešno dodano!", Severity.Success);
        }
        else 
        {
            Snackbar.Add("Napaka dodajanju plačila!", Severity.Error);
        }


        MudDialog.Close(DialogResult.Ok(paymentInformation));
    }
    private void Cancel() => MudDialog.Cancel();

}

