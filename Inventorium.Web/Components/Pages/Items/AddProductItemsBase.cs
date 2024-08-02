using Inventorium.API.Models;
using Inventorium.Dtos.Dtos;
using Inventorium.Web.Components.Pages.Categories;
using Inventorium.Web.Services;
using Inventorium.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Inventorium.Web.Components.Pages.Items
{
    public class AddProductItemsBase : ComponentBase
    {
        // The display params
        [Parameter]
        public IEnumerable<ProductReferenceDto> ProductReferences { get; set; } = new List<ProductReferenceDto>();


        // Item service handler
        [Inject]
        public IItemService ItemService { get; set; }

        // Reference service handler
        [Inject]
        public IReferenceService ReferenceService { get; set; }

        // Base app metadata handler (title, etc)
        [Inject]
        public IBlazorAppBase BlazorAppBase { get; set; }

        // Error and status message output
        public string ErrorMessage { get; set; } = "";
        public string StatusMessage { get; set; } = "";

        // the form params
        [SupplyParameterFromForm]
        protected ProductItemModel? ReferenceFormModel { get; set; }
        protected EditContext? FormEditContext;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            try
            {
                ReferenceFormModel ??= new();
                FormEditContext = new(ReferenceFormModel);
                this.BlazorAppBase.SetTitle("Add Product Item");
                ProductReferences = await ReferenceService.GetReferencesAsync();

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async Task Submit()
        {
            try
            {

                // Add product reference with our reference service method
                var response = await ItemService.CreateProductItem(ReferenceFormModel);
                if (response.Name != null && response.Name.Length > 0)
                {
                    StatusMessage = "Model created successfully!";
                }
                else
                {
                    StatusMessage = "Error creating model.";
                }

                StatusMessage = "Model created successfully!";

                /*ErrorMessage = "";*/


                StateHasChanged();
                // Delay 5 seconds and then redirect
                await Task.Delay(5000).ConfigureAwait(false);
                await BlazorAppBase.RedirectToUrl("/product-items");

            }
            catch (Exception ex)
            {
                StatusMessage = "";
                ErrorMessage = "Something went wrong while creating a new product item";
                StateHasChanged();
            }

        }
    }
}
