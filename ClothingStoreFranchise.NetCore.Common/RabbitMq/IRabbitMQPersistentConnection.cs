using RabbitMQ.Client;
using System;

namespace ClothingStoreFranchise.NetCore.Common.RabbitMq
{
    public interface IRabbitMQPersistentConnection
        : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
