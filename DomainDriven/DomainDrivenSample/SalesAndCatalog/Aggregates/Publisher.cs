using System.Collections.ObjectModel;
using DomainDrivenSample.SalesAndCatalog.Entities;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Aggregates
{
    public class Publisher : AggregateRoot<Guid>
    {
        private List<Contract> _contracts;
        public string Name { get; private set; }
        public EmailAddress Email { get; private set; }
        public ReadOnlyCollection<Contract> Contracts => _contracts.AsReadOnly();

        public Publisher(Guid id, string name, EmailAddress email)
        {
            Id = id;
            Name = name;
            Email = email;
            _contracts = new List<Contract>();
        }

        public void AddContract(Contract contract)
        {
            _contracts.Add(contract);
        }

        public void RemoveContract(Contract contract)
        {
            _contracts.Remove(contract);
        }
    }
}
