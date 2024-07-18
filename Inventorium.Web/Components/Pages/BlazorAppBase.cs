
using Microsoft.AspNetCore.Components;

namespace Inventorium.Web.Components.Pages
{ 
    public interface IBlazorAppBase
    {
        // string Title;
        string getTitle();
        void setTitle(string title);
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
        public void setTitle(string title) {
            if (Title == null) Title = "";
            else Title = title;
        }

        public string getTitle() { return Title; }
    }
}
