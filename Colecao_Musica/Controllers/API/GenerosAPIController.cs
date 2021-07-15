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
    public class GenerosAPIController : ControllerBase
    {
        private readonly Colecao_MusicaBD _context;

        public GenerosAPIController(Colecao_MusicaBD context)
        {
            _context = context;
        }

        // GET: api/GenerosAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListaGenerosAPIViewModel>>> GetGeneros()
        {
            var listaGeneros = await _context.Generos
                                                 .Select(g => new ListaGenerosAPIViewModel
                                                 {
                                                     IdGenero = g.Id,
                                                     GeneroAlbum = g.Designacao
                                                 })
                                                 .ToListAsync();

            return listaGeneros;

           
        }

        // GET: api/GenerosAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Generos>> GetGeneros(int id)
        {
            var generos = await _context.Generos.FindAsync(id);

            if (generos == null)
            {
                return NotFound();
            }

            return generos;
        }

        // PUT: api/GenerosAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeneros(int id, Generos generos)
        {
            if (id != generos.Id)
            {
                return BadRequest();
            }

            _context.Entry(generos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenerosExists(id))
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

        // POST: api/GenerosAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Generos>> PostGeneros(Generos generos)
        {
            _context.Generos.Add(generos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGeneros", new { id = generos.Id }, generos);
        }

        // DELETE: api/GenerosAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneros(int id)
        {
            var generos = await _context.Generos.FindAsync(id);
            if (generos == null)
            {
                return NotFound();
            }

            _context.Generos.Remove(generos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenerosExists(int id)
        {
            return _context.Generos.Any(e => e.Id == id);
        }
    }
}
