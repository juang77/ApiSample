using ApiSample.Controllers;
using ApiSample.Models;
using ApiSample.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiSample.Tests;

public class CityControllerTests
{
    private readonly Mock<ICityService> _serviceMock;
    private readonly CityController _controller;

    public CityControllerTests()
    {
        _serviceMock = new Mock<ICityService>();
        _controller = new CityController(_serviceMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithList()
    {
        var cities = new List<City> { new City(1, 1, "C") };
        _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(cities);

        var action = await _controller.GetAll();

        var ok = action as OkObjectResult;
        ok.Should().NotBeNull();
        ok!.Value.Should().BeEquivalentTo(cities);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenNull()
    {
        _serviceMock.Setup(s => s.GetByIdAsync(5)).ReturnsAsync((City?)null);

        var action = await _controller.GetById(5);

        action.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        var city = new City(0, 1, "NewCity");
        _serviceMock.Setup(s => s.CreateAsync(It.IsAny<City>())).ReturnsAsync(7);

        var result = await _controller.Create(city);

        var created = result as CreatedAtActionResult;
        created.Should().NotBeNull();
        created!.ActionName.Should().Be(nameof(CityController.GetById));
        ((City)created.Value!).Name.Should().Be(city.Name);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenUpdated()
    {
        var city = new City(1, 1, "Up");
        _serviceMock.Setup(s => s.UpdateAsync(city)).ReturnsAsync(true);

        var res = await _controller.Update(city);
        res.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenNotUpdated()
    {
        var city = new City(99, 1, "X");
        _serviceMock.Setup(s => s.UpdateAsync(city)).ReturnsAsync(false);

        var res = await _controller.Update(city);
        res.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenDeleted()
    {
        _serviceMock.Setup(s => s.DeleteAsync(2)).ReturnsAsync(true);

        var res = await _controller.Delete(2);
        res.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenNotDeleted()
    {
        _serviceMock.Setup(s => s.DeleteAsync(3)).ReturnsAsync(false);

        var res = await _controller.Delete(3);
        res.Should().BeOfType<NotFoundResult>();
    }
}
