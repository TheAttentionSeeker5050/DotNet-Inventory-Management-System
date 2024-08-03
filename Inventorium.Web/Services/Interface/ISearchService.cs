using Inventorium.Dtos;

namespace Inventorium.Web.Services.Interface
{
    public interface ISearchService
    {
        Task<List<SearchOptionDto>> GetSearchAutoSuggestionsBySearchParamAsync(string query);
    }
}
