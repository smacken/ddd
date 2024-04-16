// <copyright file="DomainEvents.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DomainDriven
{
    using System.Diagnostics;
    using Microsoft.Extensions.DependencyInjection;

    public static class DomainEvents
    {
        [ThreadStatic] // so that each thread has its own callbacks
        private static List<Delegate> actions = new List<Delegate>();

        private static IServiceProvider? container;

        public static void Init(IServiceProvider container)
        {
            DomainEvents.container =
                container ?? throw new ArgumentNullException(nameof(container));
        }

        /// <summary>
        /// Registers a callback for the given domain event, used for testing only.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        public static void Register<T>(Action<T> callback)
            where T : DomainEvent => actions.Add(callback);

        /// <summary>
        /// Clears callbacks passed to Register on the current thread.
        /// </summary>
        public static void ClearCallbacks() => actions.Clear();

        /// <summary>
        /// Raises the given domain event.
        /// </summary>
        /// <typeparam name="T">The type of domain event to raise.</typeparam>
        /// <param name="args">The domain event instance.</param>
        public static async void Raise<T>(T args)
            where T : DomainEvent
        {
            if (container != null)
            {
                IEnumerable<Handles<T>> handlers = new List<Handles<T>>(1);
                try
                {
                    handlers = container.GetServices<Handles<T>>();
                }
                catch
                {
                    Debug.WriteLine("No handlers found for " + typeof(T).Name);
                }

                foreach (Handles<T>? handler in handlers)
                {
                    await handler.Handle(args);
                }
            }

            foreach (Delegate? action in actions)
            {
                if (action is not Action<T>)
                {
                    continue;
                }

                try
                {
                    ((Action<T>)action)(args);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}
