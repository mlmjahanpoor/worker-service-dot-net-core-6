using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MyWorker;

[ApiController]
[Route("[controller]/[action]")]
public class TestController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        Console.WriteLine("test   22222");

        return Ok(new { message = "Hello world!" });
    }
}