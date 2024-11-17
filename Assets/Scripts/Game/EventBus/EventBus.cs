using System;
using System.Collections.Generic;
using Lessons.Game.Events;
using UnityEngine;

namespace Lessons.Game
{
    public sealed partial class EventBus
    {
        private readonly Dictionary<Type, IEventHandlerCollection> _handlers = new();

        private readonly Queue<IEvent> _events = new();

        private bool _isRunning;

        public void Subscribe<T>(Action<T> handler)
        {
            Type eventType = typeof(T);

            if (!_handlers.ContainsKey(eventType))
            {
                _handlers.Add(eventType, new EventHandlerCollection<T>());
            }
            
            _handlers[eventType].Subscribe(handler);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            Type eventType = typeof(T);

            if (_handlers.TryGetValue(eventType, out IEventHandlerCollection handlers))
            {
                handlers.Unsubscribe(handler);
            }
        }

        public void RaiseEvent<T>(T evt) where T : IEvent
        {
            if (_isRunning)
            {
                _events.Enqueue(evt);
                return;
            }
            
            _isRunning = true;
            
            Type eventType = evt.GetType();
            Debug.Log(eventType);

            if (!_handlers.TryGetValue(eventType, out var handlers))
            {
                Debug.Log($"<color=red>No subscribers found in: {eventType}</color>");
                return;
            }

            handlers.RaiseEvent(evt);

            _isRunning = false;

            if (_events.TryDequeue(out var otherEvent))
            {
                RaiseEvent(otherEvent);
            }
        }
        
        private interface IEventHandlerCollection
        {
            public void Subscribe(Delegate handler);
            
            public void Unsubscribe(Delegate handler);
            
            public void RaiseEvent<T>(T evt);
        }
    }
}