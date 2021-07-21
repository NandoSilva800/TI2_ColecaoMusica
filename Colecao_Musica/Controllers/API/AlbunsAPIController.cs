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
    
    
    
    
    public class AlbunsAPIController : ControllerBase {
        private readonly Colecao_MusicaBD _context;

        public AlbunsAPIController(Colecao_MusicaBD context)
        {
            _context = context;
        }

        // GET: api/AlbunsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListaAlbunsAPIViewModel>>> GetAlbuns()
        {
            var listaAlbuns = await _context.Albuns
                                                 .Include(a => a.Artista)
                                                 .Select(a=> new ListaAlbunsAPIViewModel {
                                                                IdAlbum = a.Id,
                                                                TituloAlbum = a.Titulo,
                                                                DuracaoAlbum =a.Duracao,
                                                                NrFaixasAlbum = a.NrFaixas,
                                                                AnoAlbum = a.Ano,
                                                                EditoraAlbum = a.Editora,
                                                                CoverAlbum = a.Cover,
                                                                GeneroAlbum = a.Genero.Designacao,
                                                                NomeArtista = a.Artista.Nome

                                                 })
                                                 .ToListAsync();

            return listaAlbuns;
        }

        // GET: api/AlbunsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListaAlbunsAPIViewModel>> GetAlbuns(int id)
        {
            var albuns = await _context.Albuns
                .Where(a => a.Id == id)
                 .Select(a => new ListaAlbunsAPIViewModel
                 {
                     IdAlbum = a.Id,
                     TituloAlbum = a.Titulo,
                     DuracaoAlbum = a.Duracao,
                     NrFaixasAlbum = a.NrFaixas,
                     AnoAlbum = a.Ano,
                     EditoraAlbum = a.Editora,
                     CoverAlbum = a.Cover,
                     GeneroAlbum = a.Genero.Designacao,
                     NomeArtista = a.Artista.Nome

                 })
                .FirstOrDefaultAsync();

            if (albuns == null)
            {
                return NotFound();
            }

            return albuns;
        }

        // PUT: api/AlbunsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbuns(int id, Albuns album)
        {
            if (id != album.Id)
            {
                return BadRequest();
            }

            _context.Entry(album).State = EntityState.Modified;

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
        public async Task<ActionResult<Albuns>> PostAlbuns(Albuns album)
        {

            // esta instrução é apenas usada para não se criar uma exceção no código
            // deverá ser apagada quando se concretizar o trabalho real
            album.Cover = "";

            _context.Albuns.Add(album);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlbuns", new { id = album.Id }, album);
        }


        // POST: Albuns/Delete/5
    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var album = await _context.Albuns.FindAsync(id);
            _context.Albuns.Remove(album);
            await _context.SaveChangesAsync();

            /*
//Apago o registo, corrigir o return 
            */

            return RedirectToAction(nameof(Index));
        }

        private bool AlbunsExists(int id)
        {
            return _context.Albuns.Any(e => e.Id == id);
        }





    }
}

