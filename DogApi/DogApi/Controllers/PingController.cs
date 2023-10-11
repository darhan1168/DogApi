using Microsoft.AspNetCore.Mvc;

namespace DogApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PingController : Controller
{
    private readonly IConfiguration _configuration;

    public PingController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet]
    public IActionResult Ping()
    {
        var version = _configuration["AppSettings:Version"];
        
        return Ok(version);
    }
}