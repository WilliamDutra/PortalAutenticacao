using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortalAutenticacao.Domain.Helpers;
using PortalAutenticacao.Domain.Services;
using PortalAutenticacao.Entities;
using PortalAutenticacao.UI.Autenticacao;
using PortalAutenticacao.UI.ViewModels;

namespace PortalAutenticacao.UI.Controllers
{
    public class UsuarioController : BaseController
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

        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            var nivelService = new NivelService();
            var niveis = nivelService.Listar();
            
            return View(niveis);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CadastrarUsuarioViewModel viewModel)
        {

            var user = new Usuario
            {
                Email = viewModel.Email,
                Endereco = viewModel.Endereco,
                Nome = viewModel.Nome,
                Senha = viewModel.Senha,
                Telefone = viewModel.Telefone
            };

            await _userManager.CreateAsync(user, user.Senha);
            var usuario = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.AddToRoleAsync(usuario, viewModel.Nivel.ToString());

            ExibirMensagem("Usuário cadastrado com sucesso!");

            return RedirectToAction("Cadastrar");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var usuario = await _userManager.FindByEmailAsync(viewModel.Email);
            var isAuth = await _userManager.CheckPasswordAsync(usuario, viewModel.Senha);

            if (!isAuth)
            {
                ExibirMensagem("Não foi possivel autenticar");
                return RedirectToAction("Login");
            }

            var identity = await _userClaimsFactory.CreateAsync(usuario);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity, new AuthenticationProperties { IsPersistent = false }); ;

            return RedirectToAction("Login");
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

            var Url = $"https://localhost:44341/Usuario/ResetarSenha?Token={escapeToken}&Email={esqueciMinhaSenhaVeiwModel.Email}";

            var usuarioService = new UsuarioService();
            usuarioService.Alterar(new Usuario { PasswordResetToken = TokenAuth, UsuarioId = EmailResult.UsuarioId });

            var mail = new MailHelper();
            mail.EnviarEmail("rodneylatariaferrugem@gmail.com", "Recuperação de Senha", $"Click no link para resetar sua senha: {Url}");


            return Redirect("/Usuario/Login");
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
