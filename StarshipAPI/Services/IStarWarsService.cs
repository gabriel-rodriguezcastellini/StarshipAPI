using StarshipAPI.Models;

namespace StarshipAPI.Services
{
    public interface IStarWarsService
    {
        Task<IEnumerable<Starship>> GetStarshipsAsync();
    }
}
