using DogApi.Controllers;
using DogApi.Enums;
using DogApi.Models;
using DogApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using X.PagedList;

namespace DogApiUnitTests;

public class DogControllerTests
{
    [Fact]
    public async Task GetDogs_ValidValues_PagedListOfDogs()
    {
        var dogsServiceMock = new Mock<IDogsService>();
        dogsServiceMock.Setup(service => service.GetDogs(It.IsAny<SortingParam>()))
                      .ReturnsAsync(new List<Dog>
                      {
                          new Dog { Name = "Dog1" },
                          new Dog { Name = "Dog2" }
                      });

        var controller = new DogController(dogsServiceMock.Object);
        
        var result = await controller.GetDogs();
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        var pagedList = Assert.IsAssignableFrom<IPagedList<Dog>>(okResult.Value);
        Assert.Equal(2, pagedList.Count);
        Assert.Equal("Dog1", pagedList[0].Name);
        Assert.Equal("Dog2", pagedList[1].Name);
    }

    [Fact]
    public async Task AddDog_ModelStateIsValid_OkResult()
    {
        var dogsServiceMock = new Mock<IDogsService>();
        var controller = new DogController(dogsServiceMock.Object);
        var dog = new Dog { Name = "Dog1" };
        
        var result = await controller.AddDog(dog);

        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task AddDog_ModelStateIsNotValid_BadRequest()
    {
        var dogsServiceMock = new Mock<IDogsService>();
        var controller = new DogController(dogsServiceMock.Object);
        var dog = new Dog();
        
        controller.ModelState.AddModelError("Name", "The Name field is required."); 
        var result = await controller.AddDog(dog);
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Invalid dog data. Please check the request format.", badRequestResult.Value);
    }
}