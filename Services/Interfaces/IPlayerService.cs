using System.Collections.Generic;
using System.Threading.Tasks;
using PlayersAPI.Models;

namespace PlayersAPI.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAllPlayersAsync(string birthplace);
        Task<Player> GetPlayerByIdAsync(int id);
        Task<Player> CreatePlayerAsync(Player player);
    }
}
