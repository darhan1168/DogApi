using System.Linq.Expressions;
using DogApi.Models;
using DogApi.Repositories.Interfaces;
using DogApi.Services.Implementations;
using Moq;

namespace DogApiUnitTests;

public class DogServiceTests
{
    [Fact]
    public async Task GetDogs_WithoutSorting_NotNullResult()
    {
        var repositoryMock = new Mock<IBaseRepository<Dog>>();
        repositoryMock.Setup(repo => repo.GetAsync(null, null))
            .ReturnsAsync(new List<Dog>());

        var dogsService = new DogsService(repositoryMock.Object);
        
        var result = await dogsService.GetDogs(sortBy: 0);
        
        Assert.NotNull(result);
        Assert.IsType<List<Dog>>(result);
    }

    [Fact]
    public async Task AddDog_DogIsNull_ArgumentNullException()
    {
        var repositoryMock = new Mock<IBaseRepository<Dog>>();
        var dogsService = new DogsService(repositoryMock.Object);
        
        await Assert.ThrowsAsync<ArgumentNullException>(() => dogsService.AddDog(null));
    }

    [Fact]
    public async Task AddDog_ExistingDog_InvalidOperationException()
    {
        var existingDog = new Dog { Name = "ExistingDog" };

        var repositoryMock = new Mock<IBaseRepository<Dog>>();
        repositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Dog, bool>>>(), null))
            .ReturnsAsync(new List<Dog> { existingDog });

        var dogsService = new DogsService(repositoryMock.Object);
        var newDog = new Dog { Name = "ExistingDog" }; 
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => dogsService.AddDog(newDog));
    }
    
    
    [Theory]
    [InlineData(null, "Black", 1, 10)] 
    [InlineData("Buddy", null, 1, 10)] 
    [InlineData("Buddy", "Black", 0, 10)] 
    [InlineData("Buddy", "Black", 1, 0)] 
    [InlineData("", "Black", 1, 10)] 
    [InlineData("Buddy", "", 1, 10)] 
    public async Task AddDog_InvalidDogValues_ArgumentException(
        string name, string color, decimal tailLength, decimal weight)
    {
        var repositoryMock = new Mock<IBaseRepository<Dog>>();
        repositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Dog, bool>>>(), null))
            .ReturnsAsync(new List<Dog>());

        var dogsService = new DogsService(repositoryMock.Object);

        var invalidDog = new Dog { Name = name, Color = color, TailLength = tailLength, Weight = weight };
        
        await Assert.ThrowsAsync<ArgumentException>(() => dogsService.AddDog(invalidDog));
    }
}