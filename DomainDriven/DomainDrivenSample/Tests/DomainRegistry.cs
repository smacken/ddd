using DomainDrivenSample.SalesAndCatalog.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using SolutionName.DomainDrivenSample.SalesAndCatalog.Handlers;

namespace DomainDrivenSample.Tests
{
    public static class DomainRegistry
    {
        public static IServiceCollection AddDomainDriven(this IServiceCollection services)
        {
            services.AddTransient<Handles<BookCreatedEvent>, DomainEventHandle<BookCreatedEvent>>();
            services.AddTransient<Handles<BookSubmittedEvent>, BookSubmissionOrchestrator>();
            services.AddTransient<Handles<BookNegotiationEvent>, BookNegotiationHandler>();
            return services;
        }
    }
}
