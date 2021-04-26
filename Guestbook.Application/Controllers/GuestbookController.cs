using System;
using Guestbook.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Guestbook.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestbookController : ControllerBase
    {
        private static readonly GuestbookEntry[] GuestbookEntries = new[]
        {
            new GuestbookEntry("Hello, World", new DateTime(2021, 4, 1)),
            new GuestbookEntry("Coffee", new DateTime(2021, 4, 2)),
            new GuestbookEntry("Zebras", new DateTime(2021, 4, 3))
        };

        private readonly ILogger<GuestbookController> _logger;

        public GuestbookController(ILogger<GuestbookController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public GuestbookModel Get()
        {
            _logger.LogInformation("Get: Received request for guestbook");
            return new GuestbookModel(GuestbookEntries);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult AddEntry(AddEntryRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                _logger.LogInformation($"AddEntry: Invalid request body");
                return BadRequest("Must provide a valid message");
            }

            if (request.Message.Length > 100)
            {
                _logger.LogInformation($"AddEntry: Message exceeds 100 character limit, actual length {request.Message.Length}");
                return BadRequest("Message cannot exceed 100 characters");
            }

            _logger.LogInformation($"AddEntry: {request.Message}");

            return Ok();
        }

        [HttpDelete]
        [Route("clear")]
        public ActionResult Clear()
        {
            _logger.LogInformation("Clear: Received clear request");
            return Ok();
        }
    }
}