using Microsoft.AspNetCore.Identity;
using PortalAutenticacao.Domain.Services;
using PortalAutenticacao.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PortalAutenticacao.UI.Autenticacao
{
    public class PortalAutenticacaoUserStore : IUserStore<Usuario>, IUserPasswordStore<Usuario>
    {
        public Task<IdentityResult> CreateAsync(Usuario user, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioService = new UsuarioService();
                usuarioService.Salvar(user);

                return Task.FromResult(IdentityResult.Success);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IdentityResult> DeleteAsync(Usuario user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public Task<Usuario> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioService = new UsuarioService();
                var user = usuarioService.Buscar(new Usuario { Nome = normalizedUserName });

                return Task.FromResult(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<string> GetNormalizedUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Nome);
        }

        public Task<string> GetPasswordHashAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Senha);
        }

        public Task<string> GetUserIdAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UsuarioId.ToString());
        }

        public Task<string> GetUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Nome);
        }

        public Task<bool> HasPasswordAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task SetNormalizedUserNameAsync(Usuario user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(Usuario user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Senha = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Usuario user, string userName, CancellationToken cancellationToken)
        {
            user.Nome = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(Usuario user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        
    }
}
