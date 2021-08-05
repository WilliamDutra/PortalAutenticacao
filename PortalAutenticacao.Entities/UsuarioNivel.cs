using System;
using System.Collections.Generic;
using System.Text;

namespace PortalAutenticacao.Entities
{
    public class UsuarioNivel
    {
        public int IdUsuarioNivel { get; set; }

        public int FkUsuario { get; set; }

        public int FkNivel { get; set; }
    }
}
