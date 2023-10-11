using DogApi.Models;

namespace DogApi.Services.Interfaces;

public interface IDogsService
{
    Task<List<Dog>> GetDogs();
}