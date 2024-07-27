using Inventorium.Dtos.Dtos;
using Inventorium.Web.Components.Pages.Items;
using Inventorium.Web.Services;
using Inventorium.Web.Services.Interface;
using Microsoft.AspNetCore.Components;


namespace Inventorium.Web.Components.Pages.References
{
    public class AddProductReferencesBase : ComponentBase
    {
        
        // Reference service handler
        [Inject]
        public IReferenceService ReferenceService { get; set; }

        // Base app metadata handler (title, etc)
        [Inject]
        public IBlazorAppBase BlazorAppBase { get; set; }

        // Error message output
        public string ErrorMessage { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            try
            {
                this.BlazorAppBase.SetTitle("Add Product Reference");

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
