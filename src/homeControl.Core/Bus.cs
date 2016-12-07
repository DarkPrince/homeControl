﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using homeControl.Core.Misc;

namespace homeControl.Core
{
    internal class Bus : IEventPublisher, IEventProcessor
    {
        private readonly IHandler[] _handlers;
        private readonly ConcurrentQueue<IEvent> _queue;


        public Bus(params IHandler[] handlers)
        {
            Guard.DebugAssertArgumentNotNull(handlers, nameof(handlers));
            Guard.DebugAssertArgument(handlers.All(h => h != null), nameof(handlers));

            _handlers = handlers;
            _queue = new ConcurrentQueue<IEvent>();
        }

        public void PublishEvent(IEvent @event)
        {
            Guard.DebugAssertArgumentNotNull(@event, nameof(@event));

            _queue.Enqueue(@event);
        }

        public EventProcessingResult ProcessEvents()
        {
            var eventsProcessed = false;

            IEvent @event;
            while (_queue.TryDequeue(out @event))
            {
                ProcessEvent(@event);
                eventsProcessed = true;
            }

            return eventsProcessed ? EventProcessingResult.Complete : EventProcessingResult.Idle;
        }

        private void ProcessEvent(IEvent @event)
        {
            var suitableHandlers = _handlers.Where(h => h.CanHandle(@event));
            foreach (var handler in suitableHandlers)
            {
                handler.Handle(@event);
            }
        }
    }
}
