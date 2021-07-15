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
    public class MusicasAPIController : ControllerBase
    {
        private readonly Colecao_MusicaBD _context;

        public MusicasAPIController(Colecao_MusicaBD context)
        {
            _context = context;
        }

        // GET: api/MusicasAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Musicas>>> GetMusicas()
        {
            return await _context.Musicas.ToListAsync();
        }

        // GET: api/MusicasAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Musicas>> GetMusicas(int id)
        {
            var musicas = await _context.Musicas.FindAsync(id);

            if (musicas == null)
            {
                return NotFound();
            }

            return musicas;
        }

        // PUT: api/MusicasAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMusicas(int id, Musicas musicas)
        {
            if (id != musicas.Id)
            {
                return BadRequest();
            }

            _context.Entry(musicas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusicasExists(id))
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

        // POST: api/MusicasAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Musicas>> PostMusicas(Musicas musicas)
        {
            _context.Musicas.Add(musicas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMusicas", new { id = musicas.Id }, musicas);
        }

        // DELETE: api/MusicasAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusicas(int id)
        {
            var musicas = await _context.Musicas.FindAsync(id);
            if (musicas == null)
            {
                return NotFound();
            }

            _context.Musicas.Remove(musicas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MusicasExists(int id)
        {
            return _context.Musicas.Any(e => e.Id == id);
        }
    }
}


