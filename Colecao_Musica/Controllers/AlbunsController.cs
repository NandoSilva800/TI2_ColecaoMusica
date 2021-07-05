using Colecao_Musica.Data;
using Colecao_Musica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Colecao_Musica.Controllers { 

    /// <summary>
    /// Controller para efetuar a gestão de Albuns de musica
    /// </summary>
    [Authorize]// Só acessivel se autenticado
    [Authorize(Roles = "Artista")]
    public class AlbunsController : Controller
    {
        /// <summary>
        /// Atributo que referencia a BD do projeto
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


        public AlbunsController(Colecao_MusicaBD context,
            IWebHostEnvironment dadosServidor,
            UserManager<IdentityUser> userManager) {
            _context = context;
            _dadosServidor = dadosServidor;
            _userManager = userManager;
        }




        // GET: Albuns
        /// <summary>
        /// Lista os albuns
        /// </summary>
        [Authorize(Roles = "Artista")]
        public async Task<IActionResult> Index() {

            //Quais os albuns do artista que se autenticou
            var listaAlbuns = await (from a in _context.Albuns
                                     join r in _context.Artistas on a.ArtistasFK equals r.Id
                                     where r.UserNameId == _userManager.GetUserId(User)
                                     select a)
                                    .OrderBy(a => a.Titulo)
                                    .ToListAsync();

            //variavel onde guarda o valor do atributo 'género'
            var colecaoAlbuns = await _context.Albuns.Include(a => a.Genero).ToListAsync();

            return View(listaAlbuns);
        }



        // GET: Albuns/Details/5
        public async Task<IActionResult> Details(int? id) {

            if (id == null) {
                return NotFound();
            }

            var album = await _context.Albuns
                        .Include(a => a.Artista)
                        .Include(a => a.Genero)
                        .FirstOrDefaultAsync(a => a.Id == id);

            if (album == null) {
                return NotFound();
            }
            return View(album);
        }



        // GET: Albuns/Create
        [Authorize(Roles = "Artista")]
        public IActionResult Create() {

            //Prepara os dados do atributo 'género' para uma Dropdown
            ViewData["GenerosFK"] = new SelectList(_context.Generos, "Id", "Designacao");
            return View();
        }


        // POST: Albuns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Artista")]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Duracao,NrFaixas,Ano,Editora,Cover,GenerosFK")] Albuns album, IFormFile albumCover, string[] MusicaSelecionada, Musicas Musica) {

            if (albumCover == null) {
                // se aqui entro, não há cover
                // notificar o utilizador que há um erro
                ModelState.AddModelError("", "Deve selecionar uma ficheiro...");

                // devolver o controlo à View
                // prepara os dados a serem enviados para a View
                // para a Dropdown
                //ViewData["GenerosFK"] = new SelectList(_context.Generos, "Id", "Designacao", album.GenerosFK);

                return View(album);
            }

            // há ficheiro. Mas, será do tipo correto (jpg/jpeg, png)?
            if (albumCover.ContentType == "image/png" || albumCover.ContentType == "image/jpeg") {

                // o ficheiro é bom

                // definir o nome do ficheiro
                string nomeCover = "";
                Guid g;
                g = Guid.NewGuid();
                nomeCover = album.ArtistasFK + "_" + g.ToString();
                string extensaoCover = Path.GetExtension(albumCover.FileName).ToLower();
                nomeCover = nomeCover + extensaoCover;

                // associar ao objeto 'album' o nome do ficheiro da imagem do cover
                album.Cover = nomeCover;
            }
            else
            {
                // se há ficheiro, mas não o cover
                ModelState.AddModelError("", "Deve selecionar um ficheiro...");


                // prepara os dados a serem enviados para a View para a Dropdown
                ViewData["GenerosFK"] = new SelectList(_context.Generos, "Id", "Designacao", album.GenerosFK);
                return View();
            }



            //Atribui ao objeto 'album' a lista de albuns do artista que está ligado
            album.ArtistasFK = (await _context.Artistas
                .Where(a => a.UserNameId == _userManager.GetUserId(User)).FirstOrDefaultAsync()).Id;


            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();

                // vou guardar o ficheiro no disco rígido do servidor
                // determinar onde guardar o ficheiro
                string caminhoAteAoFichCover = _dadosServidor.WebRootPath;
                caminhoAteAoFichCover = Path.Combine(caminhoAteAoFichCover, "coverAlbum", album.Cover);
                // guardar o ficheiro no Disco Rígido
                using var stream = new FileStream(caminhoAteAoFichCover, FileMode.Create);
                await albumCover.CopyToAsync(stream);

                ViewData["GenerosFK"] = new SelectList(_context.Generos, "Id", "Designacao");
                // redireciona a execução do código para a método Index    
                return RedirectToAction(nameof(Index));
            }



            ViewData["GenerosFK"] = new SelectList(_context.Generos, "Id", "Designacao", album.GenerosFK);

            return View(album);
        }


        // GET: Albuns/Edit/5
        public async Task<IActionResult> Edit(int? id) {

            if (id == null) {
                return NotFound();
            }


            var album = await _context.Albuns.FindAsync(id);

            if (album == null) {
                return NotFound();
            }

            ViewData["GenerosFK"] = new SelectList(_context.Generos, "Id", "Designacao", album.GenerosFK);

            return View(album);
        }


        // POST: Albuns/Edit/5
        // Para proteger os dados dos atributos de ataques - Bind
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Duracao,NrFaixas,Ano,Editora,Cover,GenerosFK,ArtistasFK")] Albuns album, IFormFile albumCover) {

            if (id != album.Id) {
                return NotFound();
            }


            //Atribui ao objeto 'album' a lista de albuns do artista que está ligado
            album.ArtistasFK = (await _context.Artistas
                .Where(a => a.UserNameId == _userManager
                .GetUserId(User))
                .FirstOrDefaultAsync()).Id;

            
            
            if (ModelState.IsValid) {

                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                catch (DbUpdateConcurrencyException)
                {

                    if (!AlbunsExists(album.Id))
                    {
                        return NotFound();
                    }

                    else
                    {

                        throw;
                    }
                }
                catch (Exception)
                {
                    // executar aqui as instruções para processar o erro...
                    // no mínimo, enviar uma mensagem para a View
                    ModelState.AddModelError("", "ocorreu um erro não identificado...");


                    return RedirectToAction(nameof(Index));
                }
            }


            ViewData["GenerosFK"] = new SelectList(_context.Generos, "Id", "Designacao", album.GenerosFK);

            return View(album);

        }


         // GET: Albuns/Delete/5
         public async Task<IActionResult> Delete(int? id) {

                        if (id == null) {
                            return NotFound();
                        }

                        var albuns = await _context.Albuns
                            .Include(a => a.Artista)
                            .Include(a => a.Genero)
                            .FirstOrDefaultAsync(m => m.Id == id);

                        if (albuns == null) {
                            return NotFound();
                        }


                        return View(albuns);
         }

         // POST: Albuns/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id) {

              var albuns = await _context.Albuns.FindAsync(id);
               _context.Albuns.Remove(albuns);
              await _context.SaveChangesAsync();

                        /*
                         * se apago o registo, preciso de apagar o ficheiro a ele associado... 
                         */

               return RedirectToAction(nameof(Index));
         }

                    private bool AlbunsExists(int id) {
                        return _context.Albuns.Any(e => e.Id == id);
                    }

         


            
     }
}



