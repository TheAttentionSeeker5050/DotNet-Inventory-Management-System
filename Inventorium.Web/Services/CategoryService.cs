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
    }
}
