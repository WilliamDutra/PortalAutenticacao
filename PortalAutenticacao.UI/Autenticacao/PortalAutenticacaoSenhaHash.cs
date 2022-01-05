using Microsoft.AspNetCore.Identity;
using PortalAutenticacao.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalAutenticacao.UI.Autenticacao
{
    public class PortalAutenticacaoSenhaHash : IPasswordHasher<Usuario>
    {
        public string HashPassword(Usuario user, string password)
        {
            string Hash = BCrypt.Net.BCrypt.HashPassword(password, 12);
            return Hash;
        }

        public PasswordVerificationResult VerifyHashedPassword(Usuario user, string hashedPassword, string providedPassword)
        {
            try
            {

                if (BCrypt.Net.BCrypt.Verify(hashedPassword, providedPassword))
                {
                    return PasswordVerificationResult.Success;
                }

                return PasswordVerificationResult.Failed;

            }catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
