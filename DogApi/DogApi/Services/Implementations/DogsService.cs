using System.Linq.Expressions;
using System.Reflection;
using DogApi.Enums;
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
    
    public async Task<List<Dog>> GetDogs(SortingParam sortBy = 0)
    {
        List<Dog> dogs;
        
        if (sortBy != 0)
        {
            var query = GetOrderByExpression(sortBy);

            dogs = await _repository.GetAsync(orderBy: query);
        }
        else
        {
            dogs = await _repository.GetAsync();
        }

        return dogs;
    }

    public async Task AddDog(Dog dog)
    {
        if (dog == null)
        {
            throw new ArgumentNullException(nameof(dog), "Dog object cannot be null.");
        }
        
        await EnsureUniqueDogName(dog.Name);
        
        EnsureValidDogValues(dog);
        
        await _repository.AddAsync(dog);
    }
    
    private void EnsureValidDogValues(Dog dog)
    {
        if (string.IsNullOrWhiteSpace(dog.Name) || 
            string.IsNullOrWhiteSpace(dog.Color) || 
            dog.TailLength <= 0 || 
            dog.Weight <= 0)
        {
            throw new ArgumentException("Invalid dog values.");
        }
    }
    
    private async Task EnsureUniqueDogName(string name)
    {
        var existingDog = await _repository.GetAsync(d => d.Name == name);

        if (existingDog.Any())
        {
            throw new InvalidOperationException("A dog with the same name already exists in the database.");
        }    
    }
    
    private Expression<Func<IQueryable<Dog>, IOrderedQueryable<Dog>>> GetOrderByExpression(SortingParam sortBy)
    {
        Expression<Func<IQueryable<Dog>, IOrderedQueryable<Dog>>> query;

        switch (sortBy)
        {
            case SortingParam.TailLength:
                query = q => q.OrderByDescending(q => q.TailLength);
                break;
            case SortingParam.WeightDesc:
                query = q => q.OrderByDescending(q => q.Weight);
                break;
            case SortingParam.Weight:
                query = q => q.OrderBy(q => q.Weight);
                break;
            default:
                query = q => q.OrderBy(q => q.TailLength);
                break;
        }

        return query;
    }
}