using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonsterHunterAPI.Models;

namespace MonsterHunterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly MonsterHunterDBContext _context;
        // added following line
        //private static readonly HttpClient client = new HttpClient();


        public WeaponController(MonsterHunterDBContext context)
        {
            _context = context;
        }

        // GET: api/Weapon
        [HttpGet]
        public async Task<ActionResult<ResponseWeapon>> GetWeapons()
        {
            var weapons = await _context.Weapons.Include(w => w.WeaponType).ToListAsync();
            var response = new ResponseWeapon();

            response.statusCode = 404;
            response.statusDescription = "Item Not Found";

            if (weapons != null)
            {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.weapons.AddRange(weapons);
            }

            return response;
        }

        // GET: api/Weapon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseWeapon>> GetWeapon(int id)
        {
            var weapons = await _context.Weapons.Include(w => w.WeaponType)
                .FirstOrDefaultAsync(w => w.WeaponID == id);
            var response = new ResponseWeapon();

            response.statusCode = 404;
            response.statusDescription = "Item Not Found";

            if (weapons != null)
            {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.weapons.Add(weapons);
            }



            return response;
        }


        
        // PUT: api/Weapon/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseWeapon>> PutWeapon(int id, Weapon weapon)
        {
            var response = new ResponseWeapon();

            response.statusCode = 404;
            response.statusDescription = "ID Not Found";

            if (id != weapon.WeaponID)
            {
                response.statusCode = 400;
                response.statusDescription = "Bad Inputs, weaponID doesn't match";
                return response;
            }


            if (weapon.WeaponType != null)
            {
                if (weapon.WeaponType.WeaponTypeID < 1 ||
                    weapon.WeaponType.WeaponTypeID > 14)
                {
                    response.statusCode = 400;
                    response.statusDescription = "waponTypeID out of range";
                    return response;
                }

                if ((_context.WeaponTypes?.Any(w => w.WeaponTypeID == weapon.WeaponType.WeaponTypeID)).GetValueOrDefault())
                {
                    weapon.WeaponType = await _context.WeaponTypes
                        .FindAsync(weapon.WeaponType.WeaponTypeID);
                    // map weaponType to this weapon
                }
            }


            _context.Entry(weapon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeaponExists(id))
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
            response.weapons.Add(weapon);

            return response;
        }
        



        // POST: api/Weapon
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResponseWeapon>> PostWeapon(Weapon weapon)
        {
            var response = new ResponseWeapon();

            response.statusCode = 404;
            response.statusDescription = "Entity Set Weapon Not Found";

            if (_context.Weapons != null)
            {
                if (weapon.WeaponType == null)
                {
                    response.statusCode = 400;
                    response.statusDescription = "waponType cannot be null";
                    return response;
                }
                
                if (weapon.WeaponType.WeaponTypeID < 1 ||
                        weapon.WeaponType.WeaponTypeID > 14)
                {
                    response.statusCode = 400;
                    response.statusDescription = "waponTypeID out of range";
                    return response;
                }

                if ((_context.WeaponTypes?.Any(w => w.WeaponTypeID == weapon.WeaponType.WeaponTypeID)).GetValueOrDefault())
                {
                    weapon.WeaponType = await _context.WeaponTypes
                        .FindAsync(weapon.WeaponType.WeaponTypeID);
                    // map weaponType to this weapon
                }

                    
                
             
                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();




                response.statusCode = 201;
                response.statusDescription = "Item Created";
                response.weapons.Add(weapon);
            }



            return response;
        }



        // DELETE: api/Weapon/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseWeapon>> DeleteWeapon(int id)
        {
            var response = new ResponseWeapon();

            response.statusCode = 404;
            response.statusDescription = "Not Found";

            if (_context.Weapons != null)
            {
                var weapon = await _context.Weapons.Include(w => w.WeaponType)
                    .FirstOrDefaultAsync(w => w.WeaponID == id);
                if (weapon != null)
                {
                    var player = await _context.Players.Include(p => p.Weapon)
                        .Include(p => p.Weapon.WeaponType)
                        .FirstOrDefaultAsync(p => p.Weapon.WeaponID == id);

                    if (player != null)
                    { // if there's a player holding this weapon, set player.weapon to null
                        player.Weapon = null;
                        _context.Weapons.Remove(weapon);
                        _context.Entry(player).State = EntityState.Modified;

                        try
                        {
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!WeaponExists(id))
                            {
                                return response;
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    else // if no player has that weapon
                    {
                        _context.Weapons.Remove(weapon);
                        await _context.SaveChangesAsync();
                    }


                    response.statusCode = 204;
                    response.statusDescription = "Item Deleted";
                    response.weapons.Add(weapon);

                }


            }


            return response;
        }




        private bool WeaponExists(int id)
        {
            return (_context.Weapons?.Any(e => e.WeaponID == id)).GetValueOrDefault();
        }
    }
}
