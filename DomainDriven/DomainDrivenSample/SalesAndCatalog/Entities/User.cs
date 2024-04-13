using DomainDriven;

namespace DomainDrivenSample.SalesAndCatalog.Entities
{
    public class User : Entity<Guid>
    {
        public string Username { get; private set; }
        public string Email { get; private set; }

        public User(Guid id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
        }
    }
}