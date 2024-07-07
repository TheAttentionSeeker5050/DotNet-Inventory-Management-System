using Inventorium.API.Models;
using Inventorium.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventorium.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReferenceController : ControllerBase
    {
        // add readonly context
        private readonly ProductReferenceService _productReferenceService;

        public ProductReferenceController(ProductReferenceService productReferenceService)
        {
            _productReferenceService = productReferenceService;
        }

        // GET all the product references
        [HttpGet]
        public ActionResult<List<ProductReferenceModel>> GetProductReferences()
        {
            var productReferences = _productReferenceService.GetProductReferences();

            if (productReferences == null | productReferences.ToList().Count == 0)
            {
                return NotFound();
            }

            return Ok(productReferences);
        }

        // GET single product reference by ID with children tables
        [HttpGet("{id}")]
        public ActionResult<ProductReferenceModel> GetProductReference(int id)
        {
            var productReference = _productReferenceService.GetProductReferenceById(id);
            if (productReference == null)
            {
                return NotFound();
            }

            return Ok(productReference);
        }

        // POST new product reference
        [HttpPost]
        public ActionResult<ProductReferenceModel> CreateProductReference(ProductReferenceModel newProductReference)
        {
            try
            {
                var productCategoryId = newProductReference.ProductCategory.Id;
                // create product reference
                var createdProductReference = _productReferenceService.CreateProductReference(newProductReference, productCategoryId);

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
                _productReferenceService.DeleteProductReferenceById(id);
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
                _productReferenceService.UpdateProductReferenceById(id, newProductReference);
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
