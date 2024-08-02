using Inventorium.API.Models;
using Inventorium.Dtos.Dtos;
using Inventorium.Web.Services.Interface;
using System.Net.Http;

namespace Inventorium.Web.Services
{
    public class ItemService : IItemService
    {
        private readonly HttpClient httpClient;
        public ItemService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductItemDto>> GetItemsAsync()
        {
            try
            {
                // This method GetFromJsonAsync transform the response into json IEnumerable with the ProductDto format
                var response = await this.httpClient.GetAsync("api/ProductItem");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductItemDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductItemDto>>();
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

        public async Task<IEnumerable<ProductItemDto>> GetItemsBySearchParamAsync(string query)
        {
            try
            {
                // This method GetFromJsonAsync transform the response into json IEnumerable with the ProductDto format
                var response = await this.httpClient.GetAsync($"api/ProductItem/Search/{query}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductItemDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductItemDto>>();
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

        public async Task<ProductItemDto> GetItemByIdAsync(int id)
        {
            try
            {
                // This method GetFromJsonAsync transform the response into json with the ProductDto format for a single product
                // We use this method instead to do the type conversion after the  response is gotten, we want to control what to do if the server call is successful or not
                var response = await httpClient.GetAsync($"api/ProductItem/{id}");

                // if response is successful
                if (response.IsSuccessStatusCode)
                {
                    // if no content is returned, or empty response, return default content
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductItemDto);
                    }

                    // if content, make conversion and return appropriate resppnse
                    return await response.Content.ReadFromJsonAsync<ProductItemDto>();
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

        public async Task<ProductItemDto> CreateProductItem(ProductItemModel productItemModel)
        {
            try
            {

                // Make the request and save the response into a variable
                var response = await httpClient.PostAsJsonAsync("api/ProductItem", productItemModel);
                var responseContent = await response.Content.ReadAsStringAsync();
                // Check if response is successful
                if (response.IsSuccessStatusCode)
                {
                    // if no content is returned, or empty response, return default content
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductItemDto);
                    }

                    // if content, make conversion and return appropriate resppnse
                    return await response.Content.ReadFromJsonAsync<ProductItemDto>();
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
