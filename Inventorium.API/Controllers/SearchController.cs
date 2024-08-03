using Inventorium.API.Extensions;
using Inventorium.API.Repositories.Contracts;
using Inventorium.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Inventorium.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        // add readonly context
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductReferenceRepository _productReferenceRepository;
        private readonly IProductItemRepository _productItemRepository;

        // constructor
        public SearchController(IProductCategoryRepository productCategoryRepository, IProductReferenceRepository productReferenceRepository, IProductItemRepository productItemRepository)
        {
            _productCategoryRepository = productCategoryRepository;
            _productReferenceRepository = productReferenceRepository;
            _productItemRepository = productItemRepository;
        }

        // GET all the searches for the query
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchOptionDto>>> GetAllSearchResults([FromQuery]string q)
        {
            try
            {

                // initiate the SearchOptionDto IEnumeralble
                var searchOptionList = new List<SearchOptionDto>();


                // get product references searches
                var productReferences = await _productReferenceRepository.GetProductReferencesBySearchQuery(q);

                if (productReferences != null && productReferences.Count() > 0)
                {
                    var productReferencesSearchDtos = productReferences.ConvertToSearchDto();

                    if (productReferencesSearchDtos != null && productReferencesSearchDtos.Count() > 0)
                    {
                        // append the product reference search dto to searchOptionList, only include first 3
                        searchOptionList.AddRange(productReferencesSearchDtos.Take(3));
                    }
                }

                // get product items searches
                var productItems = await _productItemRepository.GetProductItemsBySearchQuery(q);

                if (productItems != null && productItems.ToList().Count() > 0)
                {
                    var productItemsSearchDtos = productItems.ConvertToSearchDto();

                    if (productItemsSearchDtos != null && productItemsSearchDtos.Count() > 0)
                    {
                        searchOptionList.AddRange(productItemsSearchDtos.Take(3));
                    }
                }

                var productCategories = await _productCategoryRepository.GetProductCategoriesBySearchQuery(q);
                if (productCategories != null && productCategories.ToList().Count > 0)
                {
                    var productCategoriesSearchDtos = productCategories.ConvertToSearchDto();

                    if (productCategoriesSearchDtos != null && productCategoriesSearchDtos.Count() > 0)
                    {
                        searchOptionList.AddRange(productCategoriesSearchDtos.Take(3));
                    }
                }


                return Ok(searchOptionList);
                
            }
            catch (Exception ex)
            {
                // If an exception occurs return the status code 500 with a error message
                return StatusCode(
                    StatusCodes.Status500InternalServerError, "Could not get the resource from the database"
                );
            }
        }
    }
}
