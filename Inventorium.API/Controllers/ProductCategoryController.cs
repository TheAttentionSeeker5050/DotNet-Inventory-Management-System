using Inventorium.API.Data;
using Inventorium.API.Models;
using Inventorium.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Inventorium.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        // add readonly context
        private readonly ProductCategoryService _productCategoryService;

        // constructor
        public ProductCategoryController(ProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        // GET all the product categories
        [HttpGet]
        public ActionResult<List<ProductCategoryModel>> GetProductCategories()
        {
            var productCategories = _productCategoryService.GetProductCategories();

            if (productCategories == null | productCategories.ToList().Count == 0)
            {
                return NotFound();
            }

            return Ok(productCategories);
        }

        // GET single product category by ID with children tables
        [HttpGet("{id}")]
        public ActionResult<ProductCategoryModel> GetProductCategoryById(int id)
        {
            var productCategory = _productCategoryService.GetProductCategoryById(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return Ok(productCategory);
        }

        // POST new product category
        [HttpPost]
        public ActionResult CreateProductCategory(ProductCategoryModel newProductCategory)
        {
            try
            {
                // create product category
                var createdProductCategory = _productCategoryService.CreateProductCategory(newProductCategory);

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
                _productCategoryService.DeleteProductCategoryById(id);
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
                _productCategoryService.UpdateProductCategoryById(id, newProductCategory);
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
