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
            //var player = await _context.Players.ToListAsync();
            var player = await _context.Players.Include(p => p.Weapon)
                .Include(p => p.Weapon.WeaponType).ToListAsync();

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
            var player = await _context.Players.Include(p => p.Weapon)
                .Include(p => p.Weapon.WeaponType).FirstOrDefaultAsync(p => p.PlayerID == id);
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
        public async Task<ActionResult<ResponsePlayer>> PutPlayer(int id, Player player)
        {
            var response = new ResponsePlayer();

            response.statusCode = 404;
            response.statusDescription = "ID Not Found";

            if (id != player.PlayerID)
            {
                response.statusCode = 400;
                response.statusDescription = "Bad Inputs, playerID doesn't match";
                return response;
            }


            // update weapon when it's not null
            if (player.Weapon != null)
            {

                if ((_context.Weapons?.Any(w => w.WeaponID == player.Weapon.WeaponID)).GetValueOrDefault())
                {
                    player.Weapon = await _context.Weapons.Include(w => w.WeaponType)
                        .FirstOrDefaultAsync(w => w.WeaponID == player.Weapon.WeaponID);
                    // map weapon to this player if exist
                }
                else // when weaponID doesn't exist, create new weapon
                {
                    if (player.Weapon.WeaponID <= 0)
                    {
                        response.statusCode = 400;
                        response.statusDescription = "Bad Inputs, illegal waponID";
                        return response;
                    }

                    // check weaponType is in the range 1~14
                    if (player.Weapon.WeaponType == null)
                    {
                        response.statusCode = 400;
                        response.statusDescription = "waponType cannot be null";
                        return response;
                    }

                    if (player.Weapon.WeaponType.WeaponTypeID < 1 ||
                            player.Weapon.WeaponType.WeaponTypeID > 14)
                    {
                        response.statusCode = 400;
                        response.statusDescription = "waponTypeID out of range";
                        return response;
                    }

                    // map weaponType to this player's weapon
                    player.Weapon.WeaponType = await _context.WeaponTypes
                        .FindAsync(player.Weapon.WeaponType.WeaponTypeID);

                    // add new Weapon to database
                    _context.Weapons.Add(player.Weapon);
                }

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
                    return response;
                }
                else
                {
                    throw;
                }
            }


            response.statusCode = 204;
            response.statusDescription = "Item Updated";
            response.players.Add(player);

            return response;
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


                if (player.Weapon != null)
                {

                    if ((_context.Weapons?.Any(w => w.WeaponID == player.Weapon.WeaponID)).GetValueOrDefault())
                    {
                        player.Weapon = await _context.Weapons.Include(w => w.WeaponType)
                            .FirstOrDefaultAsync(w => w.WeaponID == player.Weapon.WeaponID);
                        // map weapon to this player
                    }
                    else // when weaponID doesn't exist, create new weapon
                    {

                        // check weaponType is in the range 1~14
                        if (player.Weapon.WeaponType == null)
                        {
                            response.statusCode = 400;
                            response.statusDescription = "waponType cannot be null";
                            return response;
                        }

                        if (player.Weapon.WeaponType.WeaponTypeID < 1 ||
                                player.Weapon.WeaponType.WeaponTypeID > 14)
                        {
                            response.statusCode = 400;
                            response.statusDescription = "waponTypeID out of range";
                            return response;
                        }

                        // map weaponType to this player's weapon
                        player.Weapon.WeaponType = await _context.WeaponTypes
                            .FindAsync(player.Weapon.WeaponType.WeaponTypeID);

                    }

                }


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
        public async Task<ActionResult<ResponsePlayer>> DeletePlayer(int id)
        {
            var response = new ResponsePlayer();

            response.statusCode = 404;
            response.statusDescription = "Not Found";

            if (_context.Players != null)
            {
               var player = await _context.Players.FindAsync(id);
               if (player != null)
               {

                    _context.Players.Remove(player);
                    await _context.SaveChangesAsync();

                    response.statusCode = 204;
                    response.statusDescription = "Item Deleted";
                    response.players.Add(player);

                }


            }
            

            return response;
        }





        private bool PlayerExists(int id)
        {
            return (_context.Players?.Any(e => e.PlayerID == id)).GetValueOrDefault();
        }
    }
}
