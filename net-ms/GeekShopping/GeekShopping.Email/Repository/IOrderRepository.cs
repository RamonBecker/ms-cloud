using GeekShopping.Email.Model;

namespace GeekShopping.OrderAPI.Repository
{
    public interface IOrderRepository
    {
         
        Task UpdateOrderPaymentStatus(long orderHeaderId, bool paid);
    }
}
