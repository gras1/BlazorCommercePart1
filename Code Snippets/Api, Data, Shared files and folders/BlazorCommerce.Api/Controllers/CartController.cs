using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlazorCommerce.Data;
using BlazorCommerce.Shared;

namespace BlazorCommerce.Api.Controllers
{
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        
        private readonly IRepository<CartMinDto> _cartMinRepository;

        public CartController(ILogger<CategoryController> logger, IRepository<CartMinDto> cartMinRepository)
        {
            _logger = logger;
            _cartMinRepository = cartMinRepository;
        }

        [HttpGet]
        [Route("~/cartmin/{cartid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CartMinDto> GetMin(string cartId)
        {
            CartMinDto cart;
            try
            {
                cart = _cartMinRepository.Get(cartId);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(cart);
        }

        [HttpPost]
        [Route("~/cart/additemtocart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult AddItemToCart([FromBody] AddItemToCartDto cartItem)
        {
            try
            {
                _cartMinRepository.AddItemToCart(cartItem.CartGuid, cartItem.ProductOptionProductInstanceId, cartItem.Quantity);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}