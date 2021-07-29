using PortalAutenticacao.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalAutenticacao.Domain.Interfaces
{
    public interface IUsuario
    {
        List<Usuario> Listar();

        Usuario Buscar(Usuario entidade);

        int Salvar(Usuario entidade);

        Usuario Alterar(Usuario entidade);

    }
}
