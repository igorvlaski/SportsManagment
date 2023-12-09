using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportsManagment.Shared.Domain;
using System.Net.Http.Json;

namespace SportsManagment.Blazor.Client.Pages.Selections;

public partial class SelectionById
{
    [Parameter] public Guid SelectionId { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private HttpClient Http { get; set; }
        
    private Selection selection;
    private List<TrainingAttendance> trainingAttendances { get; set; }
    private List<DateTime> currentWeekDays { get; set; }
    private DateTime currentWeekStart { get; set; }


    protected override async Task OnInitializedAsync()
    {
        try
        {

            selection = await Http.GetFromJsonAsync<Selection>($"Selection/{SelectionId}");
            if (selection == null)
            {
                Snackbar.Add("Ta selekcija ne obstaja.", Severity.Info);
            }
            else if (!selection.Players.Any())
            {
                Snackbar.Add("V tej selekciji ni igralcev.", Severity.Info);
            }
        }
        catch (Exception)
        {
            Snackbar.Add("Napaka pri pridobivanju podatkov!", Severity.Error);
        }

        currentWeekStart = GetStartOfWeek(DateTime.Today);
        InitializeWeekDays();
        await LoadAttendanceRecords();
    }

    private static DateTime GetStartOfWeek(DateTime date)
    {
        int daysUntilMonday = DayOfWeek.Monday - date.DayOfWeek;
        DateTime weekStart = date.AddDays(daysUntilMonday);

        if (daysUntilMonday > 0)
        {
            weekStart = weekStart.AddDays(-7); // Go to the previous week if today is past Monday
        }

        return weekStart;
    }

    private void InitializeWeekDays()
    {
        currentWeekDays = Enumerable.Range(0, 7)
                                    .Select(offset => currentWeekStart.AddDays(offset))
                                    .ToList();
    }

    private async Task LoadAttendanceRecords()
    {
        var allAttendances = await Http.GetFromJsonAsync<List<TrainingAttendance>>("TrainingAttendance");
        trainingAttendances = allAttendances
            .Where(a => a.SelectionId == SelectionId)
            .ToList() ?? new List<TrainingAttendance>();
    }

    public async Task OnAttendanceCheckboxChanged(bool isChecked, Guid playerId, DateOnly date, Guid? attendanceId)
    {

        if (isChecked && attendanceId == null)
        {
            await CreateTrainingAttendance(playerId, date);
        }
        else if (!isChecked && attendanceId != null)
        {
            await DeleteTrainingAttendance(attendanceId.Value);
        }
    }

    private async Task CreateTrainingAttendance(Guid playerId, DateOnly date)
    {
        var newAttendance = new TrainingAttendance
        {
            PlayerId = playerId,
            SelectionId = SelectionId,
            Date = date
        };

        var response = await Http.PostAsJsonAsync("TrainingAttendance", newAttendance);

        if (response.IsSuccessStatusCode)
        {
            Snackbar.Add($"Prisotnost uspešno ustvarjena.", Severity.Success);
            await LoadAttendanceRecords();
        }
        else
        {
            Snackbar.Add($"Prisotnost ni bila ustvarjena: {response.ReasonPhrase}", Severity.Error);
        }
    }

    private async Task DeleteTrainingAttendance(Guid attendanceId)
    {
        var response = await Http.DeleteAsync($"TrainingAttendance/{attendanceId}");

        if (response.IsSuccessStatusCode)
        {
            Snackbar.Add($"Prisotnost uspešno odstranjena.", Severity.Success);
            await LoadAttendanceRecords();
        }
        else
        {
            Snackbar.Add($"Prisotnost ni bila odstranjena: {response.ReasonPhrase}", Severity.Error);
        }
    }

    private void GoToPlayerDetails(Guid playerId)
    {
        NavigationManager.NavigateTo($"/player/{playerId}");
    }

    private void GoToEditSelection(Guid selectionId)
    {
        NavigationManager.NavigateTo($"/selection/{selectionId}/update");
    }
}