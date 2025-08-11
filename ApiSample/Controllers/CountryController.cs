using Microsoft.AspNetCore.Mvc;
using ApiSample.Services;
using ApiSample.Models;

namespace ApiSample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    // GET api/country
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var countries = await _countryService.GetAllAsync();
        return Ok(countries);
    }

    // GET api/country/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var country = await _countryService.GetByIdAsync(id);
        if (country == null)
            return NotFound();

        return Ok(country);
    }

    // POST api/country
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Country country)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdId = await _countryService.CreateAsync(country);
        return CreatedAtAction(nameof(GetById), new { id = createdId }, country);
    }

    // PUT api/country
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Country country)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _countryService.UpdateAsync(country);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    // DELETE api/country/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _countryService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
