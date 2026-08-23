using System;
using System.Collections.Generic;
using EventPulse.Api.Models;

namespace EventPulse.Api.Services
{
    public interface ITicketOrderService
    {
        HoldResponse CreateHold(HoldRequest request);
    }

    public class TicketOrderService : ITicketOrderService
    {
        private readonly Dictionary<string, TicketInventory> _inventory = new Dictionary<string, TicketInventory>
        {
            { "VIP", new TicketInventory { TicketTypeId = "VIP", Price = 150.00m, AvailableStock = 5 } },
            { "GA", new TicketInventory { TicketTypeId = "GA", Price = 50.00m, AvailableStock = 10 } },
            { "EARLY", new TicketInventory { TicketTypeId = "EARLY", Price = 35.00m, AvailableStock = 20 } }
        };

        public HoldResponse CreateHold(HoldRequest request)
        {
            if (request.Quantity > 4)
            {
                throw new ArgumentException("Maximum 4 tickets allowed per order.");
            }

            if (request.Quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            if (!_inventory.TryGetValue(request.TicketTypeId, out var item))
            {
                throw new KeyNotFoundException($"Ticket type '{request.TicketTypeId}' was not found.");
            }

            if (request.Quantity > item.AvailableStock)
            {
                throw new InvalidOperationException($"Insufficient stock. Only {item.AvailableStock} remaining.");
            }

            item.AvailableStock -= request.Quantity;
            var holdId = "HOLD-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            var subtotal = request.Quantity * item.Price;
            var expiration = DateTime.UtcNow.AddMinutes(10);

            return new HoldResponse(holdId, request.EventId, request.TicketTypeId, request.Quantity, subtotal, expiration);
        }
    }
}