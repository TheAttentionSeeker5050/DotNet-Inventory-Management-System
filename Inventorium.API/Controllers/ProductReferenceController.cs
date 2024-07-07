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
    public class ProductReferenceController : ControllerBase
    {
        // add readonly context
        private readonly IProductReferenceRepository _productReferenceRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductItemRepository _productItemRepository;



        public ProductReferenceController(IProductReferenceRepository productReferenceRepository, IProductCategoryRepository productCategoryRepository, IProductItemRepository productItemRepository)
        {
            _productReferenceRepository = productReferenceRepository;
            _productCategoryRepository = productCategoryRepository;
            _productItemRepository = productItemRepository;

        }

        // GET all the product references
        [HttpGet]
        public async Task<ActionResult<List<ProductReferenceDto>>> GetProductReferences()
        {
            try
            {
                var productReferences = await _productReferenceRepository.GetProductReferences();
                var productCategories = await _productCategoryRepository.GetProductCategories();

                if (productReferences == null || productReferences.ToList().Count() == 0 || productCategories.ToList().Count() == 0)
                {
                    return NotFound();

                }
                else
                {
                    var productReferencesDtos = productReferences.ConvertToDto(productCategories);
                    return Ok(productReferencesDtos);

                }
            }
            catch (Exception ex)
            {
                // If an exception occurs return the status code 500 with a error message
                return StatusCode(
                    StatusCodes.Status500InternalServerError, "Could not get the product references from the database"
                );
            }
        }

        // GET single product reference by ID with children tables
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReferenceDto>> GetProductReference(int id)
        {
            try
            {
                var productReference = await _productReferenceRepository.GetProductReferenceById(id);
                if (productReference == null)
                {
                    return NotFound();
                }

                var productCategoryId = productReference.ProductCategory.Id;
                if (productCategoryId == null)
                {
                    return BadRequest();
                }

                var productCategory = await _productCategoryRepository.GetProductCategoryById(productCategoryId);
                var productItems = await _productItemRepository.GetProductItems();

                if (productItems.ToList().Count() == 0 || productCategory == null)
                {
                    return NotFound();

                }
                else
                {
                    var productReferenceDto = productReference.ConvertToDto(productCategory, productItems.ToList());
                    return Ok(productReferenceDto);

                }
            }
            catch (Exception ex)
            {
                // If an exception occurs return the status code 500 with a error message
                return StatusCode(
                    StatusCodes.Status500InternalServerError, "Could not get the product reference data from the database"
                );
            }

            /*var productReference = _productReferenceRepository.GetProductReferenceById(id);
            if (productReference == null)
            {
                return NotFound();
            }

            return Ok(productReference);*/
        }

        // POST new product reference
        [HttpPost]
        public ActionResult<ProductReferenceDto> CreateProductReference(ProductReferenceModel newProductReference)
        {
            try
            {
                var productCategoryId = newProductReference.ProductCategory.Id;
                // create product reference
                var createdProductReference = _productReferenceRepository.CreateProductReference(newProductReference, productCategoryId);

                if (createdProductReference == null)
                {
                    return BadRequest();
                }

                return CreatedAtAction(nameof(CreateProductReference), createdProductReference);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE product reference and its related table items
        [HttpDelete("{id}")]
        public ActionResult DeleteProductReference(int id)
        {
            try
            {
                _productReferenceRepository.DeleteProductReferenceById(id);
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

        // PUT update product reference
        [HttpPut("{id}")]
        public ActionResult UpdateProductReference(int id, ProductReferenceModel newProductReference)
        {
            try
            {
                _productReferenceRepository.UpdateProductReferenceById(id, newProductReference);
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
