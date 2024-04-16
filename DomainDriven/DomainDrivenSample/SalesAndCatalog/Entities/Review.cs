using DomainDriven;
using DomainDrivenSample.SalesAndCatalog.Aggregates;

namespace DomainDrivenSample.SalesAndCatalog.Entities
{
    public class Review : Entity<long>
    {
        public string ID { get; private set; }
        public Reader Reviewer { get; private set; }
        public string Content { get; private set; }
        public int Rating { get; private set; }
        public DateTime? Date { get; private set; }

        public Review(long id, Reader reviewer, string content, int rating, DateTime? date = null)
        {
            Id = id;
            Reviewer = reviewer;
            Content = content;
            Rating = rating;
            Date = date ?? DateTime.Now;
        }

        public void ChangeContent(string newContent)
        {
            Content = newContent;
        }

        public void ChangeRating(int newRating)
        {
            Rating = newRating;
        }

        public void ChangeDate(DateTime newDate)
        {
            Date = newDate;
        }

        public void Delete()
        {
            Date = null;
        }

        public bool IsDeleted()
        {
            return Date == null;
        }

        public bool IsPublished()
        {
            return Date != null;
        }
    }
}
