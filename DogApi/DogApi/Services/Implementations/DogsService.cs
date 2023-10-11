using DogApi.Models;
using DogApi.Repositories.Interfaces;
using DogApi.Services.Interfaces;

namespace DogApi.Services.Implementations;

public class DogsService : IDogsService
{
    private readonly IBaseRepository<Dog> _repository;

    public DogsService(IBaseRepository<Dog> repository)
    {
        _repository = repository;
    }
    
    public async Task<List<Dog>> GetDogs()
    {
        var dogs = await _repository.GetAsync();

        return dogs;
    }

    public async Task AddDog(Dog dog)
    {
        if (dog == null)
            throw new ArgumentNullException(nameof(dog), "Dog object cannot be null.");

        await _repository.AddAsync(dog);
    }
}