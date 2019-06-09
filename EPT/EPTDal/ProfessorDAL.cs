using EPTEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPTDal
{
    public class ProfessorDAL
    {
        public List<Turma> GetTurmasProfessor(int idProfessor)
        {
            var conexao = Conexao.getConexao();

            List<Turma> listaTurmas = new List<Turma>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * from tbTurma");
            sb.AppendFormat("where idProfessor = {0}", idProfessor);

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var turma = new Turma();
                        turma.id = Convert.ToInt32(reader["id"]);
                        turma.Semestre = reader["Semestre"].ToString();
                        turma.idCurso = Convert.ToInt32(reader["idCurso"]);
                        turma.Nome = reader["Nome"].ToString();

                        listaTurmas.Add(turma);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listaTurmas;

        }

        public List<Curso> GetCursosProfessor(int idProfessor)
        {
            var conexao = Conexao.getConexao();

            List<Curso> listaCurso = new List<Curso>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT DISTINCT(C.id), C.Nome, C.turno from tbCurso C");
            sb.AppendLine("JOIN tbTurma T ON T.idCurso = C.id ");
            sb.AppendFormat("where T.idProfessor = {0}", idProfessor);

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var curso = new Curso();
                        curso.id = Convert.ToInt32(reader["id"]);
                        curso.Nome = reader["Nome"].ToString();
                        curso.Turno = reader["turno"].ToString();

                        listaCurso.Add(curso);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listaCurso;

        }



    }
}
