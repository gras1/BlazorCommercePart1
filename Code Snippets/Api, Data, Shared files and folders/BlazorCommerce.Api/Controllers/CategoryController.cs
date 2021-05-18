using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlazorCommerce.Data;
using BlazorCommerce.Shared;

namespace BlazorCommerce.Api.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        
        private readonly IRepository<CategoryDto> _categoryRepository;
        
        private readonly IRepository<CategoryMinDto> _categoryMinRepository;

        public CategoryController(ILogger<CategoryController> logger, IRepository<CategoryDto> categoryRepository, IRepository<CategoryMinDto> categoryMinRepository)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _categoryMinRepository = categoryMinRepository;
        }

        [HttpGet]
        [Route("~/category/siblingcategories/{friendlyUrl}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<CategoryDto>> GetAllSiblingCategories(string friendlyUrl)
        {
            IEnumerable<CategoryDto> categories;
            try
            {
                categories = _categoryRepository.GetAllSiblingCategories(friendlyUrl);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(categories);
        }

        [HttpGet]
        [Route("~/categorymin/siblingcategories/{friendlyUrl}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<CategoryMinDto>> GetMinAllSiblingCategories(string friendlyUrl)
        {
            IEnumerable<CategoryMinDto> categories;
            try
            {
                categories = _categoryMinRepository.GetAllSiblingCategories(friendlyUrl);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(categories);
        }

        [HttpGet]
        [Route("~/category/featuredcategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<CategoryDto>> GetFeaturedCategories()
        {
            IEnumerable<CategoryDto> categories;
            try
            {
                categories = _categoryRepository.GetFeaturedCategories();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(categories);
        }

        [HttpGet]
        [Route("~/categorymin/featuredcategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<CategoryMinDto>> GetMinFeaturedCategories()
        {
            IEnumerable<CategoryMinDto> categories;
            try
            {
                categories = _categoryMinRepository.GetFeaturedCategories();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(categories);
        }

        [HttpGet]
        [Route("~/category/{friendlyUrl}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CategoryDto> Get(string friendlyUrl)
        {
            if (string.IsNullOrWhiteSpace(friendlyUrl))
            {
                return BadRequest();
            }
            CategoryDto category = null;
            try
            {
                category = _categoryRepository.Get(friendlyUrl);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet]
        [Route("~/categorymin/{friendlyUrl}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CategoryMinDto> GetMin(string friendlyUrl)
        {
            if (string.IsNullOrWhiteSpace(friendlyUrl))
            {
                return BadRequest();
            }
            CategoryMinDto category = null;
            try
            {
                category = _categoryMinRepository.Get(friendlyUrl);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet]
        [Route("~/category/bestsellingcategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BestSellingCategoriesDto> GetBestSellingCategoriesProducts()
        {
            BestSellingCategoriesDto bestSellingCategories = null;
            try
            {
                bestSellingCategories = _categoryRepository.GetBestSellingCategoriesProducts();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (bestSellingCategories == null)
            {
                return NotFound();
            }
            return Ok(bestSellingCategories);
        }

        [HttpGet]
        [Route("~/category/menucategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<MenuCategoryDto>> GetMenuCategories()
        {
            IEnumerable<MenuCategoryDto> GetMenuCategories = null;
            try
            {
                GetMenuCategories = _categoryRepository.GetMenuCategories();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (GetMenuCategories == null)
            {
                return NotFound();
            }
            return Ok(GetMenuCategories);
        }
    }
}
