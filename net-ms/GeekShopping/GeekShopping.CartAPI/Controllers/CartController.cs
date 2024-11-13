using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Repository;
using GeekShopping.OrderAPI.Data.ValueObjects;
using GeekShopping.OrderAPI.Messages;
using GeekShopping.OrderAPI.RabbitMQSender;
using GeekShopping.OrderAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.OrderAPI.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class CartController : ControllerBase
	{
		private ICartRepository _cartRepository;
		private ICouponRepository _couponRepository;
		private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartRepository cartRepository, ICouponRepository couponRepository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
            _rabbitMQMessageSender = rabbitMQMessageSender ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
        }

        [HttpGet("find-cart/{id}")]
		public async Task<ActionResult<CartVO>> FindById(string id)
		{
			var cart = await _cartRepository.FindCartByUserId(id);

			if (cart == null)
				return NotFound();

			return base.Ok(cart);
		}

		[HttpPost("add-cart")]
		public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
		{
			var cart = await _cartRepository.SaveOrUpdateCart(vo);

			if (cart == null)
				return NotFound();

			return base.Ok(cart);
		}

		[HttpPut("update-cart")]
		public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
		{
			var cart = await _cartRepository.SaveOrUpdateCart(vo);

			if (cart == null)
				return NotFound();

			return base.Ok(cart);
		}

		[HttpDelete("remove-cart/{id}")]
		public async Task<ActionResult<CartVO>> RemoveCart(int id)
		{
			var status = await _cartRepository.RemoveFromCart(id);

			if (!status)
				return BadRequest();

			return base.Ok(status);
		}

		[HttpPost("apply-coupon")]
		public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vo)
		{

			var userId = vo.CartHeader.UserId;
			var couponCode = vo.CartHeader.CouponCode;

			var status = await _cartRepository.ApplyCoupon(userId, couponCode);

			if (!status)
				return NotFound();

			return base.Ok(status);
		}

		[HttpDelete("remove-coupon/{userId}")]
		public async Task<ActionResult<CartVO>> RemoveCoupon(string userId)
		{
			var status = await _cartRepository.RemoveCoupon(userId);

			if (!status)
				return NotFound();

			return base.Ok(status);
		}


        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo)
        {

			var token = Request.Headers["Authorization"].ToString();

			if (vo?.UserId == null) return BadRequest();
            var cart = await _cartRepository.FindCartByUserId(vo.UserId);

            if (cart == null)
                return NotFound();


			if (!string.IsNullOrEmpty(vo.CouponCode))
			{
				var coupon = await _couponRepository.GetCoupon(vo.CouponCode, token);

				if(vo.DiscountAmount != coupon.DiscountAmount)
				{
					return StatusCode(412);
				}

			}


			vo.CartDetails = cart.CartDetails;
			vo.DateTime = DateTime.Now;


			_rabbitMQMessageSender.SendMessage(vo, "checkoutqueue");


			await _cartRepository.ClearCart(vo.UserId);


            return base.Ok(vo);
        }
    }
}
