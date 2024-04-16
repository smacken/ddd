using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace DomainDrivenArchitecture;

public class DomainRules
{
    public readonly IObjectProvider<IType> Entities = Types()
        .That()
        .AreAssignableTo("Entity")
        .As("Entities");

    public readonly IObjectProvider<IType> ValueObjects = Types()
        .That()
        .AreAssignableTo("ValueObject")
        .As("Value Objects");

    public readonly IObjectProvider<IType> Aggregates = Types()
        .That()
        .ImplementInterface("IAggregateRoot")
        .As("Aggregates");

    public readonly IObjectProvider<IType> DomainServices = Types()
        .That()
        .ImplementInterface("IDomainService")
        .As("Services");

    public readonly IObjectProvider<IType> Repositories = Types()
        .That()
        .ImplementInterface("IRepository")
        .As("Repositories");

    public readonly IObjectProvider<IType> Specifications = Types()
        .That()
        .ImplementInterface("ISpecification")
        .As("Specifications");

    public readonly IObjectProvider<IType> DomainEvents = Types()
        .That()
        .ImplementInterface("IDomainEvent")
        .As("Domain Events");

    public List<IArchRule> DomainRuleSet =>
        EntityRules
            .Concat(ValueObjectRules)
            .Concat(AggregateRules)
            .Concat(DomainServiceRules)
            .Concat(RepositoryRules)
            .Concat(SpecificationRules)
            .ToList();

    protected List<IArchRule> EntityRules =>
        new()
        {
            Classes()
                .That()
                .Are(Entities)
                .Should()
                .HavePropertyMemberWithName("Id")
                .Because("entities should have a unique identifier")
        };

    protected List<IArchRule> ValueObjectRules =>
        new()
        {
            // Self-contained: A Value Object should be a simple object that does not have any associations with entities or services.
            Classes()
                .That()
                .Are(ValueObjects)
                .Should()
                .NotDependOnAny(Entities)
                .Because("value objects should not depend on entities"),
            Classes()
                .That()
                .Are(ValueObjects)
                .Should()
                .NotDependOnAny(DomainServices)
                .Because("value objects should not depend on domain services"),
            Classes()
                .That()
                .Are(ValueObjects)
                .Should()
                .NotDependOnAny(Repositories)
                .Because("value objects should not depend on repositories"),
            Classes()
                .That()
                .Are(ValueObjects)
                .Should()
                .NotDependOnAny(Specifications)
                .Because("value objects should not depend on specifications"),
            Classes()
                .That()
                .Are(ValueObjects)
                .Should()
                .NotDependOnAny(DomainEvents)
                .Because("value objects should not depend on domain events"),
            // No Identity: Value Objects do not have an identity field such as an ID that entities typically have.
            Classes()
                .That()
                .Are(ValueObjects)
                .Should()
                .NotHavePropertyMemberWithName("Id")
                .Because("value objects should not have an Id property"),
            // Encapsulation: All the properties of the Value Object should have private setters and can only be changed through a constructor or factory method.
            // Immutability: Once created, a Value Object should not be altered. Any operation that would change the state should instead return a new Value Object.
            Classes()
                .That()
                .Are(ValueObjects)
                .Should()
                .BeImmutable()
                .Because("value objects should have only readonly properties"),
        };

    protected List<IArchRule> AggregateRules =>
        new()
        {
            Classes()
                .That()
                .Are(Aggregates)
                .Should()
                .NotDependOnAny(Aggregates)
                .Because("aggregates should not depend on other aggregates"),
            Classes()
                .That()
                .Are(Aggregates)
                .Should()
                .NotDependOnAny(Repositories)
                .Because("aggregates should not depend on repositories"),
            Classes()
                .That()
                .Are(Aggregates)
                .Should()
                .HaveMemberWithName("Id")
                .Because("aggregates should have an Id property"),
        };

    protected List<IArchRule> DomainServiceRules => new() { };

    protected List<IArchRule> RepositoryRules =>
        new()
        {
            Classes()
                .That()
                .Are(Repositories)
                .Should()
                .HaveNameEndingWith("Repository")
                .Because("repositories should have a name ending with Repository"),
        };

    protected static List<IArchRule> SpecificationRules => new() { };

    public List<EvaluationResult> CheckDomainRules(Architecture architecture)
    {
        List<EvaluationResult> evaluationResults = DomainRuleSet
            .SelectMany(rule => rule.Evaluate(architecture))
            .ToList();
        return evaluationResults;
    }
}
