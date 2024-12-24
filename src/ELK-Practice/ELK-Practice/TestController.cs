using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ELK_Practice;

[Route("api/[controller]")]
public class TestController: ControllerBase
{
    [HttpGet]
    public string HelloWorld()
    {
        Log.Debug("Hi This is James debug");
        
        return "Hello world";
    }
}