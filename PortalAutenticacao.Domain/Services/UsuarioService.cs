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
            try
            {
                using (var Db = new Db().AbrirConexao)
                {

                    var Parametros = new DynamicParameters();

                    if (!string.IsNullOrEmpty(entidade.Nome))
                        Parametros.Add("@NOME", entidade.Nome, DbType.String);
                    else
                        Parametros.Add("@NOME", DBNull.Value, DbType.String);

                    if (!string.IsNullOrEmpty(entidade.Senha))
                        Parametros.Add("@SENHA", entidade.Senha, DbType.String);
                    else
                        Parametros.Add("@SENHA", DBNull.Value, DbType.String);

                    if (!string.IsNullOrEmpty(entidade.Telefone))
                        Parametros.Add("@TELEFONE", entidade.Telefone, DbType.String);
                    else
                        Parametros.Add("@TELEFONE", DBNull.Value, DbType.String);

                    if (!string.IsNullOrEmpty(entidade.Email))
                        Parametros.Add("@EMAIL", entidade.Email, DbType.String);
                    else
                        Parametros.Add("@EMAIL", DBNull.Value, DbType.String);

                    if (!string.IsNullOrEmpty(entidade.TokenResetSenha))
                        Parametros.Add("@TOKENSENHA", entidade.TokenResetSenha, DbType.String);
                    else
                        Parametros.Add("@TOKENSENHA", DBNull.Value, DbType.String);


                    Parametros.Add("@ID", entidade.UsuarioId, DbType.Int32);

                    Db.Execute("spAlterarUsuario", Parametros, commandType: CommandType.StoredProcedure);

                    return entidade;

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Usuario Buscar(Usuario entidade)
        {
            try
            {
                using (var Db = new Db().AbrirConexao)
                {

                    var Parametros = new DynamicParameters();

                    if (!string.IsNullOrEmpty(entidade.Nome))
                        Parametros.Add("@NOME", entidade.Nome, DbType.String);
                    else
                        Parametros.Add("@NOME", DBNull.Value, DbType.String);

                    if (!string.IsNullOrEmpty(entidade.Email))
                        Parametros.Add("@EMAIL", entidade.Email, DbType.String);
                    else
                        Parametros.Add("@EMAIL", DBNull.Value, DbType.String);

                    if (!string.IsNullOrEmpty(entidade.Senha))
                        Parametros.Add("@SENHA", entidade.Senha, DbType.String);
                    else
                        Parametros.Add("@SENHA", DBNull.Value, DbType.String);

                    return Db.Query<Usuario>("spListarUsuario", Parametros, commandType: CommandType.StoredProcedure)
                             .FirstOrDefault();
                }      

            }
            catch (Exception)
            {

                throw;
            }
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
