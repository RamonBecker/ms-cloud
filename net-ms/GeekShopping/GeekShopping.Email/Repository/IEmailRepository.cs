using GeekShopping.Email.Messages;
using GeekShopping.Email.Model;

namespace GeekShopping.OrderAPI.Repository
{
    public interface IEmailRepository
    {
         
        Task LogEmail(UpdatePaymentResultMessage message);
    }
}
