using Microsoft.AspNetCore.Mvc;
using ApiSample.Services;
using ApiSample.Models;

namespace ApiSample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    // GET api/city
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cities = await _cityService.GetAllAsync();
        return Ok(cities);
    }

    // GET api/city/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var city = await _cityService.GetByIdAsync(id);
        if (city == null)
            return NotFound();

        return Ok(city);
    }

    // POST api/city
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] City city)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdId = await _cityService.CreateAsync(city);
        return CreatedAtAction(nameof(GetById), new { id = createdId }, city);
    }

    // PUT api/city
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] City city)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _cityService.UpdateAsync(city);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    // DELETE api/city/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _cityService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
