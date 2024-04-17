using DomainDrivenSample.SalesAndCatalog.Aggregates;
using DomainDrivenSample.SalesAndCatalog.Entities;
using DomainDrivenSample.SalesAndCatalog.Services;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace DomainDrivenSample.Tests
{
    public class CatalogTests : IClassFixture<DIFixture>
    {
        private readonly DIFixture _fixture;

        public CatalogTests(DIFixture fixture)
        {
            _fixture = fixture;
        }

        //Unit tests for Book Information:
        // 1. Store unique identifiers for each book.
        // 2. Track titles, descriptions, genres, and other metadata.
        // 3. Associate books with one or more authors.
        // 4. Maintain a list of editions for each book, with details like format (digital, paperback, hardcover), price, and stock quantity.

        [Fact]
        public void PublishBook()
        {
            //author submits a book to publisher,
            var publisher = new Publisher("Addison-Wesley Professional");
            var author = new Author("Andrew Hunt", "Biography");
            var book = new BookMetadata(
                isbn: Guid.Empty.ToString(),
                title: "The Pragmatic Programmer: Your Journey to Mastery",
                description: "Programming",
                genre: "Programming",
                authors: new List<string>() { "Andrew Hunt", "David Thomas" }
            );
            author.WriteBook(new Book(book));
            author.SubmitBook(book.Title);
        }

        [Fact]
        public void Should_store_unique_books()
        {
            //arrange
            var repository = _fixture.ServiceProvider.GetService<IBookRepository>();
            var catalog = new Catalog(repository!);
            var bookId = Guid.NewGuid();
            var book = new Book(
                bookId,
                new BookMetadata(
                    "978-0-306-40615-7",
                    "The Pragmatic Programmer: Your Journey to Mastery",
                    "Addison-Wesley Professional",
                    "Programming",
                    new List<string>() { "Andrew Hunt, David Thomas" }
                )
            );
            var book2 = new Book(
                Guid.NewGuid(),
                new BookMetadata(
                    "978-0-306-40615-8",
                    "The Pragmatic Programmer: Your Journey to Mastery",
                    "Addison-Wesley Professional",
                    "programming",
                    new List<string>() { "Andrew Hunt, David Thomas" }
                )
            );

            //act
            //find book by Id
            catalog.AddBook(book);
            catalog.AddBook(book2);

            //assert
            Assert.Equal(book, catalog.FindBook("978-0-306-40615-7"));
        }

        //unit test 2. Track titles, descriptions, genres, and other metadata.
        [Fact]
        public void Should_track_titles_descriptions_genres_and_other_metadata()
        {
            //arrange
            var repository = _fixture.ServiceProvider.GetService<IBookRepository>();
            var catalog = new Catalog(repository!);
            var bookId = Guid.NewGuid();
            var book = new Book(
                bookId,
                new BookMetadata(
                    "978-0-306-40615-7",
                    "The Pragmatic Programmer: Your Journey to Mastery",
                    "Addison-Wesley Professional",
                    "Programming",
                    new List<string>() { "Andrew Hunt, David Thomas" }
                )
            );

            //act
            catalog.AddBook(book);

            //assert
            Assert.Equal(
                "978-0-306-40615-7",
                catalog.FindBook(bookId.ToString())!.Metadata.ISBN.Value
            );
            Assert.Equal(
                "The Pragmatic Programmer: Your Journey to Mastery",
                catalog.FindBook("978-0-306-40615-7").Metadata.Title
            );
            Assert.Equal(
                "Addison-Wesley Professional",
                catalog.FindBook("978-0-306-40615-7").Metadata.Publisher
            );
            Assert.Equal("Programming", catalog.FindBook("978-0-306-40615-7").Metadata.Genre);
            Assert.Equal(
                "Andrew Hunt, David Thomas",
                catalog.FindBook("978-0-306-40615-7").Metadata.Authors[0]
            );
        }

        //unit test 3. Associate books with one or more authors.
        [Fact]
        public void Should_associate_books_with_one_or_more_authors()
        {
            //arrange
            var repository = _fixture.ServiceProvider.GetService<IBookRepository>();
            var catalog = new Catalog(repository!);
            var bookId = Guid.NewGuid();
            var book = new Book(
                bookId,
                new BookMetadata(
                    "978-0-306-40615-7",
                    "The Pragmatic Programmer: Your Journey to Mastery",
                    "Addison-Wesley Professional",
                    "Programming",
                    new List<string>() { "Andrew Hunt, David Thomas" }
                )
            );

            //act
            catalog.AddBook(book);

            //assert
            Assert.Equal("Andrew Hunt", catalog.FindBook("978-0-306-40615-7").Metadata.Authors[0]);
        }

        //unit test 4. Maintain a list of editions for each book, with details like format (digital, paperback, hardcover), price, and stock quantity.
        [Fact]
        public void Should_maintain_editions()
        {
            //arrange
            var repository = _fixture.ServiceProvider.GetService<IBookRepository>();
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));
            var catalog = new Catalog((IBookRepository)repository);
            var bookId = Guid.NewGuid();
            var book = new Book(
                bookId,
                new BookMetadata(
                    "978-0-306-40615-7",
                    "The Pragmatic Programmer: Your Journey to Mastery",
                    "Addison-Wesley Professional",
                    "Programming",
                    new List<string>() { "Andrew Hunt, David Thomas" }
                )
            );
            var edition = new Edition(bookId, EditionType.Digital, new Money(29.99m, "USD"), 100);

            //act
            book.AddEdition(edition);
            catalog.AddBook(book);

            //assert
            var editions = catalog.FindBook("978-0-306-40615-7")?.Editions;
            Assert.Equal(1, editions?.Count);
            Assert.Equal("Digital", editions?[0].Type.ToString());
        }
    }
}
