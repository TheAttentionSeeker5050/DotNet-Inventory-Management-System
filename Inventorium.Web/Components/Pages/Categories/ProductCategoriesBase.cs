using Inventorium.Dtos.Dtos;
using Inventorium.Web.Services.Interface;
using Microsoft.AspNetCore.Components;



namespace Inventorium.Web.Components.Pages.Categories
{
    public class ProductCategoriesBase : ComponentBase
    {
        /*public readonly IBlazorAppBase blazorAppBase;

        public ProductCategoriesBase(IBlazorAppBase BlazorAppBase)
        {
            this.blazorAppBase = blazorAppBase;
        }*/

        [Parameter]
        public IEnumerable<ProductCategoryDto> ProductCategories { get; set; } = Enumerable.Empty<ProductCategoryDto>();

        [Inject]
        public ICategoryService CategoryService { get; set; }

        [Inject]
        public IBlazorAppBase BlazorAppBase { get; set; }

        public string ErrorMessage { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            
            try
            {
                this.BlazorAppBase.setTitle("Categories");
                ProductCategories = await CategoryService.GetCategoriesAsync();

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
