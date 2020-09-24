using ClothingStoreFranchise.NetCore.Common.Events;
using ClothingStoreFranchise.NetCore.Common.Events.Impl;
using ClothingStoreFranchise.NetCore.Common.RabbitMq.Impl;
using System;
using System.Collections.Generic;

namespace ClothingStoreFranchise.NetCore.Common.RabbitMq
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        void AddSubscription<TEvent, THandler>()
           where TEvent : IntegrationEvent
           where THandler : IIntegrationEventHandler<TEvent>;

        void RemoveSubscription<TEvent, THandler>()
             where THandler : IIntegrationEventHandler<TEvent>
             where TEvent : IntegrationEvent;

        bool HasSubscriptionsForEvent<TEvent>() where TEvent : IntegrationEvent;
        bool HasSubscriptionsForEvent(string eventName);
        Type GetEventTypeByName(string eventName);
        void Clear();
        //IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        string GetEventKey<TEvent>();
    }
}
