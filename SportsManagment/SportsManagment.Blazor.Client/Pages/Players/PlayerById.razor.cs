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
            await LoadAndFetchTrainingAttendances();
        }
    }


    private async Task LoadAndFetchTrainingAttendances()
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

    private async Task AddSelection(Guid selectionId)
    {
        if (selectionId != Guid.Empty)
        {
            await Http.PatchAsync($"Player/{PlayerId}/selection/{selectionId}", null);
            var selectionToAdd = allSelections.FirstOrDefault(s => s.Id == selectionId);
            if (selectionToAdd != null)
            {
                player.Selections.Add(selectionToAdd);
                selectedNewSelectionId = Guid.Empty; // Reset selection
                StateHasChanged();
                Snackbar.Add($"Igralec dodan v selekcijo {selectionToAdd.SelectionName}", Severity.Success);
            }
            else
            {
                Snackbar.Add("Napaka pri dodajanju igralca v selekcijo.", Severity.Error);
            }
        }
    }

    private async Task RemoveSelection(Guid selectionId)
    {
        await Http.PatchAsync($"Player/{PlayerId}/selection/{selectionId}", null);
        var selectionToRemove = player.Selections.FirstOrDefault(s => s.Id == selectionId);
        if (selectionToRemove != null)
        {
            player.Selections.Remove(selectionToRemove);
            Snackbar.Add($"Igralec odstranjen iz selekcije {selectionToRemove.SelectionName}", Severity.Success);
        }
        else
        {
            Snackbar.Add("Napaka pri odstranjevanju igralca iz selekcije.", Severity.Error);
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

    private void GoToEditPlayer(Guid playerId)
    {
        NavigationManager.NavigateTo($"/player/{playerId}/update");
    }

    private void GoToSelectionById(Guid selectionId)
    {
        NavigationManager.NavigateTo($"/selection/{selectionId}");
    }
}