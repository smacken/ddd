using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Slices;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using DomainDriven;
using DomainDrivenSample.SalesAndCatalog.Aggregates;
using DomainDrivenSample.SalesAndCatalog.Entities;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchitectureTests
{
    public class SalesAndCatalogTests
    {
        private static readonly Architecture Architecture = new ArchLoader()
            .LoadAssemblies(typeof(Book).Assembly, typeof(Entity).Assembly)
            .Build();

        private readonly IObjectProvider<IType> SalesAndCatalogNamespace = Types()
            .That()
            .HaveNameContaining("SalesAndCatalog")
            .As("SalesAndCatalog Namespace");

        private readonly IObjectProvider<IType> Entities = Types()
            .That()
            .ResideInNamespace("SalesAndCatalog.Entities")
            .As("Entities");

        private readonly IObjectProvider<IType> ValueObjects = Types()
            .That()
            .ResideInNamespace("DomainDrivenSample.SalesAndCatalog.ValueObjects")
            .As("Value Objects");

        private readonly IObjectProvider<IType> Aggregates = Types()
            .That()
            .ImplementInterface("IAggregateRoot")
            .As("Aggregates");

        private readonly IObjectProvider<IType> Services = Types()
            .That()
            .ResideInNamespace("DomainDrivenSample.SalesAndCatalog.Services")
            .As("Services");

        private readonly IObjectProvider<IType> Repositories = Types()
            .That()
            .ImplementInterface("IRepository")
            .As("Repositories");

        private readonly IObjectProvider<IType> Specifications = Types()
            .That()
            .ImplementInterface("ISpecification")
            .As("Specifications");

        private readonly IObjectProvider<Class> DomainEvents = Classes()
            .That()
            .ImplementInterface("IDomainEvent")
            .As("Domain Events");

        [Fact]
        public void EntitiesShouldHaveAnIdProperty()
        {
            IArchRule entitiesShouldHaveAnIdProperty = Classes()
                .That()
                .Are(Entities)
                .Should()
                .HavePropertyMemberWithName("Id")
                .Because("entities should have a unique identifier");

            entitiesShouldHaveAnIdProperty.Check(Architecture);
        }

        [Fact]
        public void ValueObjectsShouldBeImmutable()
        {
            IArchRule valueObjectsShouldBeImmutable = Classes()
                .That()
                .Are(ValueObjects)
                .Should()
                .BeImmutable()
                .Because(
                    "value objects should be immutable to ensure thread-safety and conceptual integrity"
                );

            valueObjectsShouldBeImmutable.Check(Architecture);
        }

        [Fact]
        public void AggregatesShouldNotDependOnServices()
        {
            IArchRule aggregatesShouldNotDependOnServices = Types()
                .That()
                .Are(Aggregates)
                .Should()
                .NotDependOnAny(Services)
                .Because(
                    "aggregates should be focused on business rules and not depend on application services"
                );

            aggregatesShouldNotDependOnServices.Check(Architecture);
        }

        [Fact]
        public void ServicesShouldNotBeDependedOnByAggregatesOrEntities()
        {
            IArchRule entitiesShouldNotDependOnServices = Types()
                .That()
                .Are(Entities)
                .Should()
                .NotDependOnAny(Services)
                .Because("entities should not depend on services");

            IArchRule aggregatesShouldNotDependOnServices = Types()
                .That()
                .Are(Aggregates)
                .Should()
                .NotDependOnAny(Services)
                .Because("aggregates should not depend on services");

            entitiesShouldNotDependOnServices.Check(Architecture);
            aggregatesShouldNotDependOnServices.Check(Architecture);
        }
    }
}
