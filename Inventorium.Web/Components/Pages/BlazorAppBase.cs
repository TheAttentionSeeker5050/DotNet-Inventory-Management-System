
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Inventorium.Web.Components.Pages
{ 
    public interface IBlazorAppBase
    {
        // string Title;
        string GetTitle();
        void SetTitle(string title);
    }

    public class BlazorAppBase : IBlazorAppBase
    {
        // page title
        public string? Title { get; set; } = "";
        public string? AppBaseUrl { get; set; } = null;

        public BlazorAppBase()
        {
            AppBaseUrl = "https://localhost:7165";
        }
        public void SetTitle(string title) {
            if (Title == null) Title = "";
            else Title = title;
        }

        public string GetTitle() { return Title; }


    }
}
