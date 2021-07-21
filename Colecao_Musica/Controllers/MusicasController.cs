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
    [Authorize(Roles = "Artista")]
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
                                UserManager<IdentityUser> userManager)
        {
            _context = context;
            _dadosServidor = dadosServidor;
            _userManager = userManager;
        }


        // GET: Musicas
        /// <summary>
        /// Lista as músicas
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Artista")]
        public async Task<IActionResult> Index(int? id)
        {
            //Lista de musicas que pertencem ao artista autenticado
            var listaMusicas = await (from m in _context.Musicas
                                      join r in _context.Artistas on m.ArtistaFK equals r.Id
                                      where r.UserNameId == _userManager.GetUserId(User)
                                      select m)
                                      .OrderBy(m => m.Titulo)
                                      .ToListAsync();

            return View(listaMusicas);
        }


        // GET: Musicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Identifica o Id do artista autenticado
            int ArtistaId = (await _context.Artistas
                             .Where(a => a.UserNameId == _userManager.GetUserId(User))
                             .FirstOrDefaultAsync()).Id;

            //procura a música cujo o Id é fornecido
            var musica = await _context.Musicas
                        .Where(m => m.Id == id && m.ArtistaFK == ArtistaId)
                        .Include(m => m.ListaDeAlbuns)
                        .FirstOrDefaultAsync();


            if (musica == null)
            {
                return NotFound();
            }

            ViewBag.listaDeAlbuns = _context.Albuns
                                    .Where(a => a.ArtistaFK == ArtistaId)
                                    .OrderBy(m => m.Titulo).ToList();

            return View(musica);
        }


        // GET: Musicas/Create
        [Authorize(Roles = "Artista")]
        public async Task<IActionResult> Create()
        {
            //Identifica o Id do artista autenticado
            int ArtistaId = (await _context.Artistas
                             .Where(a => a.UserNameId == _userManager.GetUserId(User))
                             .FirstOrDefaultAsync()).Id;

            ViewBag.ListaAlbuns = _context.Albuns
                                .Where(a => a.ArtistaFK == ArtistaId)
                                .OrderBy(m => m.Titulo).ToList();

            return View();
        }


        // POST: Musicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Artista")]
        public async Task<IActionResult> Create([Bind("Titulo,Duracao,Ano,Compositor")]
                                                Musicas musica, int[] AlbumSelecionado)
        {
            //Identifica o Id do artista autenticado
            int ArtistaId = (await _context.Artistas
                            .Where(a => a.UserNameId == _userManager.GetUserId(User))
                            .FirstOrDefaultAsync()).Id;

            // <- Atribuição de um album a uma música ->
            // avalia se o array com a lista de músicas selecionadas associadas ao album está vazia ou não
            if (AlbumSelecionado.Length == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar pelo menos um álbum que pertence a música.");

                // gerar a lista de albuns que podem ser associados à música
                ViewBag.ListaAlbuns = _context.Albuns
                                      .Where(a => a.ArtistaFK == ArtistaId)
                                      .OrderBy(m => m.Titulo).ToList();

                // devolver controlo à View
                return View(musica);
            }

            // criar uma lista com os objetos escolhidos dos albuns
            List<Albuns> ListaAlbunsSelecionados = new List<Albuns>();

            // Para cada objeto escolhido..
            foreach (int item in AlbumSelecionado)
            {
                //procurar o album
                Albuns album = await _context.Albuns.FindAsync(item);

                // adicionar a Categoria à lista
                ListaAlbunsSelecionados.Add(album);
            }

            // adicionar a lista ao objeto
            musica.ListaDeAlbuns = ListaAlbunsSelecionados;

            //Atribui ao objeto 'musica' a lista de albuns do artista que está ligado
            musica.ArtistaFK = ArtistaId;

            if (ModelState.IsValid)
            {
                _context.Add(musica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // gerar a lista de albuns que podem ser associados à música
            ViewBag.ListaAlbuns = _context.Albuns
                                  .Where(a => a.ArtistaFK == ArtistaId)
                                  .OrderBy(m => m.Titulo).ToList();

            return View(musica);
        }


        // GET: Musicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            //Identifica o Id do artista autenticado
            int ArtistaId = (await _context.Artistas
                             .Where(a => a.UserNameId == _userManager.GetUserId(User))
                            .FirstOrDefaultAsync()).Id;

            //procura a musica cujo o Id é fornecido
            var musica = await _context.Musicas
                        .Where(m => m.Id == id && m.ArtistaFK == ArtistaId)
                        .Include(m => m.ListaDeAlbuns)
                        .FirstOrDefaultAsync();

            if (musica == null)
            {
                return NotFound();
            }

            ViewBag.listaDeAlbuns = _context.Albuns
                                    .Where(a => a.ArtistaFK == ArtistaId)
                                    .OrderBy(m => m.Titulo).ToList();

            return View(musica);
        }


        // POST: Musicas/Edit/5
        // Para proteger os dados dos atributos de ataques - Bind
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Duracao,Ano,Compositor,ArtistasFK")]
                                              Musicas musica, int[] albumSelecionado)
        {

            if (id != musica.Id)
            {
                return NotFound();
            }

            //Identifica o Id do artista autenticado
            int ArtistaId = (await _context.Artistas
                             .Where(a => a.UserNameId == _userManager.GetUserId(User))
                             .FirstOrDefaultAsync()).Id;

            // <- Atribuição de um album a uma música ->
            // avalia se o array com a lista de músicas selecionadas associadas ao album está vazia ou não
            if (albumSelecionado.Length == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar pelo menos um álbum que pertence a esta música.");

                // gerar a lista de albuns que podem ser associados à música
                ViewBag.listaDeAlbuns = _context.Albuns
                                        .Where(a => a.ArtistaFK == ArtistaId)
                                        .OrderBy(m => m.Titulo).ToList();

                // devolver controlo à View
                return View(musica);
            }

            //******************Avaliar se o utilizador alterou algum album associado à musica*****************

            //Dados guardados das músicas
            var musicaAntiga = await _context.Musicas
                              .Where(m => m.Id == id)
                              .Include(m => m.ListaDeAlbuns)
                              .FirstOrDefaultAsync();

            // Obtem a lista de Id's dos albuns associados a música antes de ser editado
            var listaOldAlbuns = musicaAntiga.ListaDeAlbuns
                                .Select(a => a.Id)
                                .ToList();

            // adicionadas -> lista de albuns adicionados
            // retiradas   -> lista de albuns retirados
            var adicionados = albumSelecionado.Except(listaOldAlbuns);
            var retirados = listaOldAlbuns.Except(albumSelecionado.ToList());

            // se algum album foi adicionado ou retirado
            // é necessário alterar a lista de albuns
            // associada à música
            if (adicionados.Any() || retirados.Any())
            {
                if (retirados.Any())
                {
                    // retirar o album 
                    foreach (int oldAlbum in retirados)
                    {
                        Albuns AlbumToRemove = await _context.Albuns
                                               .Where(a => a.Id == oldAlbum && a.ArtistaFK == ArtistaId)
                                               .FirstOrDefaultAsync();

                        musicaAntiga.ListaDeAlbuns.Remove(AlbumToRemove);
                    }
                }
                if (adicionados.Any())
                {
                    // adicionar o album
                    foreach (int newAlbum in adicionados)
                    {
                        Albuns AlbumToAdd = await _context.Albuns
                                            .Where(a => a.Id == newAlbum && a.ArtistaFK == ArtistaId)
                                            .FirstOrDefaultAsync();
                        if (AlbumToAdd != null)
                        {
                            musicaAntiga.ListaDeAlbuns.Add(AlbumToAdd);
                        }
                    }
                }
            }

            // transferir os dados recolhidos para o objeto que vai atualizar os dados na BD
            musicaAntiga.Ano = musica.Ano;
            musicaAntiga.Compositor = musica.Compositor;
            musicaAntiga.Duracao = musica.Duracao;
            musicaAntiga.Titulo = musica.Titulo;

            //Atribui ao objeto 'musica' a lista de musicas do artista que está ligado
            musicaAntiga.ArtistaFK = ArtistaId;

            //**************************************************************************************************

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musicaAntiga);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                catch (DbUpdateConcurrencyException)
                {

                    if (!MusicasExists(musicaAntiga.Id))
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
                }
            }

            // gerar a lista de albuns que podem ser associados à música
            ViewBag.listaDeAlbuns = _context.Albuns
                                    .Where(a => a.ArtistaFK == ArtistaId)
                                    .OrderBy(m => m.Titulo).ToList();

            return View(musicaAntiga);
        }

        // GET: Musicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            //Identifica o Id do artista autenticado
            int ArtistaId = (await _context.Artistas
                            .Where(a => a.UserNameId == _userManager.GetUserId(User))
                            .FirstOrDefaultAsync()).Id;

            //procura a música cujo o Id é fornecido
            var musica = await _context.Musicas
                        .Where(m => m.Id == id && m.ArtistaFK == ArtistaId)
                        .Include(m => m.ListaDeAlbuns)
                        .FirstOrDefaultAsync();

            if (musica == null)
            {
                return NotFound();
            }

            ViewBag.listaDeAlbuns = _context.Albuns
                                    .Where(a => a.ArtistaFK == ArtistaId)
                                    .OrderBy(m => m.Titulo).ToList();

            return View(musica);
        }


        // POST: Musicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            //Identifica o Id do artista autenticado
            int ArtistaId = (await _context.Artistas
                             .Where(a => a.UserNameId == _userManager.GetUserId(User))
                             .FirstOrDefaultAsync()).Id;

            //var musica = await _context.Musicas.FindAsync(id);
            var musica = await _context.Musicas
                        .Where(m => m.Id == id && m.ArtistaFK == ArtistaId)
                        .Include(m => m.ListaDeAlbuns)
                        .FirstOrDefaultAsync();


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
