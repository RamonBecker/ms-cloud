namespace GeekShopping.MessageBus
{
    public class BaseMessage
    {
        public long Id { get; set; }

        public DateTime MessageCreate { get; set; }
    }
}
