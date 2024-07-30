using Microsoft.AspNetCore.Components;

namespace Inventorium.Web.Components.Pages.References
{
    public class EditProductReferenceBase : ComponentBase
    {
        // The url slug parameter id
        [Parameter]
        public int referenceId { get; set; }
    }
}
