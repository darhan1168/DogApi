using DogApi.Enums;
using DogApi.Models;
using DogApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DogApi.Controllers;

[ApiController]
public class DogController : Controller
{
    private readonly IDogsService _dogsService;

    public DogController(IDogsService dogsService)
    {
        _dogsService = dogsService;
    }

    [HttpGet("dogs")]
    public async Task<IActionResult> GetDogs(SortingParam sortBy = 0)
    {
        var dogs = await _dogsService.GetDogs(sortBy);

        return Ok(dogs);
    }

    [HttpPost("dog")]
    public async Task<IActionResult> GetDogs(Dog dog)
    {
        if (ModelState.IsValid)
        {
            await _dogsService.AddDog(dog);

            return Ok();
        }
        
        return BadRequest(ModelState);
    }
}