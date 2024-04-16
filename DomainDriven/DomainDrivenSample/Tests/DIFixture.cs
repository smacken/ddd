using DomainDrivenSample.SalesAndCatalog.DomainEvents;
using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenSample.Tests
{
    public class DIFixture : IDisposable
    {
        private readonly IServiceProvider _serviceProvider;

        public DIFixture()
        {
            ServiceCollection services = new();
            services.AddDomainDriven();
            _serviceProvider = services.BuildServiceProvider();
            DomainEvents.Init(_serviceProvider);
        }

        public IServiceProvider ServiceProvider => _serviceProvider;

        public void Dispose()
        {
            DomainEvents.ClearCallbacks();
        }
    }

    public static class DomainDrivenExtensions
    {
        public static IServiceCollection AddDomainDriven(this IServiceCollection services)
        {
            // services.AddTransient<Handles<DomainEvent>, DomainEventHandle>();
            services.AddTransient<Handles<BookCreatedEvent>, DomainEventHandle<BookCreatedEvent>>();
            return services;
        }
    }
}
