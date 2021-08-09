using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PortalAutenticacao.Domain.Services;
using PortalAutenticacao.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalAutenticacao.UI.Autenticacao
{
    public class PortalAutenticacaoUserManager : UserManager<Usuario>
    {
        public PortalAutenticacaoUserManager(IUserStore<Usuario> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Usuario> passwordHasher, IEnumerable<IUserValidator<Usuario>> userValidators, IEnumerable<IPasswordValidator<Usuario>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Usuario>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

        }

        public override Task<Usuario> FindByEmailAsync(string email)
        {
            try
            {
                var usuarioService = new UsuarioService();
                var user = usuarioService.Buscar(new Usuario { Email = email });

                return Task.FromResult(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override Task<bool> CheckPasswordAsync(Usuario user, string password)
        {
            if (user.Senha == password)
                return Task.FromResult(true);
            return Task.FromResult(false);
        }

        public override Task<IdentityResult> ResetPasswordAsync(Usuario user, string token, string newPassword)
        {
            try
            {
                var usuarioService = new UsuarioService();
                var usuario = usuarioService.Buscar(new Usuario { Email = user.Email });

                if(usuario.TokenResetSenha == token)
                {
                    return Task.FromResult(IdentityResult.Success);
                }
                else
                {
                    return Task.FromResult(IdentityResult.Failed());
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public override Task<IdentityResult> AddToRoleAsync(Usuario user, string role)
        {
            try
            {
                var nivelUsuarioService = new UsuarioNivelService();

                var nivel = new UsuarioNivel
                {
                    FkNivel = Convert.ToInt32(role),
                    FkUsuario = user.UsuarioId
                };

                nivelUsuarioService.Salvar(nivel);

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
