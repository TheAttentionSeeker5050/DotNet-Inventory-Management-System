﻿using Inventorium.Dtos.Dtos;
using Inventorium.Web.Services;
using Inventorium.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace Inventorium.Web.Components.Pages.References
{
    public class ProductReferencesBase : ComponentBase
    {
        // The dynamically displayed object to display product reference list
        [Parameter] public IEnumerable<ProductReferenceDto> ProductReferences { get; set; } = Enumerable.Empty<ProductReferenceDto>();

        // Reference service handler
        [Inject]
        public IReferenceService ReferenceService { get; set; }

        // Base app metadata handler (title, etc)
        [Inject]
        public IBlazorAppBase BlazorAppBase { get; set; }

        // Error message output
        public string ErrorMessage { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            try
            {
                this.BlazorAppBase.setTitle("References");
                ProductReferences = await ReferenceService.GetReferencesAsync();

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
