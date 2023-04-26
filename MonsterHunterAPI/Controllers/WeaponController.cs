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
        public async Task<IActionResult> PutWeapon(int id, Weapon weapon)
        {
            if (id != weapon.WeaponID)
            {
                return BadRequest();
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        

        // POST: api/Weapon
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Weapon>> PostWeapon(Weapon weapon)
        {
          if (_context.Weapons == null)
          {
              return Problem("Entity set 'MonsterHunterDBContext.Weapons'  is null.");
          }
            _context.Weapons.Add(weapon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeapon", new { id = weapon.WeaponID }, weapon);
        }

        // DELETE: api/Weapon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeapon(int id)
        {
            if (_context.Weapons == null)
            {
                return NotFound();
            }
            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }

            _context.Weapons.Remove(weapon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeaponExists(int id)
        {
            return (_context.Weapons?.Any(e => e.WeaponID == id)).GetValueOrDefault();
        }
    }
}
