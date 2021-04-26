using System;
using System.Threading.Tasks;
using Guestbook.Application.Models;
using Guestbook.Application.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Guestbook.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestbookController : ControllerBase
    {
        private readonly IGuestbookStorage _storage;
        private readonly ILogger<GuestbookController> _logger;

        public GuestbookController(IGuestbookStorage storage, ILogger<GuestbookController> logger)
        {
            _storage = storage;
            _logger = logger;
        }

        [HttpGet]
        public Task<GuestbookModel> Get()
        {
            _logger.LogInformation("Get: Received request for guestbook");
            return _storage.GetGuestbook();
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddEntry(AddEntryRequest request)
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

            await _storage.AddEntry(new GuestbookEntry(request.Message, DateTime.UtcNow));

            return Ok();
        }

        [HttpDelete]
        [Route("clear")]
        public async Task<ActionResult> Clear()
        {
            _logger.LogInformation("Clear: Received clear request");
            await _storage.ClearGuestbook();
            return Ok();
        }
    }
}