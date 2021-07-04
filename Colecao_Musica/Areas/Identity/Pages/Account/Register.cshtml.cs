using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Colecao_Musica.Data;
using Colecao_Musica.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Colecao_Musica.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        //private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;

        /// <summary>
        /// atributo que referencia a Base de Dados do projeto
        /// </summary>
        private readonly Colecao_MusicaBD _context;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            // SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            //IEmailSender emailSender)
            Colecao_MusicaBD context)
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
            _context = context;

        }


        /// <summary>
        /// Transporta dados entre o form e "código"
        /// transporta dados entre browser e servidor
        /// </summary>
        [BindProperty]          //Adiciona memória ao http
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        //public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} e máximo {1} de caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar password")]
            [Compare("Password", ErrorMessage = "A sua password não coincide com a sua password de confirmação.")]
            public string ConfirmPassword { get; set; }
            

            /// <summary>
            /// Atributo para recolher os dados do artista que está a criar a conta
            /// </summary>
            public Artistas Artista { get; set; }

        }

        public void  OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
          //  ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }



        /// <summary>
        /// Este método é acedido se a página devolver o controlo em HTTP POST
        /// é aqui que é criado o novo utilizador
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            //se "return url" for nulo altera o seu valor
            returnUrl ??= Url.Content("~/");
           // ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                //Se entrei os dados são válidos.

                //cria um objeto tipo "user"
                //Com os dados que é autentiticado
                var user = new IdentityUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    LockoutEnd = new DateTime(DateTime.Now.Year + 10,
                                          DateTime.Now.Month,
                                          DateTime.Now.Day)
                };
               
                //vou criar o utilizador
                var result = await _userManager.CreateAsync(user, Input.Password);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("Utilizador criou nova conta com password.");


                    try
                    {
                        // adicionar ao Role
                        await _userManager.AddToRoleAsync(user, "Artista");
                     
                        // atribuir ao artista o ID do user q acabou de se criar
                        Input.Artista.UserNameId = user.Id;


                        // guardar os dados na BD
                        await _context.AddAsync(Input.Artista);

                        // consolidar a operação de guardar
                        await _context.SaveChangesAsync();

                     
                        // redirecionar para a página de confirmação de criação de conta
                        return RedirectToPage("RegisterConfirmation");
                    }
                    catch (Exception)
                    {
                        // houve um erro na criação de um Criador
                        // Além da mensagem de erro,
                        ModelState.AddModelError("", "Houve um erro com a criação do utilizador");
                        //  deverá ser apagada o User q foi previamente criado
                        await _userManager.DeleteAsync(user);
                        // devolver os dados à página
                        return Page();
                    }
                    /*var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Por favor confirme a sua conta por <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }*/
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
