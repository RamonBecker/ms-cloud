using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _cliente;
        public const string BasePath = "api/v1/product";
        public async Task<IEnumerable<ProductModel>> FindAllProducts()
        {
            var response = await _cliente.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }
        public async Task<ProductModel> FindProductById(long id)
        {
            var response = await _cliente.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
        }
        public Task<ProductModel> CreateProduct(ProductModel model)
        {
            throw new NotImplementedException();
        }
        public Task<ProductModel> UpdateProduct(ProductModel model)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteProduct(long id)
        {
            throw new NotImplementedException();
        }
    }
}
