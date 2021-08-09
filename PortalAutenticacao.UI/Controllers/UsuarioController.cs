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
            var nivelService = new NivelService();
            var niveis = nivelService.Listar();
            ViewBag.Niveis = niveis;

            return View(ViewBag);
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(CadastrarUsuarioViewModel viewModel)
        {

            var user = new Usuario
            {
                Email = viewModel.Email,
                Endereco = viewModel.Endereco,
                Nome = viewModel.Nome,
                Senha = viewModel.Senha,
                Telefone = viewModel.Telefone
            };


            var resultado = await _userManager.CreateAsync(user);
            var findUser = await _userManager.FindByEmailAsync(user.Email);

            var resultadoNivel = await _userManager.AddToRoleAsync(findUser, viewModel.Nivel.ToString());
            

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

        [HttpGet]
        public async Task<IActionResult> EsqueciMinhaSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EsqueciMinhaSenha(EsqueciMinhaSenhaVeiwModel esqueciMinhaSenhaVeiwModel)
        {
            var EmailResult = await _userManager.FindByEmailAsync(esqueciMinhaSenhaVeiwModel.Email);

            var TokenAuth = await _userManager.GeneratePasswordResetTokenAsync(EmailResult);

            var escapeToken = System.Web.HttpUtility.UrlEncode(TokenAuth);

            var Url = $"https://localhost:44341/Usuario/ResetarSenha?Token={escapeToken}?Email={esqueciMinhaSenhaVeiwModel.Email}";

            var usuarioService = new UsuarioService();
            usuarioService.Alterar(new Usuario { TokenResetSenha = TokenAuth, UsuarioId = EmailResult.UsuarioId });

            return Redirect(Url);
        }


        [HttpGet]
        public async Task<IActionResult> ResetarSenha(string Token, string Email)
        {
            ViewBag.Email = Email;
            ViewBag.Token = Token;

            return View(ViewBag);
        }

        [HttpPost]
        public async Task<IActionResult> ResetarSenha(ResetarSenhaViewModel viewModel)
        {
            var EmailResult = await _userManager.FindByEmailAsync(viewModel.Email);

            var abc = _userManager.ResetPasswordAsync(EmailResult, viewModel.Token, viewModel.Email); ;

            return View();
        }

    }
}
