using Microsoft.AspNetCore.Components;

namespace Inventorium.Web.Components.Pages.Categories
{
    public class EditProductCategoryBase : ComponentBase
    {
        // The url slug parameter id
        [Parameter]
        public int categoryId { get; set; }
    }
}
