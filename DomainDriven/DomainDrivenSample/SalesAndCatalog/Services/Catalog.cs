using DomainDrivenSample.SalesAndCatalog.Aggregates;
using DomainDrivenSample.SalesAndCatalog.Specification;

namespace DomainDrivenSample.SalesAndCatalog.Services
{
    public class Catalog : IDomainService
    {
        private readonly IBookRepository _bookRepository;

        public Catalog(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<Book> GetAllBooks() => _bookRepository.GetAll();

        public IRepository<Book, Guid> Get_bookRepository()
        {
            return _bookRepository;
        }

        public static Book? GetBookById(Guid id, IRepository<Book, Guid> _bookRepository) =>
            _bookRepository.GetById(id);

        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            return _bookRepository.Find(new BookIsByAuthorSpecification(author));
        }

        public void AddBook(Book book)
        {
            _bookRepository.Add(book);
        }

        public Book? FindBook(string id)
        {
            return _bookRepository.Find(new BookByIsbnSpec(id)).FirstOrDefault();
        }
    }
}
