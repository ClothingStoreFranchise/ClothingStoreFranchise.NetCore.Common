using System;
using Newtonsoft.Json;

namespace ClothingStoreFranchise.NetCore.Common.Events.Impl
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            EventId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            EventId = id;
            CreationDate = createDate;
        }

        [JsonProperty]
        public Guid EventId { get; private set; }

        [JsonProperty]
        public DateTime CreationDate { get; private set; }
    }
}
