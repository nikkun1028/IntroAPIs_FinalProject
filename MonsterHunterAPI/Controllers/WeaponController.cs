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
            var weapons = await _context.Weapons.ToListAsync();
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
            var weapons = await _context.Weapons.FindAsync(id);
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
                response.statusDescription = "Bad Request, Bad Inputs";
                return response;
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
                //return Problem("Entity set 'MonsterHunterDBContext.WeaponTypes'  is null.");
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
                var weapon = await _context.Weapons.FindAsync(id);
                if (weapon != null)
                {

                    _context.Weapons.Remove(weapon);
                    await _context.SaveChangesAsync();

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
