using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalAutenticacao.UI.ViewModels
{
    public class ResetarSenhaViewModel
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public string Senha { get; set; }
    }
}
