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
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching players: {ex.Message}");
        }
    }
    private void GoToPlayerDetails(Guid playerId)
    {
        NavigationManager.NavigateTo($"/player/{playerId}");
    }
    private void GoToEditPlayer(Guid playerId)
    {
        NavigationManager.NavigateTo($"/player/update/{playerId}");
    }

    private void GoToCreateAPlayer()
    {
        NavigationManager.NavigateTo("/create-player");
    }

    private async Task OpenDeleteConfirmationDialog(Guid playerId)
    {
        var parameters = new DialogParameters
        {
            ["ContentText"] = "Ali ste prepričani, da želite odstranit igralca?",
            ["ButtonText"] = "Odstrani",
            ["Color"] = Color.Success
        };

        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
        var dialog = DialogService.Show<ConfirmationDialog>("Delete Player", parameters, options);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await DeletePlayer(playerId);
        }
    }

    private async Task DeletePlayer(Guid playerId)
    {
        var response = await Http.DeleteAsync($"Player/{playerId}");
        if (response.IsSuccessStatusCode)
        {
            players = players.Where(p => p.Id != playerId).ToList();
            StateHasChanged();
            Snackbar.Add("Igralec uspešno odstranjen!", Severity.Warning);
        }
        else
        {
            Console.WriteLine("Napaka pri odstranitvi igralca!");
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