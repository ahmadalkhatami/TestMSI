// Controllers/PlayersController.cs
using Microsoft.AspNetCore.Mvc;
using PlayersAPI.Models;
using PlayersAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers([FromQuery] string? birthplace)
        {
            var players = await _playerService.GetAllPlayersAsync(birthplace);
            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        [HttpPost]
        public async Task<ActionResult<Player>> CreatePlayer(Player player)
        {
            var createdPlayer = await _playerService.CreatePlayerAsync(player);
            return CreatedAtAction(nameof(GetPlayer), new { id = createdPlayer.Id }, createdPlayer);
        }
    }
}
