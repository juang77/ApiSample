namespace ApiSample.Models
{
    public class City
    {
        public int Id { get; init; }

        public int CountryId { get; init; }

        public string Name { get; init; } = string.Empty;
        
        public City(int id, int countryId, string name)
        {
            Id = id;
            CountryId = countryId;
            Name = name;
        }

        public City() { }
    }
}
