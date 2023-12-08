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

    private async Task OnPickerMonthChanged(DateTime? newMonth)
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

    private void GoToEditPlayer(Guid playerId)
    {
        NavigationManager.NavigateTo($"/player/{playerId}/update");
    }

    private void GoToSelectionById(Guid selectionId)
    {
        NavigationManager.NavigateTo($"/selection/{selectionId}");
    }
}