using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Colecao_Musica.Data;
using Colecao_Musica.Models;

namespace Colecao_Musica.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbunsAPIController : ControllerBase
    {
        private readonly Colecao_MusicaBD _context;

        public AlbunsAPIController(Colecao_MusicaBD context)
        {
            _context = context;
        }

        // GET: api/AlbunsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Albuns>>> GetAlbuns()
        {
            return await _context.Albuns.ToListAsync();
        }

        // GET: api/AlbunsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Albuns>> GetAlbuns(int id)
        {
            var albuns = await _context.Albuns.FindAsync(id);

            if (albuns == null)
            {
                return NotFound();
            }

            return albuns;
        }

        // PUT: api/AlbunsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbuns(int id, Albuns albuns)
        {
            if (id != albuns.Id)
            {
                return BadRequest();
            }

            _context.Entry(albuns).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbunsExists(id))
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

        // POST: api/AlbunsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Albuns>> PostAlbuns(Albuns albuns)
        {
            _context.Albuns.Add(albuns);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlbuns", new { id = albuns.Id }, albuns);
        }

        // DELETE: api/AlbunsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbuns(int id)
        {
            var albuns = await _context.Albuns.FindAsync(id);
            if (albuns == null)
            {
                return NotFound();
            }

            _context.Albuns.Remove(albuns);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlbunsExists(int id)
        {
            return _context.Albuns.Any(e => e.Id == id);
        }
    }
}
