﻿using Inventorium.Models;
using Inventorium.Dtos;
using Inventorium.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Inventorium.Web.Components.Pages.References
{
    public class AddProductReferencesBase : ComponentBase
    {
        // The display params
        [Parameter]
        public IEnumerable<ProductCategoryDto> ProductCategories { get; set; } = new List<ProductCategoryDto>();

        // Reference service handler
        [Inject]
        public IReferenceService ReferenceService { get; set; }

        // Category service handler
        [Inject]
        public ICategoryService CategoryService { get; set; }

        // Base app metadata handler (title, etc)
        [Inject]
        public IBlazorAppBase BlazorAppBase { get; set; }

        // Error and status message output
        public string ErrorMessage { get; set; } = "";
        public string StatusMessage { get; set; } = "";

        // the form params
        [SupplyParameterFromForm]
        protected ProductReferenceModel? ReferenceFormModel { get; set; }
        protected EditContext? FormEditContext;



        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            try
            {
                ReferenceFormModel ??= new();
                FormEditContext = new(ReferenceFormModel);
                this.BlazorAppBase.SetTitle("Add Product Reference");
                ProductCategories = await CategoryService.GetCategoriesAsync();

                
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        public async Task Submit()
        {
            try
            {

                // Add product reference with our reference service method
                var response = await ReferenceService.CreateProductReference(ReferenceFormModel);
                if (response.Name != null && response.Name.Length > 0)
                {
                    StatusMessage = "Model created successfully!";
                }
                else
                {
                    StatusMessage = "Error creating model.";
                }

                ErrorMessage = "";


                StateHasChanged();
                // Delay 5 seconds and then redirect
                await Task.Delay(5000).ConfigureAwait(false);
                await BlazorAppBase.RedirectToUrl("/product-references");

            }
            catch (Exception ex)
            {
                StatusMessage = "";
                ErrorMessage = "Something went wrong while creating a new product reference";
                StateHasChanged();
            }

        }

        
    }
}
