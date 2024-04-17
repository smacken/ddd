# DomainDriven Library for .NET

![DomainDriven](assets/logo.png)
The DomainDriven library is a C# class library designed to encapsulate the key concepts of Domain-Driven Design (DDD) and provide a foundation for implementing rich domain models in enterprise applications. The library aims to facilitate the development of applications following DDD principles by providing base classes, interfaces, and patterns commonly used in DDD.

## Getting Started

### Prerequisites

- .NET Core
- Visual Studio 2019 or later (optional, but recommended for development)

### Installation

To use the DomainDriven library in your project, install it via NuGet package manager:

```shell
    dotnet add package DomainDriven
```

Or, if you're using the Package Manager Console in Visual Studio:

```powershell
    Install-Package DomainDriven
```

### Usage

After installing the package, you can begin using the components in your domain layer. Here's a quick example of defining an entity:

```csharp
using DomainDriven;

public class Product : Entity<long>
{
    public string Name { get; private set; }
    public Money Price { get; private set; }

    // Add methods and domain logic here
}
```

Refer to the [Documentation](#documentation) section for detailed usage instructions and examples.

## Library Structure

The library is organized into several namespaces, each representing key DDD concepts:

- `DomainDriven.Entities`: Base classes and interfaces for entities.
- `DomainDriven.ValueObjects`: Base classes for value objects.
- `DomainDriven.Aggregates`: Base classes for aggregate roots.
- `DomainDriven.Repositories`: Interfaces for repository patterns.
- `DomainDriven.Services`: Interfaces for domain services.
- `DomainDriven.Events`: Base classes and interfaces for domain events.

## Contributing

We welcome contributions from the community! If you would like to contribute to the DomainDriven library, please follow these steps:

1. Fork the repository.
2. Create a feature branch (`git checkout -b feature/AmazingFeature`).
3. Make your changes.
4. Commit your changes (`git commit -m 'Add some AmazingFeature'`).
5. Push to the branch (`git push origin feature/AmazingFeature`).
6. Open a pull request.

Please ensure you write unit tests for your changes where applicable.

## Documentation

For a comprehensive guide to using the DomainDriven library, please refer to the `/docs` directory in this repository or visit our [online documentation](#).

## Sample DDD Project - [Publisher](./DomainDriven/DomainDrivenSample/README.md)

**Bounded Context**
![BoundedContext](assets/boundedcontext.png)

**Sales & Catalog Domain model**
![SalesDomain](assets/salesdomain.png)

## Domain Rules for Architecture Validation

Our library provides a set of domain-driven design architecture validations through DomainRules.cs. This set of rules ensures that your project adheres to the principles of Domain-Driven Design (DDD) and maintains a clean, maintainable architecture.

#### How to Use DomainRules

To leverage DomainRules in your project, follow these steps:

1. Include the Library: Ensure that DomainDrivenArchitecture is referenced in your project. This can be done by adding the library as a dependency in your project file.

2. Load Your Architecture: Use ArchLoader to load your project's assemblies into an Architecture object. This object will be used to apply the domain rules.

3. Apply the Rules: Create a test class that will apply the domain rules to your architecture. You can use any testing framework that you prefer, such as xUnit, NUnit, or MSTest.

4. Run the Tests: Execute the tests to validate your architecture against the domain rules. Any violations will be reported by the test runner, allowing you to make necessary adjustments to your codebase.

5. Integrate into CI/CD: Integrate these tests into your Continuous Integration/Continuous Deployment (CI/CD) pipeline to ensure that architecture rules are enforced with every build.

#### Example Test Case

Here's an example of how you might write a test case using DomainRules:

```csharp
using DomainDriven.DomainDrivenArchitecture;
using ArchUnitNET.Loader;
using ArchUnitNET.Domain;
using Xunit;

public class ArchitectureValidationTests
{
    private static readonly Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(MyRootClass).Assembly).Build();

    [Fact]
    public void ValidateDomainLayer()
    {
        var domainLayerRules = new DomainRules();
        domainLayerRules.ValidateDomainLayer(Architecture);
    }
}
```

#### Available Domain Rules

DomainRules.cs provides a number of predefined rules that reflect common DDD architecture principles, including:

- Aggregate Root Validations: Ensuring that Aggregate Roots have a global identity and are the only point of reference for aggregates.
- Entity Validations: Entities within the domain layer should have an identity and support state changes within a transaction boundary.
- Value Object Validations: Value Objects should be immutable and define equality based on their attributes.
- Service Layer Validations: Services should coordinate tasks and delegate work to entities and value objects.
- Module Cohesion: Components within a module should be cohesive and loosely coupled with components outside the module.

#### Customizing Rules

While DomainRules.cs provides a solid foundation for DDD architecture validation, you might find that you need to
customize or extend these rules to fit the specific needs of your project. You can do this by creating your own rules
following the pattern established in DomainRules.cs and adding them to your test suite.

## Support and Contact

If you need help or have any questions about the DomainDriven library, please [open an issue](https://github.com/your-repo/DomainDriven/issues) in this repository.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
