﻿
using GeekShopping.CartAPI.Data.ValueObjects;

namespace GeekShopping.CartAPI.Repository
{
	public interface ICouponRepository
	{
		Task<CouponVO> GetCoupon(string code, string token);
	}
}
