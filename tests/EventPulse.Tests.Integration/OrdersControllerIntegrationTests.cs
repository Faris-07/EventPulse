using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EventPulse.Api;
using EventPulse.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace EventPulse.Tests.Integration
{
    public class OrdersControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public OrdersControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            // Creates an in-memory HTTP client pointing to the ASP.NET Core application
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateHold_ValidPayload_Returns201CreatedAndHoldResponse()
        {
            // Arrange
            var holdRequest = new HoldRequest(1, "GA", 2);
            var content = new StringContent(
                JsonSerializer.Serialize(holdRequest), 
                Encoding.UTF8, 
                "application/json"
            );

            // Act
            var response = await _client.PostAsync("/api/orders/hold", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var holdResult = JsonSerializer.Deserialize<HoldResponse>(responseBody, options);

            holdResult.Should().NotBeNull();
            holdResult.HoldId.Should().StartWith("HOLD-");
            holdResult.Quantity.Should().Be(2);
            holdResult.Subtotal.Should().Be(100.00m);
        }

        [Fact]
        public async Task CreateHold_ExceedsMaxQuantity_Returns400BadRequest()
        {
            // Arrange (Requesting 5 tickets exceeds limit of 4)
            var holdRequest = new HoldRequest(1, "GA", 5);
            var content = new StringContent(
                JsonSerializer.Serialize(holdRequest), 
                Encoding.UTF8, 
                "application/json"
            );

            // Act
            var response = await _client.PostAsync("/api/orders/hold", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Contain("Maximum 4 tickets allowed per order.");
        }

        [Fact]
        public async Task CreateHold_InvalidTicketType_Returns404NotFound()
        {
            // Arrange
            var holdRequest = new HoldRequest(1, "INVALID_CODE", 2);
            var content = new StringContent(
                JsonSerializer.Serialize(holdRequest), 
                Encoding.UTF8, 
                "application/json"
            );

            // Act
            var response = await _client.PostAsync("/api/orders/hold", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}