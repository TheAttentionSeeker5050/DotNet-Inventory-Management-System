using Inventorium.API.Extensions;
using Inventorium.API.Models;
using Inventorium.API.Repositories;
using Inventorium.API.Repositories.Contracts;
using Inventorium.Dtos.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Inventorium.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        // add readonly context
        private readonly IProductItemRepository _productItemRepository;
        private readonly IProductReferenceRepository _productReferenceRepository;

        // constructor
        public ProductItemController(IProductItemRepository productItemRepository, IProductReferenceRepository productReferenceRepository)
        {
            _productItemRepository = productItemRepository;
            _productReferenceRepository = productReferenceRepository;
        }

        // GET all the product items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItemDto>>> GetProductItems()
        {
            try
            {
                var productItems = await _productItemRepository.GetProductItems();
                var productReferences = await _productReferenceRepository.GetProductReferences();

                if (productItems == null || productItems.ToList().Count() == 0 || productReferences == null || productReferences.ToList().Count() == 0)
                {
                    return NotFound();
                }
                else
                {
                    var productItemsDtos = productItems.ConvertToDto(productReferences);
                    return Ok(productItemsDtos);

                }
            }
            catch (Exception ex)
            {
                // If an exception occurs return the status code 500 with a error message
                return StatusCode(
                    StatusCodes.Status500InternalServerError, "Could not get the product items from the database"
                );
            }
        }

        // GET all the searches for items
        [HttpGet("Search/{query}")]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductItemsSearchResults(string query)
        {
            try
            {
                // Get the product searches of every table type

                var productItems = await _productItemRepository.GetProductItemsBySearchQuery(query);
                var productReferences = await _productReferenceRepository.GetProductReferences();
                if (productItems == null || productItems.ToList().Count() == 0 || productReferences == null || productReferences.ToList().Count() == 0)
                {
                    return NotFound();
                }
                else
                {
                    var productItemsDtos = productItems.ConvertToDto(productReferences);
                    return Ok(productItemsDtos);
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

        // GET single product item by ID with children tables
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductItemDto>> GetProductItem(int id)
        {
            try
            {
                var productItem = await _productItemRepository.GetProductItemById(id);
                var productReferenceId = productItem.ProductReferenceId;

                if (productReferenceId == null || productItem == null)
                {
                    return NotFound();
                }

                var productReference = await _productReferenceRepository.GetProductReferenceById(productReferenceId);

                if (productReference == null)
                {
                    return BadRequest();
                }

                var productItemDto = productItem.ConvertToDto(productReference);
                return Ok(productItemDto);

            }
            catch (Exception ex)
            {
                // If an exception occurs return the status code 500 with a error message
                return StatusCode(
                    StatusCodes.Status500InternalServerError, "Could not get the product item data from the database"
                );
            }
        }

        // POST new product item
        [HttpPost]
        public ActionResult CreateProductItem(ProductItemModel newProductItem)
        {
            try
            {
                var productReferenceId = newProductItem.ProductReference.Id;
                // create product item
                var createdProductItem = _productItemRepository.CreateProductItem(newProductItem, productReferenceId);

                if (createdProductItem == null)
                {
                    return NotFound();
                }

                return CreatedAtAction(nameof(CreateProductItem), createdProductItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE product items and its related table items
        [HttpDelete("{id}")]
        public ActionResult DeleteProductItem(int id)
        {
            try
            {
                _productItemRepository.DeleteProductItemById(id);
                return Ok();

            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT update product item
        [HttpPut("{id}")]
        public ActionResult UpdateProductItem(int id, ProductItemModel newProductItem)
        {
            try
            {
                _productItemRepository.UpdateProductItemById(id, newProductItem);
                return Ok();

            }
            catch (InvalidOperationException ex)
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
