using Inventorium.API.Extensions;
using Inventorium.API.Repositories.Contracts;
using Inventorium.Dtos.Dtos;
using Microsoft.AspNetCore.Http;
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
/*
        // GET all the searches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchOptionDto>>> GetSearchResults(string query)
        {
            try
            {
                // Get the product searches of every table type

                var productCategories = await _productCategoryRepository.GetProductCategoryBySearchQuery(query);
                if (productCategories == null)
                {
                    return NotFound();
                }
                else
                {
                    var productCategoriesDtos = productCategories.ConvertToDto();
                    return Ok(productCategoriesDtos);
                }

            }
            catch (Exception ex)
            {
                // If an exception occurs return the status code 500 with a error message
                return StatusCode(
                    StatusCodes.Status500InternalServerError, "Could not get the product categories from the database"
                );
            }
        }*/
    }
}
