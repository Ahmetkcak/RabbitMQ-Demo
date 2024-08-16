namespace WebApi.Services;

public interface IMessageProducer
{
    public void SendMessaging<T>(T message);
}
