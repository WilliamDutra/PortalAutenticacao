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
    public class UsuarioNivelService : IUsuarioNivel
    {
        public UsuarioNivel Alterar(UsuarioNivel entidade)
        {
            throw new NotImplementedException();
        }

        public UsuarioNivel Buscar(UsuarioNivel entidade)
        {
            try
            {
                try
                {
                    using (var Db = new Db().AbrirConexao)
                    {

                        var Parametros = new DynamicParameters();

                        if(entidade.FkUsuario > 0)
                            Parametros.Add("@FKUSUARIO", entidade.FkUsuario, DbType.Int32);
                        else
                            Parametros.Add("@FKUSUARIO", DBNull.Value, DbType.Int32);

                        if (entidade.FkNivel > 0)
                            Parametros.Add("@FKNIVEL", entidade.FkNivel, DbType.Int32);
                        else
                            Parametros.Add("@FKNIVEL", DBNull.Value, DbType.Int32);

                        return Db.Query<UsuarioNivel>("spListarUsuarioNivel", Parametros, commandType: CommandType.StoredProcedure)
                                 .FirstOrDefault();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<UsuarioNivel> Listar()
        {
            throw new NotImplementedException();
        }

        public int Salvar(UsuarioNivel entidade)
        {
            try
            {
                using (var Db = new Db().AbrirConexao)
                {

                    var Parametros = new DynamicParameters();
                    Parametros.Add("@FKUSUARIO", entidade.FkUsuario, DbType.Int32);
                    Parametros.Add("@FKNIVEL", entidade.FkNivel, DbType.Int32);

                    return Db.Query<int>("spSalvarUsuarioNivel", Parametros, commandType: CommandType.StoredProcedure)
                             .FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
