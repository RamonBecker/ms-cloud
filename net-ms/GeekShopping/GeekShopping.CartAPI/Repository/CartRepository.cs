using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository
{
	public class CartRepository : ICartRepository
	{
		private readonly MySQLContext _context;

		private IMapper _mapper;

		public CartRepository(MySQLContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<bool> ApplyCoupon(string userId, string couponCode)
		{
			var header = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);

			if (header != null)
			{
				header.CouponCode = couponCode;
				_context.CartHeaders.Update(header);
				await _context.SaveChangesAsync();

				return true;
			}

			return false;
		}

		public async Task<bool> RemoveCoupon(string userId)
		{
			var header = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);

			if (header != null)
			{
				header.CouponCode = string.Empty;
				_context.CartHeaders.Update(header);
				await _context.SaveChangesAsync();

				return true;
			}

			return false;
		}

		public async Task<bool> ClearCart(string userId)
		{
			var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);

			if (cartHeader != null)
			{
				var cartsDetails = _context.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id);

				_context.CartDetails.RemoveRange(cartsDetails);
				_context.CartHeaders.Remove(cartHeader);

				await _context.SaveChangesAsync();

				return true;
			}

			return false;
		}

		public async Task<CartVO> FindCartByUserId(string userId)
		{

			Cart cart = new()
			{
				CartHeader = await _context.CartHeaders
				.FirstOrDefaultAsync(c => c.UserId == userId) ?? new CartHeader(),
			};
			cart.CartDetails = _context.CartDetails
				.Where(c => c.CartHeaderId == cart.CartHeader.Id)
					.Include(c => c.Product);
			return _mapper.Map<CartVO>(cart);

		}
		public async Task<bool> RemoveFromCart(long cartDetailsId)
		{
			try
			{
				var cartDetail = await _context.CartDetails.FirstOrDefaultAsync(c => c.Id == cartDetailsId);


				var total = _context.CartDetails.Where(c => c.CartHeaderId == cartDetail.CartHeaderId).Count();

				_context.CartDetails.Remove(cartDetail);

				if (total == 1)
				{
					var cartHeaderToRemove = await _context.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);

					_context.CartHeaders.Remove(cartHeaderToRemove);
				}

				await _context.SaveChangesAsync();

				return true;
			}
			catch
			{

				return false;
			}
		}

		public async Task<CartVO> SaveOrUpdateCart(CartVO vo)
		{
			Cart cart = _mapper.Map<Cart>(vo);

			var product = await _context.Products.FirstOrDefaultAsync(p =>
																		p.Id == vo.CartDetails.FirstOrDefault().ProductId);
			if (product == null)
			{
				_context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
				await _context.SaveChangesAsync();
			}


			var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId);

			if (cartHeader == null)
			{
				_context.CartHeaders.Add(cart.CartHeader);
				await _context.SaveChangesAsync();

				var findCartDetail = cart.CartDetails.FirstOrDefault();

				if (findCartDetail != null)
				{
					findCartDetail.CartHeaderId = cart.CartHeader.Id;
					findCartDetail.Product = null;
					_context.CartDetails.Add(findCartDetail);
					await _context.SaveChangesAsync();
				}
			}
			else
			{
				var findCartDetail = cart.CartDetails.FirstOrDefault();

				var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
					p => p.ProductId == findCartDetail.ProductId &&
					p.CartHeaderId == cartHeader.Id
					);


				if (cartDetail == null)
				{

					if (findCartDetail != null)
					{
						findCartDetail.CartHeaderId = cartHeader.Id;
						findCartDetail.Product = null;
						_context.CartDetails.Add(findCartDetail);
						await _context.SaveChangesAsync();
					}
				}
				else
				{

					if (findCartDetail != null)
					{

						findCartDetail.Product = null;
						findCartDetail.Count += cartDetail.Count;
						findCartDetail.Id = cartDetail.Id;
						findCartDetail.CartHeaderId = cartDetail.CartHeaderId;
						_context.CartDetails.Update(findCartDetail);
						await _context.SaveChangesAsync();
					}
				}
			}

			return _mapper.Map<CartVO>(cart);
		}
	}
}
