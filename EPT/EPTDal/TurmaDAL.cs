using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using EPTEntity;

namespace EPTDal
{
    public class TurmaDAL
    {
        public List<Turma> GetTurmas()
        {
            var conexao = Conexao.getConexao();

            List<Turma> listaTurmas = new List<Turma>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select * from tbTurma");

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
                        turma.Nome = reader["nome"].ToString(); ;
                        turma.idCurso = Convert.ToInt32(reader["idCurso"]);
                        turma.Semestre = reader["semestre"].ToString();

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

        public List<Semestre> GetSemestres(int idCurso)
        {
            var conexao = Conexao.getConexao();

            List<Semestre> listaSemestre = new List<Semestre>();

            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("Select Distinct semestre from tbTurma");
            sb.AppendFormat("WHERE idCurso = {0}", idCurso);

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Semestre semestre = new Semestre();

                        semestre.semestre = Convert.ToInt32(reader["semestre"]);

                        listaSemestre.Add(semestre);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listaSemestre;
        }

        public List<Turma> GetTurmas(int idCurso, int semestre)
        {
            var conexao = Conexao.getConexao();

            List<Turma> listaTurmas = new List<Turma>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select * from tbTurma");
            sb.AppendFormat("WHERE idCurso = {0}", idCurso);
            sb.AppendFormat("AND semestre = '{0}'", semestre);

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
                        turma.Nome = reader["nome"].ToString(); ;
                        turma.idCurso = Convert.ToInt32(reader["idCurso"]);
                        turma.Semestre = reader["semestre"].ToString();

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

        public Turma GetTurma(int idTurma)
        {
            var conexao = Conexao.getConexao();

            Turma turma = new Turma();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select * from tbTurma");
            sb.AppendFormat("WHERE id = {0}", idTurma);

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        turma.id = Convert.ToInt32(reader["id"]);
                        turma.Nome = reader["nome"].ToString(); ;
                        turma.idCurso = Convert.ToInt32(reader["idCurso"]);
                        turma.Semestre = reader["semestre"].ToString();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return turma;
        }


    }
}