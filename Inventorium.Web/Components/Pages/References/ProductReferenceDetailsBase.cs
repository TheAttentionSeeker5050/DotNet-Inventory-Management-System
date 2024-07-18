using Inventorium.Dtos.Dtos;
using Inventorium.Web.Services;
using Inventorium.Web.Services.Interface;
using Microsoft.AspNetCore.Components;


namespace Inventorium.Web.Components.Pages.References
{
    public class ProductReferenceDetailsBase : ComponentBase
    {
        // The url slug parameter id
        [Parameter]
        public int referenceId { get; set; }

        // The dynamically displayed object to display product reference details
        [Parameter]
        public ProductReferenceDto ProductReference { get; set; } = new ProductReferenceDto();

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
                this.BlazorAppBase.setTitle("Reference Details");
                ProductReference = await ReferenceService.GetReferenceByIdAsync(referenceId);

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}


