using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace SportsManagment.Blazor.Client.Shared
{
    public partial class DeleteConfirmationDialog
    {
        [Parameter] public string DeleteItemName { get; set; }

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        private string ContentText => $"Ali ste prepričani, da želite odstraniti {DeleteItemName}?";

        void Cancel() => MudDialog.Cancel();
        void Confirm() => MudDialog.Close(DialogResult.Ok(true));
    }
}