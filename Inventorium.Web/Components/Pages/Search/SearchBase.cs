using Inventorium.Web.Components.Pages.References;
using Inventorium.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Inventorium.Web.Components.Pages.Search
{
    public class SearchBase : ComponentBase
    {
        // Base app metadata handler (title, etc)
        [Inject]
        public IBlazorAppBase BlazorAppBase { get; set; }

        // Error message output
        public string ErrorMessage { get; set; }

        public string SearchParam { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            try
            {
                // get url search param
                SearchParam = BlazorAppBase.GetURLSearchParam();


                if (BlazorAppBase.GetURLSearchParam() != null)
                {
                    this.BlazorAppBase.SetTitle("References Search");
                }
                else
                {
                    this.BlazorAppBase.SetTitle("References");
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
