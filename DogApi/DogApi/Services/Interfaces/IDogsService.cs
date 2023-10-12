using DogApi.Enums;
using DogApi.Models;

namespace DogApi.Services.Interfaces;

public interface IDogsService
{
    Task<List<Dog>> GetDogs(SortingParam sortBy = 0);
    
    Task AddDog(Dog dog);
}