using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SportsManagment.Shared.Domain;
using SportsManagment.Blazor.Client.Shared;


namespace SportsManagment.Blazor.Client.Pages.Players;

public partial class Players
{
    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] NavigationManager? NavigationManager { get; set; }
    [Inject] IDialogService? DialogService { get; set; }
    [Inject] HttpClient Http { get; set; }

    private List<Player>? players = new();
    private string searchString = "";

    protected override async Task OnInitializedAsync()
    {
        try
        { 
            players = await Http.GetFromJsonAsync<List<Player>>("Player");
            if (players == null || !players.Any())
            {
                Snackbar.Add("V podatkovni bazi ni igralcev,prosim ustvarite Igralca.", Severity.Info);
                GoToCreateAPlayer();
            }
        }
        catch (Exception)
        {
            Snackbar.Add("Napaka pri pridobivanju podatkov !", Severity.Error);
        }
    }
    private void GoToPlayerDetails(Guid playerId)
    {
        NavigationManager.NavigateTo($"/player/{playerId}");
    }
    private void GoToEditPlayer(Guid playerId)
    {
        NavigationManager.NavigateTo($"/player/{playerId}/update");
    }

    private void GoToCreateAPlayer()
    {
        NavigationManager.NavigateTo("/player");
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
                players.RemoveAll(p => p.Id == player.Id);
                StateHasChanged();
                Snackbar.Add("Igralec uspešno odstranjen!", Severity.Success);
            }
            else
            {
                Snackbar.Add("Napaka pri odstranjevanju podatkov. Prosim poskusite kasneje.", Severity.Error);
            }
        }
    }

    private bool FilterFunction(Player player)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;

        var search = searchString.Trim();

        if (player.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase))
            return true;

        if (player.LastName.Contains(search, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

}