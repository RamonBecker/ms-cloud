using GeekShopping.MessageBus;

namespace GeekShopping.PaymentAPIAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
