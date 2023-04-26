using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonsterHunterAPI.Models;

namespace MonsterHunterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly MonsterHunterDBContext _context;

        public PlayerController(MonsterHunterDBContext context)
        {
            _context = context;
        }

        // GET: api/Player
        [HttpGet]
        public async Task<ActionResult<ResponsePlayer>> GetPlayers()
        {
            var player = await _context.Players.ToListAsync();
            var response = new ResponsePlayer();

            response.statusCode = 404;
            response.statusDescription = "Item Not Found";

            if (player != null) {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.players.AddRange(player);
            }

            return response;
        }

        // GET: api/Player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponsePlayer>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            var response = new ResponsePlayer();

            response.statusCode = 404;
            response.statusDescription = "Item Not Found";

          if (player != null)
          {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.players.Add(player);
          }



            return response;
        }

        
        // PUT: api/Player/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.PlayerID)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        

        // POST: api/Player
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResponsePlayer>> PostPlayer(Player player)
        {
            var response = new ResponsePlayer();

            response.statusCode = 404;
            response.statusDescription = "Entity Set Player Not Found";

            if (_context.Players != null)
            {
                //return Problem("Entity set 'MonsterHunterDBContext.WeaponTypes'  is null.");
                _context.Players.Add(player);
                await _context.SaveChangesAsync();


                response.statusCode = 201;
                response.statusDescription = "Item Created";
                response.players.Add(player);
            }
           
            

            return response;


        }


        // DELETE: api/Player/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            if (_context.Players == null)
            {
                return NotFound();
            }
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerExists(int id)
        {
            return (_context.Players?.Any(e => e.PlayerID == id)).GetValueOrDefault();
        }
    }
}
