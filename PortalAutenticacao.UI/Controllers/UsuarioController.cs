using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortalAutenticacao.Domain.Services;
using PortalAutenticacao.Entities;
using PortalAutenticacao.UI.ViewModels;

namespace PortalAutenticacao.UI.Controllers
{
    public class UsuarioController : Controller
    {
        private UserManager<Usuario> _userManager;

        public UsuarioController(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

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


            var resultado = _userManager.CreateAsync(user, "1235");
            resultado.Wait();

            return View();
        }
    }
}
