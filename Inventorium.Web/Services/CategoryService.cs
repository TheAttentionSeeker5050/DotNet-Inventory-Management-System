using Inventorium.Dtos.Dtos;
using Inventorium.Web.Services.Interface;

namespace Inventorium.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient httpClient;
        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetCategoriesAsync()
        {
            try
            {
                // This method GetFromJsonAsync transform the response into json IEnumerable with the ProductDto format
                var response = await this.httpClient.GetAsync("api/ProductCategory");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductCategoryDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductCategoryDto>>();
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
                throw;
                // throw new Exception($"{ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetCategoriesBySearchParamAsync(string query)
        {
            try
            {
                // This method GetFromJsonAsync transform the response into json IEnumerable with the ProductDto format
                var response = await this.httpClient.GetAsync($"api/ProductCategory/Search/{query}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductCategoryDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductCategoryDto>>();
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
                throw;
                // throw new Exception($"{ex.Message}", ex);
            }
        }

        public async Task<ProductCategoryDto> GetCategoryByIdAsync(int id)
        {
            try
            {
                // This method GetFromJsonAsync transform the response into json with the ProductDto format for a single product
                // We use this method instead to do the type conversion after the  response is gotten, we want to control what to do if the server call is successful or not
                var response = await httpClient.GetAsync($"api/ProductCategory/{id}");

                // if response is successful
                if (response.IsSuccessStatusCode)
                {
                    // if no content is returned, or empty response, return default content
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductCategoryDto);
                    }

                    // if content, make conversion and return appropriate resppnse
                    return await response.Content.ReadFromJsonAsync<ProductCategoryDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

            }
            catch (Exception)
            {
                // Log Exception
                throw;
            }
        }
    }
}
