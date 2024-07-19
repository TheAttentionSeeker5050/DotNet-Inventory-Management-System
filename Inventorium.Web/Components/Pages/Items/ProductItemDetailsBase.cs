using Inventorium.Dtos.Dtos;
using Inventorium.Web.Services;
using Inventorium.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace Inventorium.Web.Components.Pages.Items
{
    public class ProductItemDetailsBase : ComponentBase
    {
        // The url slug parameter id
        [Parameter]
        public int itemId { get; set; }

        // The dynamically displayed object to display product category details
        [Parameter]
        public ProductItemDto ProductItem { get; set; } = new ProductItemDto();

        // Category service handler
        [Inject]
        public IItemService ItemService { get; set; }

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
                this.BlazorAppBase.SetTitle("Item Details");
                ProductItem = await ItemService.GetItemByIdAsync(itemId);

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
