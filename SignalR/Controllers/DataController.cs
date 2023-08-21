using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;
using System;

namespace SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IHubContext<RealTimeHub> _hubContext;

        public DataController(IHubContext<RealTimeHub> hubContext)
        {
            _hubContext = hubContext;
        }

[HttpGet]
public IActionResult GetRealTimeData()
{
    // Dummy data for demonstration
    var dummyData = new
    {
        Message = "Hello from the server!",
        Timestamp = DateTime.UtcNow
    };

    // Send data to connected clients via SignalR hub
    _hubContext.Clients.All.SendAsync("SendRealTimeData", dummyData);

    return Ok(dummyData);
}
    }
}
