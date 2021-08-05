using PortalAutenticacao.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalAutenticacao.Domain.Interfaces
{
    public interface INivel
    {
        List<Nivel> Listar();

        Nivel Buscar(Nivel entidade);

        int Salvar(Nivel entidade);

        Nivel Alterar(Nivel entidade);

    }
}
