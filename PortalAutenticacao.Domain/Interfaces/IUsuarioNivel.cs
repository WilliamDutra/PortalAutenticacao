using PortalAutenticacao.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalAutenticacao.Domain.Interfaces
{
    public interface IUsuarioNivel
    {
        List<UsuarioNivel> Listar();

        UsuarioNivel Buscar(UsuarioNivel entidade);

        int Salvar(UsuarioNivel entidade);

        UsuarioNivel Alterar(UsuarioNivel entidade);
    }
}
