
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;

namespace EPTDal
{
    class Conexao
    {
        public static SqlConnection getConexao()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConexaoEPT"].ConnectionString;

            SqlConnection conexao = new SqlConnection(connStr);

            conexao.Open();

            return conexao;

        } 

    }
}