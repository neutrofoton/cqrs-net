﻿using Confluent.Kafka;
using CQRS.Core.Events.Consumers;
using CQRS.Core.Events;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CQRS.Core.Events.Handlers;
using System.Text.Json.Serialization;

namespace CQRS.Core.Kafka.Events.Consumers
{
    public class KafkaEventConsumer : EventConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly JsonConverter<EventMessage> _jsonConverter;
        public KafkaEventConsumer(
            IEventListenerHandler eventHandler,
            IOptions<ConsumerConfig> config,
            JsonConverter<EventMessage> jsonConverter
            ) : base(eventHandler)
        {
            _config = config.Value;
            _jsonConverter = jsonConverter;
        }

        public override void Consume(string topic)
        {
            using var consumer = new ConsumerBuilder<string, string>(_config)
                    .SetKeyDeserializer(Deserializers.Utf8)
                    .SetValueDeserializer(Deserializers.Utf8)
                    .Build();

            consumer.Subscribe(topic);

            while (true)
            {
                var consumeResult = consumer.Consume();

                if (consumeResult?.Message == null) continue;

                var options = new JsonSerializerOptions { Converters = { _jsonConverter } };
                var @event = JsonSerializer.Deserialize<EventMessage>(consumeResult.Message.Value, options);

                ExecuteEventHandler(@event);


                consumer.Commit(consumeResult);
            }
        }
    }
}
