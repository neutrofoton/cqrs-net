using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Core.Events;

namespace CQRS.Core.Events.Producers
{
   public interface IEventProducer
    {
        Task ProduceAsync<T>(string topic, T @event) where T : EventMessage;
    }
}