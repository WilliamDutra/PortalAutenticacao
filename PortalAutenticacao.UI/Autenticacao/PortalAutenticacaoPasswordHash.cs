using Microsoft.AspNetCore.Identity;
using PortalAutenticacao.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PortalAutenticacao.UI.Autenticacao
{
    public class PortalAutenticacaoPasswordHash : PasswordHasher<Usuario>
    {
        public override string HashPassword(Usuario user, string password)
        {
            return ReversePassword(password);
        }

        public override PasswordVerificationResult VerifyHashedPassword(Usuario user, string hashedPassword, string providedPassword)
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
