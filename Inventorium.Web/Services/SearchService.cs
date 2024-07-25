using Inventorium.Dtos.Dtos;
using Inventorium.Web.Services.Interface;

namespace Inventorium.Web.Services
{
    public class SearchService : ISearchService
    {
        private readonly HttpClient httpClient;

        public SearchService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        // get autosuggestions from backend api
        public async Task<List<SearchOptionDto>> GetSearchAutoSuggestionsBySearchParamAsync(string q)
        {
            try
            {
                // This method GetFromJsonAsync transform the response into json IEnumerable with the ProductDto format
                var response = await this.httpClient.GetAsync($"api/Search?q={q}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new List<SearchOptionDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<List<SearchOptionDto>>();
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


    }
}
