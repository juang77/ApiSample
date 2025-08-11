using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ApiSample.Models
{
    public class Country
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public List<City> Cities { get; init; } = new();

        public Country(int id, string name, List<City>? cities = null) 
        {
            Id = id;
            Name = name;
            Cities = cities ?? new List<City>();
        }

        public Country() { }
    }
}
