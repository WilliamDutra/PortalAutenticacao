using System;
using System.Data.SqlClient;

namespace PortalAutenticacao.Domain
{
    public class Db
    {
        private string strConnection = @"Server=localhost,1433; Database=PortalAutenticacao; User=SA; Password=yourStrong(!)Password";

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
