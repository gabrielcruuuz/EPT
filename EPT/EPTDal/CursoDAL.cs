using EPTEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPTDal
{
    public class CursoDAL
    {
        public List<Curso> GetCursos()
        {
            var conexao = Conexao.getConexao();
            List<Curso> listaCurso = new List<Curso>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select * from tbCurso");
         
            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Curso curso = new Curso();

                        curso.id = Convert.ToInt32(reader["id"]);
                        curso.Nome = reader["nome"].ToString(); ;
                        curso.idCampus = Convert.ToInt32(reader["idCampus"]);
                        curso.Turno = reader["turno"].ToString();

                        listaCurso.Add(curso);

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listaCurso;

        }

        public Curso GetCurso(int idCurso)
        {
            var conexao = Conexao.getConexao();

            Curso curso = new Curso();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select * from tbCurso");
            sb.AppendFormat("WHERE id = {0}", idCurso);

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        curso.id = Convert.ToInt32(reader["id"]);
                        curso.Nome = reader["nome"].ToString(); ;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return curso;
        }


    }
}
