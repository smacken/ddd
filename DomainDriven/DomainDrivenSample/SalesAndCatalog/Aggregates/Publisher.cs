using System.Collections.ObjectModel;
using DomainDrivenSample.SalesAndCatalog.Entities;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Aggregates
{
    public enum ContractType
    {
        Royalty,
        FixedPrice
    }

    public enum PublisherStatus
    {
        OpenToOffers,
        NotAcceptingOffers
    }

    public class Publisher : AggregateRoot<Guid>
    {
        private readonly List<Contract> _contracts;
        public string Name { get; private set; }
        public PublisherStatus Status { get; private set; } = PublisherStatus.OpenToOffers;
        public EmailAddress Email { get; private set; }
        public ReadOnlyCollection<Contract> Contracts => _contracts.AsReadOnly();

        public Publisher(Guid id, string name, EmailAddress email)
            : base(id)
        {
            Name = name;
            Email = email;
            _contracts = new List<Contract>();
        }

        public Publisher(string name)
            : this(
                Guid.NewGuid(),
                name,
                new EmailAddress("info@" + name.Replace(" ", "").ToLower() + ".com")
            ) { }

        public Contract DrawUpContract(Author author, Book book, ContractType? contractType)
        {
            var existingContract = _contracts.FirstOrDefault(c => c.Author.Id == author.Id);
            if (existingContract is not null)
                return existingContract;
            //omit terms, etc until negotiation
            return new Contract(author, this, book, contractType ?? ContractType.Royalty);
        }

        /// <summary>
        /// Negotiate a contract with the Author for the book.
        /// </summary>
        public void Negotiate(Contract contract, ContractType contractType)
        {
            SetAdvances(contract, contractType);
            SetRoyalties(contract, contractType);
            SetDeadline(contract, contractType);
            SetLegalTerms(contract, contractType);
            _contracts.Add(contract);
        }

        private void SetLegalTerms(Contract contract, ContractType contractType)
        {
            string legalTerms = contractType switch
            {
                ContractType.Royalty => "Royalty terms",
                ContractType.FixedPrice => "Fixed price terms",
                _ => throw new ArgumentOutOfRangeException(nameof(contractType), contractType, null)
            };
            contract.SetContractDetails(null, null, legalTerms);
        }

        private void SetDeadline(Contract contract, ContractType contractType)
        {
            string deadline = contractType switch
            {
                ContractType.Royalty => "Royalty deadline",
                ContractType.FixedPrice => "Fixed price deadline",
                _ => throw new ArgumentOutOfRangeException(nameof(contractType), contractType, null)
            };
            contract.SetContractDetails(null, deadline, null);
        }

        private void SetRoyalties(Contract contract, ContractType contractType)
        {
            string royalties = contractType switch
            {
                ContractType.Royalty => "Royalty royalties",
                ContractType.FixedPrice => "Fixed price royalties",
                _ => throw new ArgumentOutOfRangeException(nameof(contractType), contractType, null)
            };
            contract.SetContractDetails(royalties, null, null);
        }

        private void SetAdvances(Contract contract, ContractType contractType)
        {
            decimal advances = contractType switch
            {
                ContractType.Royalty => 1000m,
                ContractType.FixedPrice => 0m,
                _ => throw new ArgumentOutOfRangeException(nameof(contractType), contractType, null)
            };
            contract.SetAdvances(advances);
        }

        /// <summary>
        /// Terminate a contract
        /// </summary>
        public void Terminate(Contract contract)
        {
            var contractToTerminate = _contracts.FirstOrDefault(c => c.Id == contract.Id);
            if (contractToTerminate is null)
                return;
            contractToTerminate.ChangeDates(
                contractToTerminate.DateRange.StartDate,
                DateTime.UtcNow
            );
            // todo: raise notification event
        }
    }
}
