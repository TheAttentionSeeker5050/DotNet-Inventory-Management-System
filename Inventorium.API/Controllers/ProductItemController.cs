using Inventorium.API.Models;
using Inventorium.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventorium.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        // add readonly context
        private readonly ProductItemService _productItemService;

        // constructor
        public ProductItemController(ProductItemService productItemService)
        {
            _productItemService = productItemService;
        }

        // GET all the product items
        [HttpGet]
        public ActionResult<List<ProductItemModel>> GetProductItems()
        {
            var productItems = _productItemService.GetProductItems();

            if (productItems == null | productItems.ToList().Count == 0)
            {
                return NotFound();
            }

            return Ok(productItems);
        }

        // GET single product item by ID with children tables
        [HttpGet("{id}")]
        public ActionResult<ProductItemModel> GetProductItem(int id)
        {
            var productItem = _productItemService.GetProductItemById(id);
            if (productItem == null)
            {
                return NotFound();
            }

            return Ok(productItem);
        }

        // POST new product item
        [HttpPost]
        public ActionResult CreateProductItem(ProductItemModel newProductItem)
        {
            try
            {
                var productReferenceId = newProductItem.ProductReference.Id;
                // create product item
                var createdProductItem = _productItemService.CreateProductItem(newProductItem, productReferenceId);

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
                _productItemService.DeleteProductItemById(id);
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
                _productItemService.UpdateProductItemById(id, newProductItem);
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
