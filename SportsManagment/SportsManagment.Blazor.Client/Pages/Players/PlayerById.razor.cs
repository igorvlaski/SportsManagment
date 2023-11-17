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
    [Inject] ISnackbar Snackbar { get; set; }

    private Player? player;
    private int activeTabIndex = 0;
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
        else
        {
            NavigationManager.NavigateTo($"/player/{playerId}?tab=0");
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
            Snackbar.Add("Napaka pri odstranjevanju podatkov. Prosim poskusite kasneje.", Severity.Error);
        }
    }

    private void GoToEditPlayer(Guid playerId)
    {
        NavigationManager.NavigateTo($"/player/{playerId}/update");
    }
}