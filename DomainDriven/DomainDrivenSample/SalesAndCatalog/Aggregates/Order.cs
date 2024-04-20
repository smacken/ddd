using System.Collections.ObjectModel;
using DomainDrivenSample.SalesAndCatalog.DomainEvents;
using DomainDrivenSample.SalesAndCatalog.Entities;
using DomainDrivenSample.SalesAndCatalog.Services;
using DomainDrivenSample.SalesAndCatalog.ValueObjects;

namespace DomainDrivenSample.SalesAndCatalog.Aggregates
{
    public class Order : AggregateRoot<Guid>
    {
        private List<OrderItem> _orderItems = new();
        public Reader Buyer { get; private set; }
        public ReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public DateTime DatePlaced { get; private set; } = DateTime.UtcNow;
        public OrderStatus Status { get; private set; } = OrderStatus.Placed;
        public List<PromotionCode> PromotionCode { get; private set; }

        public Order(Guid id, Reader buyer, List<PromotionCode>? promotionCodes = null)
            : base(id)
        {
            Buyer = buyer;
            PromotionCode = promotionCodes ?? new List<PromotionCode>();
            Status = OrderStatus.Placed;
            DatePlaced = DateTime.UtcNow;
        }

        public Order(Reader buyer, List<PromotionCode>? promotionCodes = null)
            : base(Guid.NewGuid())
        {
            Buyer = buyer;
            PromotionCode = promotionCodes ?? new List<PromotionCode>();
            Status = OrderStatus.Placed;
            DatePlaced = DateTime.UtcNow;
        }

        public void AddBookToOrder(
            Book book,
            Edition edition,
            int quantity,
            IPricingCalculator pricingService
        )
        {
            if (!book.Editions.Contains(edition))
            {
                throw new InvalidOperationException("Edition is not for the book");
            }

            OrderItem orderItem =
                new(
                    OrderItems.Count != 0 ? OrderItems.Max(oi => oi.Id) + 1 : 1,
                    book,
                    edition,
                    quantity,
                    pricingService.CalculatePrice(edition, quantity)
                );
            _orderItems.Add(orderItem);
        }

        public void RemoveItem(long orderItemId)
        {
            var index = _orderItems.FindIndex(oi => oi.Id == orderItemId);
            if (index < 0)
                return;
            _orderItems.RemoveAt(index);
        }

        public void UpdateItem(long orderItemId, int quantity)
        {
            OrderItem? orderItem =
                OrderItems.FirstOrDefault(oi => oi.Id == orderItemId)
                ?? throw new InvalidOperationException("Order item not found");
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
            foreach (var orderItem in OrderItems)
            {
                DomainDriven.DomainEvents.Raise(
                    new BookStockChangedEvent(orderItem.Book.Id, orderItem.Quantity)
                );
            }
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
