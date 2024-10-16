using Newtonsoft.Json;
using StarshipAPI.Models;

namespace StarshipAPI.Services
{
    public class StarWarsService(HttpClient httpClient, IConfiguration configuration) : IStarWarsService
    {
        private readonly string _baseUrl = configuration["StarWarsApi:BaseUrl"] ?? throw new InvalidOperationException("Star Wars API base URL is not configured.");

        public async Task<IEnumerable<Starship>> GetStarshipsAsync()
        {
            string endpoint = "starships/";
            string response = await httpClient.GetStringAsync($"{_baseUrl}{endpoint}");
            StarshipResponse? starships = JsonConvert.DeserializeObject<StarshipResponse>(response);
            return starships?.Results ?? [];
        }
    }

    public class StarshipResponse
    {
        public required IEnumerable<Starship> Results { get; set; }
    }
}
