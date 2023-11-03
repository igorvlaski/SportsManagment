using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using SportsManagment.Shared.Domain;


namespace SportsManagment.Blazor.Client.Pages.Players
{
    public partial class Players
    {
        [Inject] HttpClient Http { get; set; }
        private List<Player>? players = new();
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

    }
}