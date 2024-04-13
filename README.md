# DomainDriven Library for .NET

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

## Sample DDD Project

[Publisher](./DomainDriven/DomainDrivenSample/README.md)

## Support and Contact

If you need help or have any questions about the DomainDriven library, please [open an issue](https://github.com/your-repo/DomainDriven/issues) in this repository.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
