using Inventorium.Web.Components;
using Inventorium.Web.Components.Pages;
using Inventorium.Web.Services;
using Inventorium.Web.Services.Interface;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();



// API service for fetching data from backend
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IReferenceService,ReferenceService>();
builder.Services.AddScoped<IItemService,ItemService>();
builder.Services.AddScoped<ISearchService, SearchService>();

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7188/api")
    });

// App base handler for app wide metadata
builder.Services.AddScoped<IBlazorAppBase, BlazorAppBase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();