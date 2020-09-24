using ClothingStoreFranchise.NetCore.Common.Events.Impl;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Common.Events
{
    public interface IIntegrationEventHandler<in TEvent> : IIntegrationEventHandler
        where TEvent : IntegrationEvent
    {
        Task HandleAsync(TEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
