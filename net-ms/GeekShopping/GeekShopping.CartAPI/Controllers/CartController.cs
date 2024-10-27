using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Messages;
using GeekShopping.CartAPI.RabbitMQSender;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class CartController : ControllerBase
	{
		private ICartRepository _repository;

		private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartRepository repository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _rabbitMQMessageSender = rabbitMQMessageSender ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
        }

        [HttpGet("find-cart/{id}")]
		public async Task<ActionResult<CartVO>> FindById(string id)
		{
			var cart = await _repository.FindCartByUserId(id);

			if (cart == null)
				return NotFound();

			return base.Ok(cart);
		}

		[HttpPost("add-cart")]
		public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
		{
			var cart = await _repository.SaveOrUpdateCart(vo);

			if (cart == null)
				return NotFound();

			return base.Ok(cart);
		}

		[HttpPut("update-cart/{id}")]
		public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
		{
			var cart = await _repository.SaveOrUpdateCart(vo);

			if (cart == null)
				return NotFound();

			return base.Ok(cart);
		}

		[HttpDelete("remove-cart/{id}")]
		public async Task<ActionResult<CartVO>> RemoveCart(int id)
		{
			var status = await _repository.RemoveFromCart(id);

			if (!status)
				return BadRequest();

			return base.Ok(status);
		}

		[HttpPost("apply-coupon")]
		public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vo)
		{

			var userId = vo.CartHeader.UserId;
			var couponCode = vo.CartHeader.CouponCode;

			var status = await _repository.ApplyCoupon(userId, couponCode);

			if (!status)
				return NotFound();

			return base.Ok(status);
		}

		[HttpDelete("remove-coupon/{userId}")]
		public async Task<ActionResult<CartVO>> RemoveCoupon(string userId)
		{
			var status = await _repository.RemoveCoupon(userId);

			if (!status)
				return NotFound();

			return base.Ok(status);
		}


        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo)
        {
			if (vo?.UserId == null) return BadRequest();



            var cart = await _repository.FindCartByUserId(vo.UserId);

            if (cart == null)
                return NotFound();

			vo.CartDetails = cart.CartDetails;
			vo.DateTime = DateTime.Now;


			_rabbitMQMessageSender.SendMessage(vo, "checkoutqueue");

            return base.Ok(vo);
        }
    }
}
