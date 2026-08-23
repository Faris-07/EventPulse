using System;
using System.Collections.Generic;
using EventPulse.Api.Models;
using EventPulse.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventPulse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ITicketOrderService _ticketOrderService;

        public OrdersController(ITicketOrderService ticketOrderService)
        {
            _ticketOrderService = ticketOrderService;
        }

        [HttpPost("hold")]
        public IActionResult CreateHold([FromBody] HoldRequest request)
        {
            try
            {
                var result = _ticketOrderService.CreateHold(request);
                return CreatedAtAction(nameof(CreateHold), new { id = result.HoldId }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}