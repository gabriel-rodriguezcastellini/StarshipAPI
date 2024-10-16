using Newtonsoft.Json;
using StarshipAPI.Models;

namespace StarshipAPI.Services
{
    public class StarWarsService(HttpClient httpClient, IConfiguration configuration, ILogger<StarWarsService> logger) : IStarWarsService
    {
        private readonly string _baseUrl = configuration["StarWarsApi:BaseUrl"] ?? throw new InvalidOperationException("Star Wars API base URL is not configured.");

        public async Task<IEnumerable<Starship>> GetStarshipsAsync()
        {
            string endpoint = "starships/";
            try
            {
                string response = await httpClient.GetStringAsync($"{_baseUrl}{endpoint}");
                StarshipResponse? starships = JsonConvert.DeserializeObject<StarshipResponse>(response);
                return starships?.Results ?? [];
            }
            catch (HttpRequestException httpEx)
            {
                logger.LogError(httpEx, "An error occurred while making the HTTP request.");
                throw new StarWarsServiceException("An error occurred while fetching starships from the Star Wars API.", httpEx);
            }
            catch (JsonException jsonEx)
            {
                logger.LogError(jsonEx, "An error occurred while deserializing the JSON response.");
                throw new StarWarsServiceException("An error occurred while processing the starships data.", jsonEx);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occurred.");
                throw new StarWarsServiceException("An unexpected error occurred while fetching starships.", ex);
            }
        }
    }

    public class StarshipResponse
    {
        public required IEnumerable<Starship> Results { get; set; }
    }

    public class StarWarsServiceException(string message, Exception innerException) : Exception(message, innerException)
    {
    }
}
