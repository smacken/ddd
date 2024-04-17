using DomainDrivenSample.SalesAndCatalog.Aggregates;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Services;

public interface IBookRegistry
{
    Book Register(Book book, Publisher publisher);
}

public class BookRegistry : IBookRegistry
{
    public Book Register(Book book, Publisher publisher)
    {
        book.Metadata.RegisterBook(publisher.Name, GenerateIsbn());
        return book;
    }

    public Isbn GenerateIsbn()
    {
        return new Isbn(Guid.NewGuid().ToString());
    }
}
