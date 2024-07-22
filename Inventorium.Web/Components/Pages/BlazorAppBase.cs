
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.WebUtilities;

namespace Inventorium.Web.Components.Pages
{ 
    public interface IBlazorAppBase
    {
        // string Title;
        string GetTitle();
        void SetTitle(string title);
        void SetSearchInputRef(ElementReference SearchInput);
        ElementReference GetSearchInputRef();
        string GetURLSearchParam();
        void SetIsClickAwayOfSearchRef(bool isClickAwayOfSearchRef);
        bool GetIsClickAwayOfSearchRef();
    }

    public class BlazorAppBase : IBlazorAppBase
    {
        // page title
        public string? Title { get; set; } = "";
        public string? AppBaseUrl { get; set; } = null;
        public ElementReference SearchInput;
        public bool IsClickAwayOfSearchRef { get; set; }

        

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public BlazorAppBase(NavigationManager _navigationManager)
        {
            AppBaseUrl = "https://localhost:7165";
            NavigationManager = _navigationManager;
            SearchInput = new ElementReference();
        }
        public void SetTitle(string title) {
            if (Title == null) Title = "";
            else Title = title;
        }

        public string GetTitle() { return Title; }

        public string GetURLSearchParam()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);


            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("q", out var searchBarQuery))
            {
                return searchBarQuery.ToString();
            }

            else return null;

        }

        public void SetSearchInputRef(ElementReference _SearchInput)
        {
            SearchInput = _SearchInput;
        }

        public ElementReference GetSearchInputRef()
        {
            return SearchInput;
        }

        public void SetIsClickAwayOfSearchRef(bool isClickAwayOfSearchRef)
        {
            IsClickAwayOfSearchRef = isClickAwayOfSearchRef;
        }

        public bool GetIsClickAwayOfSearchRef()
        {
            return IsClickAwayOfSearchRef;
        }


    }
}
