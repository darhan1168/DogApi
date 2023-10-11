using DogApi.Helpers;
using DogApi.Services.Interfaces;
using DogApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DogApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DogsController : Controller
{
    private readonly IDogsService _dogsService;

    public DogsController(IDogsService dogsService)
    {
        _dogsService = dogsService;
    }

    [HttpGet]
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
}