using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using PaymentServiceProvider.Domain.Entities;

namespace PaymentServiceProvider.Producer;

public static class RabbitProducer
{
    public static void PublishMessage(object messageJson, PaymentStatus paymentStatus)
    {
        ConnectionFactory factory = new()
        {
            UserName = "twlvsbxr",
            Password = "vbeQt4hymocx2OG3lCzR4t6qI0OTRjKA",
            VirtualHost = "twlvsbxr",
            HostName = "fish-01.rmq.cloudamqp.com",
            Port = 5672
        };
        IConnection conn = factory.CreateConnection();

        switch (paymentStatus)
        {
            case PaymentStatus.AguardandoPagamento:
                break;
            case PaymentStatus.Pago:
                PublishMessageQueuePedidoPago(messageJson, conn);
                break;
            case PaymentStatus.Cancelado:
                PublishMessageQueuePedidoCancelado  (messageJson, conn);
                break;
            default:
                break;
        }
    }

    private static void PublishMessageQueuePedidoPago(object messageJson, IConnection conn)
    {
        IModel channel = conn.CreateModel();
        channel.ExchangeDeclare(exchange: "exchange_pedido_pago", type: ExchangeType.Fanout, durable: false, autoDelete: false, null);
        var messageJsonSerialize = JsonSerializer.Serialize(messageJson);
        var body = Encoding.UTF8.GetBytes(messageJsonSerialize);
        channel.BasicPublish(exchange: "exchange_pedido_pago", routingKey: "key_default", mandatory: false, null, body);
    }

    private static void PublishMessageQueuePedidoCancelado(object messageJson, IConnection conn)
    {
        IModel channel = conn.CreateModel();
        channel.ExchangeDeclare(exchange: "exchange_pedido_cancelado", type: ExchangeType.Fanout, durable: false, autoDelete: false, null);
        var messageJsonSerialize = JsonSerializer.Serialize(messageJson);
        var body = Encoding.UTF8.GetBytes(messageJsonSerialize);
        channel.BasicPublish(exchange: "exchange_pedido_cancelado", routingKey: "key_default", mandatory: false, null, body);
    }
}
