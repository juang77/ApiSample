using ApiSample.Data;
using ApiSample.Models;
using ApiSample.Services;
using FluentAssertions;
using Moq;

namespace ApiSample.Tests;

public class CityServiceTests
{
    private readonly Mock<ICityRepository> _repoMock;
    private readonly CityService _service;

    public CityServiceTests()
    {
        _repoMock = new Mock<ICityRepository>();
        _service = new CityService(_repoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllCities()
    {
        // Arrange
        var cities = new List<City>
            {
                new City(1, 10, "A"),
                new City(2, 10, "B")
            };
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(cities);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(cities);
        _repoMock.Verify(r => r.GetAllAsync(), Times.Once);
    }
    [Fact]
    public async Task GetByIdAsync_ReturnsCity_WhenFound()
    {
        var city = new City(1, 10, "City1");
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(city);

        var result = await _service.GetByIdAsync(1);

        result.Should().BeSameAs(city);
        _repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_InvokesRepositoryAndReturnsId()
    {
        var city = new City(0, 10, "New");
        _repoMock.Setup(r => r.CreateAsync(city)).ReturnsAsync(42);

        var id = await _service.CreateAsync(city);

        id.Should().Be(42);
        _repoMock.Verify(r => r.CreateAsync(city), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsTrue_WhenRepositorySucceeds()
    {
        var city = new City(1, 10, "Up");
        _repoMock.Setup(r => r.UpdateAsync(city)).ReturnsAsync(true);

        var ok = await _service.UpdateAsync(city);

        ok.Should().BeTrue();
        _repoMock.Verify(r => r.UpdateAsync(city), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenNotFound()
    {
        _repoMock.Setup(r => r.DeleteAsync(99)).ReturnsAsync(false);

        var ok = await _service.DeleteAsync(99);

        ok.Should().BeFalse();
        _repoMock.Verify(r => r.DeleteAsync(99), Times.Once);
    }
}
