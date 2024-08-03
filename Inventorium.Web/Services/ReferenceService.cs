using Inventorium.Models;
using Inventorium.Web.Components.Pages.References;
using Inventorium.Web.Components;
using Inventorium.Web.Services.Interface;
using System.Text.Json;
using System.Text;
using Inventorium.Dtos;

namespace Inventorium.Web.Services
{
    public class ReferenceService : IReferenceService
    {
        private readonly HttpClient httpClient;
        public ReferenceService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductReferenceDto>> GetReferencesAsync()
        {
            try
            {
                // This method GetFromJsonAsync transform the response into json IEnumerable with the ProductDto format
                var response = await this.httpClient.GetAsync("api/ProductReference");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductReferenceDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductReferenceDto>>();
                }

                else
                {
                    var messsage = await response.Content.ReadAsStringAsync();
                    throw new Exception(messsage);
                }

            }
            catch (Exception ex)
            {
                // Log Exception
                // throw;
                throw new Exception($"{ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<ProductReferenceDto>> GetReferencesBySearchParamAsync(string query)
        {
            try
            {
                // This method GetFromJsonAsync transform the response into json IEnumerable with the ProductDto format
                var response = await this.httpClient.GetAsync($"api/ProductReference/Search/{query}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductReferenceDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductReferenceDto>>();
                }

                else
                {
                    var messsage = await response.Content.ReadAsStringAsync();
                    throw new Exception(messsage);
                }

            }
            catch (Exception ex)
            {
                // Log Exception
                // throw;
                throw new Exception($"{ex.Message}", ex);
            }
        }

        public async Task<ProductReferenceDto> GetReferenceByIdAsync(int id)
        {
            try
            {
                // This method GetFromJsonAsync transform the response into json with the ProductDto format for a single product
                // We use this method instead to do the type conversion after the  response is gotten, we want to control what to do if the server call is successful or not
                var response = await httpClient.GetAsync($"api/ProductReference/{id}");

                // if response is successful
                if (response.IsSuccessStatusCode)
                {
                    // if no content is returned, or empty response, return default content
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductReferenceDto);
                    }

                    // if content, make conversion and return appropriate resppnse
                    return await response.Content.ReadFromJsonAsync<ProductReferenceDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

            }
            catch (Exception ex)
            {
                // Log Exception
                // throw;
                throw new Exception($"{ex.Message}", ex);
            }
        }

        public async Task<ProductReferenceDto> CreateProductReference(ProductReferenceModel productReferenceModel)
        {
            try
            {

                // Make the request and save the response into a variable
                var response = await httpClient.PostAsJsonAsync("api/ProductReference", productReferenceModel);
                var responseContent = await response.Content.ReadAsStringAsync();
                // Check if response is successful
                if (response.IsSuccessStatusCode)
                {
                    // if no content is returned, or empty response, return default content
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductReferenceDto);
                    }

                    // if content, make conversion and return appropriate resppnse
                    return await response.Content.ReadFromJsonAsync<ProductReferenceDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception ex) 
            {
                throw new Exception($"{ex.Message}", ex);
            }
        }
    }
}
