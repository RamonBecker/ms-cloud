using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;
using System.Reflection;

namespace GeekShopping.Web.Services
{
    public class CartService : ICartService
    {

        private readonly HttpClient _cliente;
        public const string BasePath = "api/v1/cart";

        public CartService(HttpClient cliente)
        {
            _cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
        }

  

        public async Task<bool> ApplyCoupon(CartViewModel cart, string token)
        {
			_cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _cliente.PostAsJson($"{BasePath}/apply-coupon", cart);

			if (response.IsSuccessStatusCode)
				return await response.ReadContentAs<bool>();
			else
				throw new Exception("Something went wrong when calling API");
		}

		public async Task<bool> RemoveCoupon(string userId, string token)
		{
			_cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _cliente.DeleteAsync($"{BasePath}/remove-coupon/{userId}");

			if (response.IsSuccessStatusCode)
				return await response.ReadContentAs<bool>();
			else
				throw new Exception("Something went wrong when calling API");
		}


		public async Task<CartHeaderViewModel> Checkout(CartHeaderViewModel model, string token)
        {
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _cliente.PostAsJson($"{BasePath}/checkout", model);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CartHeaderViewModel>();
            else
                throw new Exception("Something went wrong when calling API");
        }

        public async Task<bool> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<CartViewModel> FindCartByUserId(string userId, string token)
        {
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _cliente.GetAsync($"{BasePath}/find-cart/{userId}");
            return await response.ReadContentAs<CartViewModel>();
        }

		public async Task<CartViewModel> AddItemToCart(CartViewModel model, string token)
		{
			_cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _cliente.PostAsJson($"{BasePath}/add-cart", model);

			if (response.IsSuccessStatusCode)
				return await response.ReadContentAs<CartViewModel>();
			else
				throw new Exception("Something went wrong when calling API");
		}

		public async Task<bool> RemoveFromCart(long cartId, string token)
        {
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _cliente.DeleteAsync($"{BasePath}/remove-cart/{cartId}");

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else
                throw new Exception("Something went wrong when calling API");
        }

        public async Task<CartViewModel> UpdateCart(CartViewModel model, string token)
        {
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _cliente.PuttAsJson($"{BasePath}/update-cart", model);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CartViewModel>();
            else
                throw new Exception("Something went wrong when calling API");
        }
    }
}
