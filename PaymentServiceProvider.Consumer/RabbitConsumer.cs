using PaymentServiceProvider.Application.Interfaces.Transaction;
using PaymentServiceProvider.Domain.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace PaymentServiceProvider.Consumer;
public static class RabbitConsumer
{
    public static void Consume(ICreateTransactionUseCase createTransactionUseCase)
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

        ListenQueuePedidoAguardandoPagamento(conn, createTransactionUseCase);
        Console.WriteLine("Iniciando consumer");
    }

    private static void ListenQueuePedidoAguardandoPagamento(IConnection conn, ICreateTransactionUseCase createTransactionUseCase)
    {
        IModel channel = conn.CreateModel();
        channel.ExchangeDeclare(exchange: "exchange_pedido_aguardando_pagamento", type: ExchangeType.Direct, durable: false, autoDelete: false, null);
        channel.QueueDeclare(queue: "queue_pedido_aguardando_pagamento_1", durable: true, exclusive: false, autoDelete: false, null);
        channel.QueueBind(queue: "queue_pedido_aguardando_pagamento_1", exchange: "exchange_pedido_aguardando_pagamento", routingKey: "key_default", null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (ch, ea) =>
        {
            var body = ea.Body.ToArray();
            var messageSerialize = Encoding.UTF8.GetString(body);
            var message = JsonSerializer.Deserialize<ConsumerModel>(messageSerialize, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (message.Amount > 0)
                createTransactionUseCase.Execute(message);
            channel.BasicAck(ea.DeliveryTag, false);
            Thread.Sleep(1000);

        };
        string consumerTag = channel.BasicConsume(queue: "queue_pedido_aguardando_pagamento_1", autoAck: false, consumer: consumer);
    }
}


