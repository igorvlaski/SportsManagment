using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using SportsManagment.Shared.Domain;
using MudBlazor;
using SportsManagment.Blazor.Client.Shared;

namespace SportsManagment.Blazor.Client.Pages.Players;

public partial class PlayerById
{
    [Parameter]
    public Guid PlayerId { get; set; }

    [Inject] private HttpClient Http { get; set; }
    [Inject] NavigationManager? NavigationManager { get; set; }
    [Inject] IDialogService? DialogService { get; set; }

    private Player? player;
    protected override async Task OnInitializedAsync()
    {
        try
        {
            player = await Http.GetFromJsonAsync<Player>($"Player/{PlayerId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching player details: {ex.Message}");
        }
    }

    private async Task OpenDeleteConfirmationDialog(Guid playerId)
    {
        var parameters = new DialogParameters
        {
            ["ContentText"] = "Ali ste prepri?ani, da želite odstranit igralca?",
            ["ButtonText"] = "Odstrani",
            ["Color"] = Color.Error
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
            NavigationManager.NavigateTo("/players");
        }
        else
        {
            await DialogService.ShowMessageBox(
                            "Error",
                            "There was an error deleting the player. Please try again later.",
                            yesText: "OK");
        }
    }
}