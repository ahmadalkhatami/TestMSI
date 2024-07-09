// Services/PlayerService.cs
using Microsoft.EntityFrameworkCore;
using PlayersAPI.Data;
using PlayersAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayersAPI.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly PlayerContext _context;

        public PlayerService(PlayerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync(string birthplace)
        {
            var query = _context.Players.AsQueryable();

            if (!string.IsNullOrEmpty(birthplace))
            {
                query = query.Where(p => p.BirthPlace == birthplace);
            }

            return await query.ToListAsync();
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            return await _context.Players.FindAsync(id);
        }

        public async Task<Player> CreatePlayerAsync(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }
    }
}
