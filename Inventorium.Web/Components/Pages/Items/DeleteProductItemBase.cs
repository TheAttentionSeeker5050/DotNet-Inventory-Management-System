using Microsoft.AspNetCore.Components;

namespace Inventorium.Web.Components.Pages.Items
{
    public class DeleteProductItemBase : ComponentBase
    {
        // The url slug parameter id
        [Parameter]
        public int itemId { get; set; }
    }
}
