using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Slices;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using DomainDriven;
using DomainDrivenArchitecture;
using DomainDrivenSample.SalesAndCatalog.Aggregates;
using DomainDrivenSample.SalesAndCatalog.Entities;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchitectureTests
{
    public class SalesAndCatalogTests
    {
        private readonly static Architecture Architecture = new ArchLoader()
            .LoadAssemblies(typeof(Book).Assembly, typeof(Entity).Assembly)
            .Build();
        private readonly DomainRules _domainRules;

        public SalesAndCatalogTests()
        {
            _domainRules = new DomainRules();
        }

        [Fact]
        public void DomainRulesShouldBeRespected()
        {
            _domainRules.CheckDomainRules(Architecture);
            foreach (IArchRule rule in _domainRules.DomainRuleSet)
            {
                rule.Check(Architecture);
            }
        }

        [Fact]
        public void EntitiesShouldHaveAnIdProperty()
        {
            IArchRule entitiesShouldHaveAnIdProperty = Classes()
                .That()
                .Are(_domainRules.Entities)
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
                .Are(_domainRules.ValueObjects)
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
                .Are(_domainRules.Aggregates)
                .Should()
                .NotDependOnAny(_domainRules.DomainServices)
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
                .Are(_domainRules.Entities)
                .Should()
                .NotDependOnAny(_domainRules.DomainServices)
                .Because("entities should not depend on services");

            IArchRule aggregatesShouldNotDependOnServices = Types()
                .That()
                .Are(_domainRules.Aggregates)
                .Should()
                .NotDependOnAny(_domainRules.DomainServices)
                .Because("aggregates should not depend on services");

            entitiesShouldNotDependOnServices.Check(Architecture);
            aggregatesShouldNotDependOnServices.Check(Architecture);
        }
    }
}
