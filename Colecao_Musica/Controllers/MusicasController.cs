using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Colecao_Musica.Data;
using Colecao_Musica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Colecao_Musica.Controllers
{
    /// <summary>
    /// Controller para efetuar a gestão de músicas
    /// </summary>
    [Authorize]// Só acessivel se autenticado
    public class MusicasController : Controller
    {
        /// <summary>
        /// atributo que referencia a BD do projeto
        /// </summary>
        private readonly Colecao_MusicaBD _context;

        /// <summary>
        /// Atributo que guarda nele os dados do Servidor
        /// </summary>
        private readonly IWebHostEnvironment _dadosServidor;

        /// <summary>
        /// Atributo que irá receber todos os dados referentes à
        /// pessoa q se autenticou no sistema
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;


        public MusicasController(Colecao_MusicaBD context,
            IWebHostEnvironment dadosServidor,
            UserManager<IdentityUser> userManager){
            _context = context;
            _dadosServidor = dadosServidor;
            _userManager = userManager;
        }

        // GET: Musicas
        /// <summary>
        /// Lista as músicas
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()  {
   
             var listaMusicas = await (from m in _context.Musicas
                               join r in _context.Artistas on m.ArtistasFK equals r.Id
                               where r.UserNameId == _userManager.GetUserId(User)
                               select m)
                               .OrderBy(m => m.Titulo)
                               .ToListAsync();
          
            return View(listaMusicas);
        }
       

    

        // GET: Musicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) {
                return NotFound();
            }

            var musica = await _context.Musicas
                .Include(m => m.Artista)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (musica == null) {
                return NotFound();
            }

            return View(musica);
        }


        // GET: Musicas/Create
        public IActionResult Create()
        {
            //ViewData["ArtistasFK"] = new SelectList(_context.Artistas, "Id", "Nome");
            return View();
        }

        // POST: Musicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Duracao,Ano,Compositor, ArtistasFK")] Musicas musica) {

            //Atribui ao objeto 'musica' a lista de albuns do artista que está ligado
            musica.ArtistasFK = (await _context.Artistas
                .Where(m => m.UserNameId == _userManager.GetUserId(User)).FirstOrDefaultAsync()).Id;
            
            if (ModelState.IsValid) {

                _context.Add(musica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(musica);
        }

        // GET: Musicas/Edit/5
        public async Task<IActionResult> Edit(int? id) {

            if (id == null) {
                return NotFound();
            }

            var musica = await _context.Musicas.FindAsync(id);

            if (musica == null) { 
                return NotFound();
            }
            
            //ViewData["ArtistasFK"] = new SelectList(_context.Artistas, "Id", "Nome", musicas.ArtistasFK);
            return View(musica);
        }

        // POST: Musicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Duracao,Ano,Compositor,ArtistasFK")] Musicas musica) {
            
            if (id != musica.Id) {
                return NotFound();
            }

            //Atribui ao objeto 'musica' a lista de musicas do artista que está ligado
            musica.ArtistasFK = (await _context.Artistas.Where(m => m.UserNameId == _userManager.GetUserId(User)).FirstOrDefaultAsync()).Id;

            if (ModelState.IsValid) {
                try {

                    _context.Update(musica);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException) {

                    if (!MusicasExists(musica.Id)) {
                        return NotFound();
                    }

                    else {

                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(musica);
        }

        // GET: Musicas/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            
            if (id == null) {
                return NotFound();
            }

            var musica = await _context.Musicas
                .Include(m => m.Artista)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (musica == null) {
                return NotFound();
            }

            return View(musica);
        }

        // POST: Musicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musica = await _context.Musicas.FindAsync(id);
            _context.Musicas.Remove(musica);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool MusicasExists(int id)
        {
            return _context.Musicas.Any(e => e.Id == id);
        }
    }
}
