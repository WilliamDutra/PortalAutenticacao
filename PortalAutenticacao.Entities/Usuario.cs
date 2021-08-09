﻿using System;

namespace PortalAutenticacao.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        public string TokenResetSenha { get; set; }

    }
}
