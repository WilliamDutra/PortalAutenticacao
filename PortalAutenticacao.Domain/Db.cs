using System;
using System.Data.SqlClient;

namespace PortalAutenticacao.Domain
{
    public class Db
    {
        private string strConnection = @"Data Source=PC-PC\SQLEXPRESS;Initial Catalog=PORTALAUTENTICACAO;Integrated Security=True;";

        public SqlConnection AbrirConexao => OpenConnection();

        private SqlConnection OpenConnection()
        {
            try
            {
                var Connection = new SqlConnection(strConnection);
                Connection.Open();

                return Connection;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
