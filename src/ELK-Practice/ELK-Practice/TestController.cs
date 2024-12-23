using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ELK_Practice;

[Route("api/[controller]")]
public class TestController: ControllerBase
{
    [HttpGet]
    public string HelloWorld()
    {
        Log.Information("Hi This is James");
        
        return "Hello world";
    }
}