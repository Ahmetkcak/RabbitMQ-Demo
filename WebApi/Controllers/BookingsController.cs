
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController(ILogger<BookingsController> logger, IMessageProducer messageProducer) : ControllerBase
{
    //In-memory database
    private static readonly List<Booking> bookings = [];

    [HttpPost]
    public IActionResult Post(Booking booking)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        else
        {
            bookings.Add(booking);
            messageProducer.SendMessaging<Booking>(booking);
            return Ok();
        }
    }
}
