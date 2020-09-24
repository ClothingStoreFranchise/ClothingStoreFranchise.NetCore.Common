using RawRabbit.Configuration;

namespace ClothingStoreFranchise.NetCore.Common.RabbitMq.Impl
{
    public class RabbitMqOptions : RawRabbitConfiguration
    {
        public string Namespace { get; set; }
        public int Retries { get; set; }
        public int RetryInterval { get; set; }
    }
}
