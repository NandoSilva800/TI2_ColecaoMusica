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
    
    
    
    public class ArtistasAPIController : ControllerBase
    {
        private readonly Colecao_MusicaBD _context;

        public ArtistasAPIController(Colecao_MusicaBD context)
        {
            _context = context;
        }

        // GET: api/ArtistasAPI
        /// <summary>
        /// Método para listar os dados de todos os artistas existentes na BD
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListaArtistasAPIViewModel>>> GetArtistas()
        {
            var listaArtistas = await _context.Artistas
                                                 .Select(r => new ListaArtistasAPIViewModel
                                                 {
                                                     IdArtista = r.Id,
                                                     NomeArtista = r.Nome
                                                 })
                                                 .OrderBy(r => r.NomeArtista)
                                                 .ToListAsync();

            return listaArtistas;
        }

      

        // GET: api/ArtistasAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artistas>> GetArtistas(int id)
        {
            var artistas = await _context.Artistas.FindAsync(id);

            if (artistas == null)
            {
                return NotFound();
            }

            return artistas;
        }

        // PUT: api/ArtistasAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtistas(int id, Artistas artistas)
        {
            if (id != artistas.Id)
            {
                return BadRequest();
            }

            _context.Entry(artistas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistasExists(id))
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

        // POST: api/ArtistasAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artistas>> PostArtistas(Artistas artistas)
        {
            _context.Artistas.Add(artistas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtistas", new { id = artistas.Id }, artistas);
        }

        // DELETE: api/ArtistasAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtistas(int id)
        {
            var artistas = await _context.Artistas.FindAsync(id);
            if (artistas == null)
            {
                return NotFound();
            }

            _context.Artistas.Remove(artistas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistasExists(int id)
        {
            return _context.Artistas.Any(e => e.Id == id);
        }
    }
}
