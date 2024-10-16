namespace StarshipAPI.Models
{
    public class Starship
    {
        public required string Name { get; set; }
        public required string Manufacturer { get; set; }

        public List<string> GetManufacturers()
        {
            return Manufacturer?.Split(',').Select(m => m.Trim()).ToList() ?? [];
        }
    }
}
