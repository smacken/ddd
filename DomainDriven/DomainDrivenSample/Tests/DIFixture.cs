using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenSample.Tests
{
    public class DIFixture : IDisposable
    {
        private readonly IServiceProvider _serviceProvider;

        public DIFixture()
        {
            var services = new ServiceCollection();
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
            return services;
        }
    }
}
