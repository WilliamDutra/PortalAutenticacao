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
    public class NivelService : INivel
    {
        public Nivel Alterar(Nivel entidade)
        {
            throw new NotImplementedException();
        }

        public Nivel Buscar(Nivel entidade)
        {
            try
            {
                using (var Db = new Db().AbrirConexao)
                {

                    var Parametros = new DynamicParameters();

                    if (entidade.NivelId > 0)
                        Parametros.Add("@ID", entidade.Nome, DbType.Int32);
                    else
                        Parametros.Add("@ID", DBNull.Value, DbType.Int32);

                    if (!string.IsNullOrEmpty(entidade.Nome))
                        Parametros.Add("@NOME", entidade.Nome, DbType.String);
                    else
                        Parametros.Add("@NOME", DBNull.Value, DbType.String);

                    if (!string.IsNullOrEmpty(entidade.Descricao))
                        Parametros.Add("@DESCRICAO", entidade.Descricao, DbType.String);
                    else
                        Parametros.Add("@DESCRICAO", DBNull.Value, DbType.String);

                    return Db.Query<Nivel>("spListarNivel", Parametros, commandType: CommandType.StoredProcedure)
                             .FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Nivel> Listar()
        {
            try
            {
                using (var Db = new Db().AbrirConexao)
                {

                    var Parametros = new DynamicParameters();

                    return Db.Query<Nivel>("spListarNivel", Parametros, commandType: CommandType.StoredProcedure)
                             .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Salvar(Nivel entidade)
        {
            throw new NotImplementedException();
        }
    }
}
