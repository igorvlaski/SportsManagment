using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using SportsManagment.Shared.Domain;
using MudBlazor;
using SportsManagment.Blazor.Client.Shared;

namespace SportsManagment.Blazor.Client.Pages.Players;

public partial class PlayerById
{
    [Parameter] public Guid PlayerId { get; set; }
    [Inject] private HttpClient Http { get; set; }
    [Inject] NavigationManager? NavigationManager { get; set; }
    [Inject] IDialogService? DialogService { get; set; }
    [Inject] ISnackbar Snackbar { get; set; }

    private Player? player;
    private List<PaymentInformation> paymentInformation = new();
    private DateTime? paymentDate =  new DateTime(2023,1,1);
    private List<Selection> allSelections = new();
    private Guid selectedNewSelectionId;
    private List<TrainingAttendance> trainingAttendances = new ();
    private DateTime selectedMonth = DateTime.Today;
    private List<DateTime> daysInSelectedMonth = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            player = await Http.GetFromJsonAsync<Player>($"Player/{PlayerId}?includeSelections=true");
            allSelections = await Http.GetFromJsonAsync<List<Selection>>("Selection");
        }
        catch (Exception)
        {
            Snackbar.Add("Napaka pri pridobivanju podatkov!", Severity.Error);
        }
    }

    private async Task OnMonthChanged(DateTime? newMonth)
    {
        if (newMonth.HasValue)
        {
            selectedMonth = newMonth.Value;
            await LoadTrainingAttendances();
        }
    }

    private async Task LoadTrainingAttendances()
    {
        GenerateDaysInMonth(selectedMonth);

        var startDate = new DateOnly(selectedMonth.Year, selectedMonth.Month, 1).AddMonths(-1).AddDays(1);


        trainingAttendances = await Http.GetFromJsonAsync<List<TrainingAttendance>>($"TrainingAttendance/player/{PlayerId}?newerthen={startDate:yyyy-MM-dd}");
    }

    private void GenerateDaysInMonth(DateTime selectedMonth)
    {
        daysInSelectedMonth.Clear();
        int daysCount = DateTime.DaysInMonth(selectedMonth.Year, selectedMonth.Month);
        for (int day = 1; day <= daysCount; day++)
        {
            daysInSelectedMonth.Add(new DateTime(selectedMonth.Year, selectedMonth.Month, day));
        }
    }

    private async Task UpdatePlayerSelection(Guid selectionId)
    {
        bool currentlySelected = player.Selections.Any(s => s.Id == selectionId);
        var response = await Http.PatchAsync($"Player/{PlayerId}/selection/{selectionId}", null);

        var selection = allSelections.FirstOrDefault(s => s.Id == selectionId);
        if (selection == null)
        {
            Snackbar.Add("Selekcija ni najdena.", Severity.Error);
            return;
        }

        if (currentlySelected)
        {
            var existingSelection = player.Selections.FirstOrDefault(s => s.Id == selectionId);
            if (existingSelection != null)
            {
                player.Selections.Remove(existingSelection);
                Snackbar.Add($"Igralec odstranjen iz selekcije {selection.SelectionName}", Severity.Success);
            }
        }
        else
        {
            player.Selections.Add(selection);
            Snackbar.Add($"Igralec dodan v selekcijo {selection.SelectionName}", Severity.Success);
        }

        selectedNewSelectionId = Guid.Empty;
        StateHasChanged();
    }

    private async Task DeletePlayer(Player player)
    {
        var confirmationResult = await DialogService.Show<DeleteConfirmationDialog>(
            "Potrdi odstranitev igralca",
            new DialogParameters { ["DeleteItemName"] = $"{player.FirstName} {player.LastName}" }).Result;

        if (!confirmationResult.Canceled)
        {
            var deleteResponse = await Http.DeleteAsync($"Player/{player.Id}");
            if (deleteResponse.IsSuccessStatusCode)
            {
                Snackbar.Add("Igralec uspešno odstranjen!", Severity.Success);
                NavigationManager.NavigateTo("/players");
            }
            else
            {
                Snackbar.Add("Napaka pri odstranjevanju podatkov. Prosim poskusite kasneje.", Severity.Error);
            }
        }
    }

    private async Task OpenAddPaymentDialog()
    {
        var result = await DialogService.Show<PaymentDialog>("Dodaj plačilo", new DialogParameters { ["PlayerId"] = PlayerId }).Result;


        await LoadPaymentInformation();
        StateHasChanged();
    }

    private async Task OpenEditPaymentDialog(PaymentInformation payment)
    {
        var result = await DialogService.Show<PaymentDialog>("Uredi plačilo", new DialogParameters
        {
            ["PlayerId"] = PlayerId,
            ["ExistingPaymentInformation"] = payment
        }).Result;

        await LoadPaymentInformation();
        StateHasChanged();
    }

    private async Task LoadPaymentInformation()
    {
        try
        {
            paymentInformation = await Http.GetFromJsonAsync<List<PaymentInformation>>
                                                            ($"PaymentInformation/player/{PlayerId}?newerthen={paymentDate:yyyy-MM-dd}");
        }
        catch (Exception)
        {
            Snackbar.Add("Napaka pri pridobivanju podatkov!", Severity.Error);
        }
    }

    private async Task LoadPaymentInformationBasedOnDate(DateTime? newDate)
    {
        if (newDate.HasValue)
        {
            paymentDate = newDate.Value;
            await LoadPaymentInformation();
        }
    }

    private async Task DeletePayment(PaymentInformation paymentInformation)
    {
        var result = await DialogService.Show<DeleteConfirmationDialog>("Potrdi odstranitev plačila",
            new DialogParameters { ["DeleteItemName"] = "plačilo " +paymentInformation.Description + " v znesku " + paymentInformation.Amount}).Result;

        if (!result.Canceled)
        {
            var response = await Http.DeleteAsync($"PaymentInformation/{paymentInformation.Id}");
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add("Plačilo uspešno odstranjeno!", Severity.Success);
                await LoadPaymentInformation();
            }
            else
            {
                Snackbar.Add("Napaka pri odstranjevanju plačila.", Severity.Error);
            }
        }
    }

    private void GoToEditPlayer(Guid playerId)
    {
        NavigationManager.NavigateTo($"/player/{playerId}/update");
    }

    private void GoToSelectionById(Guid selectionId)
    {
        NavigationManager.NavigateTo($"/selection/{selectionId}");
    }
}