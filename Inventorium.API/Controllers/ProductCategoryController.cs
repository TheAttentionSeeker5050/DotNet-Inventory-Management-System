using Microsoft.AspNetCore.Mvc;
using Inventorium.API.Extensions;
using Inventorium.API.Repositories.Contracts;
using Inventorium.Dtos;
using Inventorium.Models;

namespace Inventorium.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        // add readonly context
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductReferenceRepository _productReferenceRepository;

        // constructor
        public ProductCategoryController(IProductCategoryRepository productCategoryRepository, IProductReferenceRepository productReferenceRepository)
        {
            _productCategoryRepository = productCategoryRepository;
            _productReferenceRepository = productReferenceRepository;
        }

        // GET all the product categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
        {
            try
            {
                var productCategories = await _productCategoryRepository.GetProductCategories();
                if (productCategories == null)
                {
                    return NotFound();
                } else
                {
                    var productCategoriesDtos = productCategories.ConvertToDto();
                    return Ok(productCategoriesDtos);
                }

            } catch (Exception ex)
            {
                // If an exception occurs return the status code 500 with a error message
                return StatusCode(
                    StatusCodes.Status500InternalServerError, "Could not get the product categories from the database"
                );
            }
        }

        // GET all the searches for categories
        [HttpGet("Search/{query}")]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetCategoriesSearchResults(string query)
        {
            try
            {
                // Get the product searches of every table type

                var productCategories = await _productCategoryRepository.GetProductCategoriesBySearchQuery(query);
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
        }

        // GET single product category by ID with children tables
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDto>> GetProductCategoryById(int id)
        {
            try
            {
                var productCategory = await _productCategoryRepository.GetProductCategoryById(id);
                var productReferences = await _productReferenceRepository.GetProductReferences();

                if (productCategory == null || productReferences == null || productReferences.ToList().Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    
                    var productCategoriesDto = productCategory.ConvertToDto(productReferences);
                    return Ok(productCategoriesDto);

                }
            }
            catch (Exception ex)
            {
                // If an exception occurs return the status code 500 with a error message
                return StatusCode(
                    StatusCodes.Status500InternalServerError, "Could not get the product category data from the database"
                );
            }
        }

        // POST new product category
        [HttpPost]
        public ActionResult CreateProductCategory(ProductCategoryModel newProductCategory)
        {
            try
            {
                // create product category
                var createdProductCategory = _productCategoryRepository.CreateProductCategory(newProductCategory);

                if (createdProductCategory == null)
                {
                    return BadRequest();
                }

                return CreatedAtAction(nameof(CreateProductCategory), createdProductCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE product category and its related table items
        [HttpDelete("{id}")]
        public ActionResult DeleteProductCategory(int id)
        {
            try
            {
                _productCategoryRepository.DeleteProductCategoryById(id);
                return Ok();

            } catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT update product category
        [HttpPut("{id}")]
        public ActionResult UpdateProductCategory(int id, ProductCategoryModel newProductCategory)
        {
            try
            {
                _productCategoryRepository.UpdateProductCategoryById(id, newProductCategory);
                return Ok();

            } catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
