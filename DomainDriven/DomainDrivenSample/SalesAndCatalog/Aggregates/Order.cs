using DomainDrivenSample.SalesAndCatalog.Entities;
using DomainDrivenSample.SalesAndCatalog.Services;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Aggregates
{
    public class Order : AggregateRoot<Guid>
    {
        public Reader Buyer { get; private set; }
        public List<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
        public DateTime DatePlaced { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order(Guid id, Reader buyer)
            : base(id)
        {
            Buyer = buyer;
            DatePlaced = DateTime.UtcNow;
            Status = OrderStatus.Placed;
        }

        public void AddBookToOrder(Book book, Edition edition, int quantity, IPricingService pricingService)
        {
            if (!book.Editions.Contains(edition))
            {            
                throw new InvalidOperationException("Edition is not for the book");
            }
            
            var orderItem = new OrderItem(
                OrderItems.Any() ? OrderItems.Max(oi => oi.Id) + 1 : 1,
                book,
                edition,
                quantity,
                pricingService.CalculatePrice(edition, quantity)
            );
            OrderItems.Add(orderItem);
        }

        public void RemoveItem(long orderItemId)
        {
            var orderItem = OrderItems.Find(oi => oi.Id == orderItemId);
            if (orderItem != null)
            {
                OrderItems.Remove(orderItem);
            }
        }

        public void UpdateItem(long orderItemId, int quantity)
        {
            var orderItem = OrderItems.FirstOrDefault(oi => oi.Id == orderItemId);
            if (orderItem == null)
            {
                throw new InvalidOperationException("Order item not found");
            }

            orderItem.UpdateQuantity(quantity);
        }

        public void Submit()
        {
            if (Status != OrderStatus.Placed)
            {
                throw new InvalidOperationException("Order already submitted");
            }

            Status = OrderStatus.Submitted;
        }

        public void Cancel()
        {
            if (Status != OrderStatus.Placed)
            {
                throw new InvalidOperationException("Order already submitted");
            }

            Status = OrderStatus.Cancelled;
        }

        public void Ship()
        {
            if (Status != OrderStatus.Submitted)
            {
                throw new InvalidOperationException("Order not submitted");
            }

            Status = OrderStatus.Shipped;
        }

        public void Deliver()
        {
            if (Status != OrderStatus.Shipped)
            {
                throw new InvalidOperationException("Order not shipped");
            }

            Status = OrderStatus.Delivered;
        }

        public void Return()
        {
            if (Status != OrderStatus.Delivered)
            {
                throw new InvalidOperationException("Order not delivered");
            }

            Status = OrderStatus.Returned;
        }

        public void Refund()
        {
            if (Status != OrderStatus.Returned)
            {
                throw new InvalidOperationException("Order not returned");
            }

            Status = OrderStatus.Refunded;
        }

        public void Complete()
        {
            if (Status != OrderStatus.Returned && Status != OrderStatus.Refunded)
            {
                throw new InvalidOperationException("Order not returned or refunded");
            }

            Status = OrderStatus.Completed;
        }

        public void Archive()
        {
            if (Status != OrderStatus.Completed)
            {
                throw new InvalidOperationException("Order not completed");
            }

            Status = OrderStatus.Archived;
        }

        public void Delete()
        {
            if (Status != OrderStatus.Archived)
            {
                throw new InvalidOperationException("Order not archived");
            }

            Status = OrderStatus.Deleted;
        }
    }
}
