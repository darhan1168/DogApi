using DogApi.Helpers;
using DogApi.Models;
using DogApi.Services.Interfaces;
using DogApi.ViewModels;
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
    public async Task<IActionResult> GetDogs()
    {
        var dogs = await _dogsService.GetDogs();

        var dogViewModels = dogs.Select(dog =>
        {
            var dogViewModel = new DogViewModel();
            dog.MapTo(dogViewModel);
            
            return dogViewModel;
        }).ToList();
        
        return Ok(dogViewModels);
    }

    [HttpPost("dog")]
    public async Task<IActionResult> GetDogs(DogViewModel dogViewModel)
    {
        if (ModelState.IsValid)
        {
            var dog = new Dog();
            dogViewModel.MapTo(dog);

            await _dogsService.AddDog(dog);

            return Ok();
        }
        
        return BadRequest(ModelState);
    }
}