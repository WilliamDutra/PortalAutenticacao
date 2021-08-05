using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PortalAutenticacao.Domain.Services;
using PortalAutenticacao.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortalAutenticacao.UI.Autenticacao
{
    public class PortalAutenticacaoClaimsFactory : IUserClaimsPrincipalFactory<Usuario>
    {

        public async Task<ClaimsPrincipal> CreateAsync(Usuario user)
        {
            try
            {
                var usuarioUsuarioNivel = new UsuarioNivelService();
                var usuarioNivel = new NivelService();

                var NivelId = usuarioUsuarioNivel.Buscar(new UsuarioNivel { FkUsuario = user.IdUsuario }).FkNivel;
                var NivelNome = usuarioNivel.Buscar(new Nivel { NivelId = NivelId }).Nome;

                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Nome));
                identity.AddClaim(new Claim(ClaimTypes.Role, NivelNome));
                var principal = new ClaimsPrincipal(identity);

                return principal;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
