
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.Email.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private const string EXCHANGE_NAME = "FanoutPaymentUpdateExchange";
        private string _queueName = "";


        public RabbitMQPaymentConsumer(OrderRepository repository)
        {
            _repository = repository;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };


            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(_queueName, EXCHANGE_NAME, "");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (chanel, evet) =>
            {
                var content = Encoding.UTF8.GetString(evet.Body.ToArray());
                UpdatePaymentResultVO vo = JsonSerializer.Deserialize<UpdatePaymentResultVO>(content);
                UpdatePaymentStatus(vo).GetAwaiter().GetResult();
                _channel.BasicAck(evet.DeliveryTag, false);
            };

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task UpdatePaymentStatus(UpdatePaymentResultVO vo)
        {
            try
            {
                await _repository.UpdateOrderPaymentStatus(vo.OrderId, vo.Status);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
