using System;

namespace EventPulse.Api.Models
{
    public class HoldRequest
    {
        public int EventId { get; set; }
        public string TicketTypeId { get; set; }
        public int Quantity { get; set; }

        public HoldRequest() { }

        public HoldRequest(int eventId, string ticketTypeId, int quantity)
        {
            EventId = eventId;
            TicketTypeId = ticketTypeId;
            Quantity = quantity;
        }
    }

    public class HoldResponse
    {
        public string HoldId { get; set; }
        public int EventId { get; set; }
        public string TicketTypeId { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public DateTime ExpirationTimestamp { get; set; }

        public HoldResponse(string holdId, int eventId, string ticketTypeId, int quantity, decimal subtotal, DateTime expirationTimestamp)
        {
            HoldId = holdId;
            EventId = eventId;
            TicketTypeId = ticketTypeId;
            Quantity = quantity;
            Subtotal = subtotal;
            ExpirationTimestamp = expirationTimestamp;
        }
    }

    public class TicketInventory
    {
        public string TicketTypeId { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int AvailableStock { get; set; }
    }
}