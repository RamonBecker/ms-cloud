using AutoMapper;
using GeekShopping.CouponApi.Model.Context;
using GeekShopping.CouponAPI.Data.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponAPI.Repository
{
	public class CouponRepository : ICouponRepository
	{
		private readonly MySQLContext _context;

		private IMapper _mapper;

		public CouponRepository(MySQLContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<CouponVO> GetCouponByCode(string code)
		{
			var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == code);

			return _mapper.Map<CouponVO>(coupon);
		}
	}
}
