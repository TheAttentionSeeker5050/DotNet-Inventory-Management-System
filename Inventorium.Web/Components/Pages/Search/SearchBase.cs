using Inventorium.Dtos.Dtos;
using Inventorium.Web.Components.Pages.References;
using Inventorium.Web.Services;
using Inventorium.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace Inventorium.Web.Components.Pages.Search
{
    public class SearchBase : ComponentBase
    {
        // Base app metadata handler (title, etc)
        [Inject]
        public IBlazorAppBase BlazorAppBase { get; set; }

        // Inject page specific blazor objects
        
        [Inject]
        public ISearchService SearchService { get; set; }

        [Parameter]
        public  IEnumerable<SearchOptionDto> SearchResults { get; set; } = Enumerable.Empty<SearchOptionDto>();


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

                this.BlazorAppBase.SetTitle("Search");

                if (BlazorAppBase.GetURLSearchParam() != null)
                {
                    this.BlazorAppBase.SetTitle("Search");
                    SearchResults = await SearchService.GetSearchAutoSuggestionsBySearchParamAsync(SearchParam);
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
