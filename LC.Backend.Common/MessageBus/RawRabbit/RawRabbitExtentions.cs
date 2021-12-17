﻿using LC.Backend.Common.Commands;
using LC.Backend.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using IBusClient = RawRabbit.IBusClient;

namespace LC.Backend.Common.MessageBus.RawRabbit
{
    public static class RawRabbitExtentions
    {
        private static string GetQueueName<T>()
           => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
        public static void AddCommandHandler<TCommand>(this IBusClient bus, ICommandHandler<TCommand> handler) where TCommand : ICommand
           => bus.SubscribeAsync<TCommand>(async (msg, ctx) => await handler.HandleAsync(msg),
               config => config.WithQueue(q => q.WithName(GetQueueName<TCommand>())));

        public static void AddEventHandler<TEvent>(this IBusClient bus, IEventHandler<TEvent> handler) where TEvent : IEvent
            => bus.SubscribeAsync<TEvent>(async (msg, ctx) => await handler.HandleAsync(msg),
                cfg => cfg.WithQueue(q => q.WithName(GetQueueName<TEvent>())));

        public static IServiceProvider SubscribeToCommand<TCommand>(this IServiceProvider provider) where TCommand : ICommand
        {
            var handler = provider.GetService<ICommandHandler<TCommand>>();

            return SubscribeToCommand(provider, handler);
        }

        public static IServiceProvider SubscribeToCommand<TCommand>(this IServiceProvider provider, ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            var bus = provider.GetService<IBusClient>();
            bus.AddCommandHandler(handler);
            return provider;
        }

        public static IServiceProvider SubscribeToEvent<TEvent>(this IServiceProvider provider) where TEvent : IEvent
        {
            var handler = provider.GetService<IEventHandler<TEvent>>();
            return SubscribeToEvent(provider, handler);
        }

        public static IServiceProvider SubscribeToEvent<TEvent>(IServiceProvider provider, IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            var bus = provider.GetService<IBusClient>();
            bus.AddEventHandler(handler);
            return provider;
        }
    }
}
