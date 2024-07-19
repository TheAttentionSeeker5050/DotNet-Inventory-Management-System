using Inventorium.Dtos.Dtos;
using Inventorium.Web.Components.Pages.References;
using Inventorium.Web.Services;
using Inventorium.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace Inventorium.Web.Components.Pages.Items
{
    public class ProductItemsBase : ComponentBase
    {
        // The dynamically displayed object to display product reference list
        [Parameter] public IEnumerable<ProductItemDto> ProductItems { get; set; } = Enumerable.Empty<ProductItemDto>();

        // Reference service handler
        [Inject]
        public IItemService ItemService{ get; set; }

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
                this.BlazorAppBase.SetTitle("Items");
                ProductItems = await ItemService.GetItemsAsync();

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
