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

        public void setTitle(string title) {
            if (Title == null) Title = "";
            else Title = title;
        }

        public string getTitle() { return Title; }
    }
}
