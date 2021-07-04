using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Colecao_Musica.Data;
using Colecao_Musica.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Colecao_Musica.Controllers
{
    /// <summary>
    /// Controller para efetuar a gestão de Albuns de musica
    /// </summary>
    [Authorize]// Só acessivel se autenticado
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
        [AllowAnonymous]
        public async Task<IActionResult> Index() {

            //Quais os albuns do artista que se autenticou
            var listaAlbuns = (from a in _context.Albuns
                               join r in _context.Artistas on a.ArtistasFK equals r.Id
                               where r.UserNameId == _userManager.GetUserId(User)
                               select a)
                             .OrderBy(a => a.Titulo);
            
            //variavel onde guarda o valor do atributo 'genero'
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
        public IActionResult Create() {
            
            
          // ViewBag.ListaMusicas = _context.Musicas.OrderBy(m => m.Titulo).ToList();
            
            //Prepara os dados do atributo 'género' para uma Dropdown
            ViewData["GenerosFK"] = new SelectList(_context.Generos, "Id", "Designacao");
            return View();
        }


        // POST: Albuns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( [Bind("Titulo,Duracao,NrFaixas,Ano,Editora,Cover,GenerosFK")]Albuns album, IFormFile albumCover, string[] MusicaSelecionada, Musicas Musica)  {

            
            //Atribui ao objeto 'musica' a lista de musicas do artista que está ligado
            Musica.ArtistasFK = (await _context.Artistas.Where(m => m.UserNameId == _userManager.GetUserId(User)).FirstOrDefaultAsync()).Id;


            // <- ADIÇÃO DE músicas ao album ->
            // avalia se o array com a lista de musicas selecionadas associadas ao album está vazia ou não
            if (MusicaSelecionada.Count() == 0)
            {
                //É gerada uma mensagem de erro
                ModelState.AddModelError("", "É necessário selecionar pelo menos uma música.");
                // gerar a lista de musicas que podem ser associadas à aula
                ViewBag.musica = _context.Musicas.OrderBy(m => m.Titulo).ToList();


                //ViewBag.listaMusicas = _context.Musicas.OrderBy(m => m.Titulo).ToList();

                //ViewData["GenerosFK"] = new SelectList(_context.Generos, "Id", "Designacao");
                // devolver controlo à View
                return View(album);
            }

            // criar uma lista com os objetos escolhidos das músicas
            List<Musicas> ListaMusicasSelecionadas = new List<Musicas>();
            // Para cada objeto escolhido..
            foreach (string item in MusicaSelecionada)
            {
                //procurar a musica
                Musicas musica = _context.Musicas.Find(Convert.ToInt32(item));
                // adicionar a Categoria à lista
                ListaMusicasSelecionadas.Add(musica);
            }

            // adicionar a lista ao objeto de "Lesson"
            album.ListaDeMusicas = ListaMusicasSelecionadas;





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
                // se aqui chego, há ficheiro, mas não o cover
                // se aqui entro, não há foto
                // notificar o utilizador que há um erro
                ModelState.AddModelError("", "Deve selecionar um ficheiro...");

                // devolver o controlo à View
                // prepara os dados a serem enviados para a View
                // para a Dropdown
                //ViewData["GenerosFK"] = new SelectList(_context.Generos, "Id", "Designacao", album.GenerosFK);
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
                string caminhoAteAoFichFoto = _dadosServidor.WebRootPath;
                caminhoAteAoFichFoto = Path.Combine(caminhoAteAoFichFoto, "coverAlbum", album.Cover);
                // guardar o ficheiro no Disco Rígido
                using var stream = new FileStream(caminhoAteAoFichFoto, FileMode.Create);
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
            
            if (id == null){
                return NotFound();
            }

           
            var album = await _context.Albuns.FindAsync(id);

            if (album == null){
                return NotFound();
            }

            ViewBag.ListaDeMusicas = _context.Musicas.OrderBy(m => m.Titulo).ToList();

            ViewData["GenerosFK"] = new SelectList(_context.Generos, "Id", "Designacao", album.GenerosFK);

            return View(album);
        }


        // POST: Albuns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Duracao,NrFaixas,Ano,Editora,Cover,GenerosFK,ArtistasFK")] Albuns album, IFormFile albumCover, int[] ListaDeMusicas) {
            
            if (id != album.Id) {
                return NotFound();
            }

            //**************************************************************************************************

            // criar uma lista com os objetos escolhidos das Categorias

            

            // lista das categorias associadas às lições
            var listaOldMusicas = (from a in _context.Albuns
                                      where a.Id == id
                                      select a.ListaDeMusicas).FirstOrDefault()
                                     ;

            //from c in l.CategoriesList.SelectMany(c => c..Where(c => c..ID == l.ID)
            //select c.ID;

            // houve alteração de dados na edição da Lesson
            var diferenca = listaOldMusicas.Select(m => ListaDeMusicas.Contains(m.Id));

            // avalia se há diferencas
            if (diferenca.Any())
            {
                int hasgdj = 0;
            }


            //  _context.Lessons.Where(l => l.ID == id).Select(l => l.CategoriesList).
            // Where(c => CategoriaEscolhida.Contains(c.ID)).Select(c => c.ID);

            // verificar se hou


            List<Musicas> listaDeMusicasSelecionadas = new List<Musicas>();
            // Para cada objeto escolhido..
            foreach (int item in ListaDeMusicas)
            {
                //procurar a categoria
                Musicas musica = await _context.Musicas.FindAsync(item);
                // adicionar a Categoria à lista
                listaDeMusicasSelecionadas.Add(musica);
            }
            album.ListaDeMusicas = listaDeMusicasSelecionadas;
            //**************************************************************************************************

            // recuperar o Id do objeto enviado para o browser
            var numIdAlbum = HttpContext.Session.GetInt32("NumAlbumEmEdicao");

            // e compará-lo com o Id recebido
            // se forem iguais, continuamos
            // se forem diferentes, não fazemos a alteração

            if (numIdAlbum == null || numIdAlbum != album.Id)
            {
                // se entro aqui, é pq houve problemas

                // redirecionar para a página de início
                return RedirectToAction("Index");
            }

            //Atribui ao objeto 'album' a lista de albuns do artista que está ligado
            album.ArtistasFK = (await _context.Artistas
                .Where(a => a.UserNameId == _userManager.GetUserId(User)).FirstOrDefaultAsync()).Id;

            if (ModelState.IsValid) {

                try  {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                
                catch (DbUpdateConcurrencyException) {

                    if (!AlbunsExists(album.Id)) {
                        return NotFound();
                    }
                   
                    else {

                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
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

            //// guardar o ID do cover escolhido, para memória futura
            //// Session["idCover"]= id;   --> deixou de estar disponível
            //HttpContext.Session.SetInt32("idCover", (int)id);

            return View(albuns);
        }

        // POST: Albuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {

            //// ler o valor do ID que foi previamente guardado
            //var numIdFoto = HttpContext.Session.GetInt32("idCover");

            //// se a var de sessão extinguir-se,
            //// temos um problema
            //if (numIdFoto == null)
            //{
            //    //é preciso alertar o utilizador que demorou tempo de mais

            //    // e devolver o controlo à View
            //    return RedirectToAction("Index");
            //}



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
