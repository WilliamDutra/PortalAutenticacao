﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalAutenticacao.UI.ViewModels
{
    public class CadastrarUsuarioViewModel
    {
        public string Nome { get; set; }

        public string Endereco { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Telefone { get; set; }

        public int Nivel { get; set; }

    }
}
