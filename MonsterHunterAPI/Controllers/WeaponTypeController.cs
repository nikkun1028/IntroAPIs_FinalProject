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
    public class WeaponTypeController : ControllerBase
    {
        private readonly MonsterHunterDBContext _context;

        public WeaponTypeController(MonsterHunterDBContext context)
        {
            _context = context;
        }

        // GET: api/WeaponType
        [HttpGet]
        public async Task<ActionResult<ResponseWeaponType>> GetWeaponTypes()
        {
            var weaponTypes = await _context.WeaponTypes.ToListAsync();
            var response = new ResponseWeaponType();

            response.statusCode = 404;
            response.statusDescription = "Item Not Found";

            if (weaponTypes != null)
            {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.weaponTypes.AddRange(weaponTypes);
            }

            return response;
        }

        // GET: api/WeaponType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseWeaponType>> GetWeaponType(int id)
        {
            var weaponTypes = await _context.WeaponTypes.FindAsync(id);
            var response = new ResponseWeaponType();

            response.statusCode = 404;
            response.statusDescription = "Item Not Found";

            if (weaponTypes != null)
            {
                response.statusCode = 200;
                response.statusDescription = "GET successful";
                response.weaponTypes.Add(weaponTypes);
            }



            return response;
        }

        // PUT: api/WeaponType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeaponType(int id, WeaponType weaponType)
        {
            if (id != weaponType.WeaponTypeID)
            {
                return BadRequest();
            }

            _context.Entry(weaponType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeaponTypeExists(id))
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

        // POST: api/WeaponType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WeaponType>> PostWeaponType(WeaponType weaponType)
        {
          if (_context.WeaponTypes == null)
          {
              return Problem("Entity set 'MonsterHunterDBContext.WeaponTypes'  is null.");
          }
            _context.WeaponTypes.Add(weaponType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeaponType", new { id = weaponType.WeaponTypeID }, weaponType);
        }

        // DELETE: api/WeaponType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeaponType(int id)
        {
            if (_context.WeaponTypes == null)
            {
                return NotFound();
            }
            var weaponType = await _context.WeaponTypes.FindAsync(id);
            if (weaponType == null)
            {
                return NotFound();
            }

            _context.WeaponTypes.Remove(weaponType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeaponTypeExists(int id)
        {
            return (_context.WeaponTypes?.Any(e => e.WeaponTypeID == id)).GetValueOrDefault();
        }

        */
    }
}
