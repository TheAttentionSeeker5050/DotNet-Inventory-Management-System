using Microsoft.AspNetCore.Components;

namespace Inventorium.Web.Components.Pages.Items
{
    public class EditProductItemBase : ComponentBase
    {
        // The url slug parameter id
        [Parameter]
        public int itemId { get; set; }
    }
}
