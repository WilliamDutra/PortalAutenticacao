using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortalAutenticacao.Domain.Services;
using PortalAutenticacao.Entities;
using PortalAutenticacao.UI.Autenticacao;
using PortalAutenticacao.UI.ViewModels;

namespace PortalAutenticacao.UI.Controllers
{
    public class UsuarioController : Controller
    {
        private UserManager<Usuario> _userManager;

        private IUserClaimsPrincipalFactory<Usuario> _userClaimsFactory;

        public UsuarioController(UserManager<Usuario> userManager, IUserClaimsPrincipalFactory<Usuario> userClaimsFactory)
        {
            _userManager = userManager;
            _userClaimsFactory = userClaimsFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Salvar(CadastrarUsuarioViewModel viewModel)
        {

            var user = new Usuario
            {
                Email = viewModel.Email,
                Endereco = viewModel.Endereco,
                Nome = viewModel.Nome,
                Senha = viewModel.Senha,
                Telefone = viewModel.Telefone
            };


            var resultado = _userManager.CreateAsync(user);
            resultado.Wait();

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var EmailResult = _userManager.FindByEmailAsync(viewModel.Email);
            var SenhaResult = _userManager.CheckPasswordAsync(EmailResult.Result, viewModel.Senha);

            //var identity = new ClaimsIdentity(new List<Claim> {
            //    new Claim(ClaimTypes.Name, EmailResult.Result.Nome),
            //    new Claim(ClaimTypes.NameIdentifier, EmailResult.Result.Email)
            //}, CookieAuthenticationDefaults.AuthenticationScheme);

            var identity = await _userClaimsFactory.CreateAsync(EmailResult.Result);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity, new AuthenticationProperties { IsPersistent = false }); ;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> NaoAutorizado()
        {
            var authResult = await HttpContext.AuthenticateAsync();

            return View();
        }

    }
}
