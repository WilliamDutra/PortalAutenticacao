using Dapper;
using PortalAutenticacao.Domain.Interfaces;
using PortalAutenticacao.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PortalAutenticacao.Domain.Services
{
    public class UsuarioService : IUsuario
    {
        public Usuario Alterar(Usuario entidade)
        {
            throw new NotImplementedException();
        }

        public Usuario Buscar(Usuario entidade)
        {
            return null;
        }

        public List<Usuario> Listar()
        {
            try
            {
                using (var Db = new Db().AbrirConexao)
                {

                    var Parametros = new DynamicParameters();

                    return Db.Query<Usuario>("spListarUsuario", Parametros, commandType: CommandType.StoredProcedure)
                             .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Salvar(Usuario entidade)
        {
            try
            {
                using (var Db = new Db().AbrirConexao)
                {

                    var Parametros = new DynamicParameters();

                    Parametros.Add("@NOME", entidade.Nome, DbType.String);
                    Parametros.Add("@EMAIL", entidade.Email, DbType.String);
                    Parametros.Add("@SENHA", entidade.Senha, DbType.String);
                    Parametros.Add("@TELEFONE", entidade.Telefone, DbType.String);
                    Parametros.Add("@ENDERECO", entidade.Endereco, DbType.String);

                    int Id = Db.Query<int>("spSalvarUsuario", Parametros, commandType: CommandType.StoredProcedure)
                               .FirstOrDefault();

                    return Id;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
