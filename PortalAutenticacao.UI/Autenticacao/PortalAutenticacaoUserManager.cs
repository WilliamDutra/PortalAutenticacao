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
        private readonly IPasswordHasher<Usuario> _PasswordHasher;
        private readonly IUserPasswordStore<Usuario> _UserPasswordStore;

        public PortalAutenticacaoUserManager(IUserStore<Usuario> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Usuario> passwordHasher, IEnumerable<IUserValidator<Usuario>> userValidators, IEnumerable<IPasswordValidator<Usuario>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Usuario>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _UserPasswordStore = Store as IUserPasswordStore<Usuario>;
            _PasswordHasher = passwordHasher;
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

        public override async Task<bool> CheckPasswordAsync(Usuario user, string password)
        {

            PasswordVerificationResult isValid = await VerifyPasswordAsync(_UserPasswordStore, user, password);

            if (isValid == PasswordVerificationResult.Success)
                return true;
            return false;

        }


        protected override Task<PasswordVerificationResult> VerifyPasswordAsync(IUserPasswordStore<Usuario> store, Usuario user, string password)
        {
            var isValid = _PasswordHasher.VerifyHashedPassword(user, password, user.Senha);
            return Task.FromResult(isValid);
        }

        public override Task<IdentityResult> ResetPasswordAsync(Usuario user, string token, string newPassword)
        {
            try
            {
                var usuarioService = new UsuarioService();
                var usuario = usuarioService.Buscar(new Usuario { Email = user.Email });

                if(usuario.PasswordResetToken == token)
                {
                    usuario.Senha = newPassword;
                    usuario.PasswordResetToken = "EM BRANCO";
                    usuarioService.Alterar(usuario);

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

    public class PassHash : IPasswordHasher<Usuario>
    {
        public string HashPassword(Usuario user, string password)
        {
            return ReversePassword(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(Usuario user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == ReversePassword(providedPassword))
            {
                return PasswordVerificationResult.Success;
            }

            return PasswordVerificationResult.Failed;
        }

        private string ReversePassword(string value)
        {
            // This is not a secure way to store a password!
            char[] charArray = value.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
