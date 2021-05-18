using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlazorCommerce.Data;
using BlazorCommerce.Shared;

namespace BlazorCommerce.Api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        
        private readonly IRepository<ProductMinDto> _productMinRepository;
        
        private readonly IRepository<ProductDto> _productRepository;

        public ProductController(ILogger<CategoryController> logger, IRepository<ProductMinDto> productMinRepository, IRepository<ProductDto> productRepository)
        {
            _logger = logger;
            _productMinRepository = productMinRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("~/product/trendingproducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TrendingProductsDto> GetTrendingProducts()
        {
            TrendingProductsDto trendingProducts;
            try
            {
                trendingProducts = _productMinRepository.GetTrendingProducts();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(trendingProducts);
        }

        [HttpGet]
        [Route("~/product/{friendlyUrl}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDto> Get(string friendlyUrl)
        {
            ProductDto product;
            try
            {
                product = _productRepository.Get(friendlyUrl);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("~/product/categoryproducts/{leafCategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<CategoryProductDto>> GetProductsByLeafCategoryId(int leafCategoryId)
        {
            IEnumerable<CategoryProductDto> categoryProducts;
            try
            {
                categoryProducts = _productMinRepository.GetProductsByLeafCategoryId(leafCategoryId);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(categoryProducts);
        }

        [HttpPost]
        [Route("~/product/incrementnumberoftimesviewed/{friendlyUrl}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult IncrementNumberOfTimesViewed(string friendlyUrl)
        {
            try
            {
                _productRepository.IncrementNumberOfTimesViewed(friendlyUrl);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}