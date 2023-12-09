using Microsoft.AspNetCore.Components;

namespace SportsManagment.Blazor.Client.Shared;

public partial class FormButtons
{
    [Parameter] public string PrimaryButtonText { get; set; }
    [Parameter] public EventCallback OnPrimaryButtonClick { get; set; }
    [Parameter] public string SecondaryButtonText { get; set; }
    [Parameter] public EventCallback OnSecondaryButtonClick { get; set; }
    [Parameter] public EventCallback OnReset { get; set; }
}