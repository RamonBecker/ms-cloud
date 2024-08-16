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
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            throw new NotImplementedException();
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

                var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    p => p.ProductId == vo.CartDetails.FirstOrDefault().ProductId &&
                    p.CartHeaderId == cartHeader.Id
                    );

                var findCartDetail = cart.CartDetails.FirstOrDefault();

                if (cartDetail == null)
                {

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
