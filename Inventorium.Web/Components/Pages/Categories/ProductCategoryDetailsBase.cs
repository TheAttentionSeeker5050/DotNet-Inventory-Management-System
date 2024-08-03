using Inventorium.Dtos;
using Inventorium.Web.Services;
using Inventorium.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace Inventorium.Web.Components.Pages.Categories
{
    public class ProductCategoryDetailsBase : ComponentBase
    {
        // The url slug parameter id
        [Parameter]
        public int categoryId { get; set; }

        // The dynamically displayed object to display product category details
        [Parameter]
        public ProductCategoryDto ProductCategory { get; set; } = new ProductCategoryDto();

        // Category service handler
        [Inject]
        public ICategoryService CategoryService { get; set; }

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
                this.BlazorAppBase.SetTitle("Category Details");
                ProductCategory = await CategoryService.GetCategoryByIdAsync(categoryId);

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
