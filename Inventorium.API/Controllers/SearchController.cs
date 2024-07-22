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

        
    }
}
