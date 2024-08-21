using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _cliente;
        public const string BasePath = "api/v1/product";

        public ProductService(HttpClient cliente)
        {
            _cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
        }

        public async Task<IEnumerable<ProductViewModel>> FindAllProducts(string token)
        {

            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _cliente.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductViewModel>>();
        }
        public async Task<ProductViewModel> FindProductById(long id, string token)
        {
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _cliente.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductViewModel>();
        }
        public async Task<ProductViewModel> CreateProduct([FromBody] ProductViewModel model, string token)
        {
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _cliente.PostAsJson(BasePath, model);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductViewModel>();
            else
                throw new Exception("Something went wrong when calling API");
        }
        public async Task<ProductViewModel> UpdateProduct([FromBody] ProductViewModel model, string token)
        {
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _cliente.PuttAsJson(BasePath, model);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductViewModel>();
            else
                throw new Exception("Something went wrong when calling API");
        }
        public async Task<bool> DeleteProduct(long id, string token)
        {
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _cliente.DeleteAsync($"{BasePath}/{id}");

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else
                throw new Exception("Something went wrong when calling API");
        }
    }
}
