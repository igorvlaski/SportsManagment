using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using SportsManagment.Shared.Domain;
using MudBlazor;
using SportsManagment.Blazor.Client.Shared;
using System.Net.WebSockets;

namespace SportsManagment.Blazor.Client.Pages.Players;

public partial class PlayerById
{
    [Parameter]
    public Guid PlayerId { get; set; }

    [Inject] private HttpClient Http { get; set; }
    [Inject] NavigationManager? NavigationManager { get; set; }
    [Inject] IDialogService? DialogService { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    private Player? player;
    private List<PaymentInformation> paymentInformation = new();
    private DateTime? paymentDate =  new DateTime(2023,1,1);

    protected override async Task OnInitializedAsync()
    {
        try
        {
            player = await Http.GetFromJsonAsync<Player>($"Player/{PlayerId}");
        }
        catch (Exception)
        {
            Snackbar.Add("Napaka pri pridobivanju podatkov !", Severity.Error);
        }
    }

    private async Task ConfirmDeletePlayer(Player player)
    {
        var result = await DialogService.Show<DeleteConfirmationDialog>("Potrdi odstranitev igralca",
            new DialogParameters { ["DeleteItemName"] = $"{player.FirstName} {player.LastName}" }).Result;

        if (!result.Canceled)
        {
            await DeletePlayer(player.Id);
        }
    }

    private async Task DeletePlayer(Guid playerId)
    {
        var response = await Http.DeleteAsync($"Player/{playerId}");
        if (response.IsSuccessStatusCode)
        {
            NavigationManager.NavigateTo("/players");
            Snackbar.Add("Igralec uspešno odstranjen!", Severity.Success);
        }
        else
        {
            Snackbar.Add("Napaka pri odstranjevanju podatkov. Prosim poskusite kasneje.", Severity.Error);
        }
    }

    private async Task OpenAddPaymentDialog()
    {
        var result = await DialogService.Show<AddPaymentDialog>("Dodaj plačilo", new DialogParameters { ["PlayerId"] = PlayerId }).Result;

        await LoadPaymentInformation();
        StateHasChanged();
    }

    private async Task LoadPaymentInformation()
    {
        try
        {
            paymentInformation = await Http.GetFromJsonAsync<List<PaymentInformation>>($"PaymentInformation/player/{PlayerId}?newerthen={paymentDate:yyyy-MM-dd}");

            if (paymentInformation == null || !paymentInformation.Any())
            {
                OpenAddPaymentDialog();
            }
            
        }
        catch (Exception)
        {
            Snackbar.Add("Napaka pri pridobivanju podatkov!", Severity.Error);
        }
    }

    private void GoToEditPlayer(Guid playerId)
    {
        NavigationManager.NavigateTo($"/player/{playerId}/update");
    }

}