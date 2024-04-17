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
            services.AddTransient(typeof(IRepository<>), typeof(InMemoryRepository<>));
            _serviceProvider = services.BuildServiceProvider();
            DomainEvents.Init(_serviceProvider);
        }

        public IServiceProvider ServiceProvider => _serviceProvider;

        public void Dispose()
        {
            DomainEvents.ClearCallbacks();
        }
    }
}
