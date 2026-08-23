using System;
using EventPulse.Api.Models;
using EventPulse.Api.Services;
using FluentAssertions;
using Xunit;

namespace EventPulse.Tests.Unit
{
    public class TicketOrderServiceTests
    {
        private readonly TicketOrderService _sut = new TicketOrderService();

        [Fact]
        public void CreateHold_ValidRequest_ReturnsHoldResponseWith10MinExpiration()
        {
            // Arrange
            var request = new HoldRequest(1, "GA", 2);

            // Act
            var result = _sut.CreateHold(request);

            // Assert
            result.Should().NotBeNull();
            result.HoldId.Should().StartWith("HOLD-");
            result.Quantity.Should().Be(2);
            result.Subtotal.Should().Be(100.00m);
            result.ExpirationTimestamp.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(10), TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void CreateHold_ExceedsMaxQuantityLimit_ThrowsArgumentException()
        {
            // Arrange (Attempting 5 tickets)
            var request = new HoldRequest(1, "GA", 5);

            // Act
            Action act = () => _sut.CreateHold(request);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage("Maximum 4 tickets allowed per order.");
        }

        [Fact]
        public void CreateHold_ExceedsAvailableStock_ThrowsInvalidOperationException()
        {
            // Arrange (VIP has only 5 available; requesting 4 twice exceeds stock)
            var request1 = new HoldRequest(1, "VIP", 4);
            var request2 = new HoldRequest(1, "VIP", 2);

            _sut.CreateHold(request1);

            // Act
            Action act = () => _sut.CreateHold(request2);

            // Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage("Insufficient stock. Only 1 remaining.");
        }
    }
}