using DogApi.Enums;
using DogApi.Models;
using DogApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

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
    public async Task<IActionResult> GetDogs(int pageNumber = 1, SortingParam sortBy = 0)
    {
        const int pageSize = 10;
        
        var dogs = await _dogsService.GetDogs(sortBy);

        return Ok(dogs.ToPagedList(pageNumber, pageSize));
    }

    [HttpPost("dog")]
    public async Task<IActionResult> AddDog([FromBody] Dog dog)
    {
        if (ModelState.IsValid)
        {
            await _dogsService.AddDog(dog);

            return Ok();
        }
        
        return BadRequest("Invalid dog data. Please check the request format.");
    }
}