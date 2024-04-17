namespace DomainDrivenSample.SalesAndCatalog.Services
{
    using System;
    using System.Threading.Tasks;
    using DomainDriven;
    using DomainDrivenSample.SalesAndCatalog.Aggregates;
    using DomainDrivenSample.SalesAndCatalog.Specification;

    public interface IAuthorRepository : IRepository<Author, Guid>
    {
        Task<Author> GetByName(string name);
    }

    public interface IBookRepository : IRepository<Book, Guid>
    {
        Book? GetByISBN(string isbn) => this.Find(new BookByIsbnSpec(isbn)).FirstOrDefault();
    }

    public interface IPublisherRepository : IRepository<Publisher, Guid>
    {
        Task<Publisher> GetByName(string name);
    }
}
