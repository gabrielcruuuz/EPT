using EPTEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPTDal
{
    public class NotaDAL
    {
        public List<Nota> GetListaNotas(int ra)
        {
            var conexao = Conexao.getConexao();

            List<Nota> listaNotas = new List<Nota>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select T.nome as 'Turma', I.semestre, I.qtdAcertos, U.nome, U.ra");
            sb.AppendLine("FROM tbNotaIndividual I ");
            sb.AppendLine("JOIN tbAluno A on A.id = I.idAluno");
            sb.AppendLine("JOIN tbUsuario U on U.id = A.idUsuario");
            sb.AppendLine("JOIN tbTurma T on T.id = A.idTurma");
            sb.AppendFormat("WHERE U.ra = {0}", ra);
            sb.AppendLine("ORDER BY T.semestre");

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var nota = new Nota();
                        nota.nota = Convert.ToInt32(reader["qtdacertos"]);
                        nota.Semestre = Convert.ToInt32(reader["Semestre"]);

                        listaNotas.Add(nota);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listaNotas;

        }

        public List<NotaTurma> GetListaNotaTurmaAluno(int idTurma)
        {
            var conexao = Conexao.getConexao();

            List<NotaTurma> listaNotas = new List<NotaTurma>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select N.semestre, AVG(N.qtdAcertos) 'qtdAcertos'");
            sb.AppendLine("FROM tbNotaIndividual N  ");
            sb.AppendLine("JOIN tbAluno A on A.id = N.idAluno");
            sb.AppendLine("JOIN tbTurma T on T.id = A.idTurma");
            sb.AppendFormat("where T.id = {0}", idTurma);
            sb.AppendLine("GROUP BY N.semestre");

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var nota = new NotaTurma();
                        nota.nota = Convert.ToInt32(reader["qtdAcertos"]);
                        nota.Semestre = Convert.ToInt32(reader["Semestre"]);

                        listaNotas.Add(nota);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listaNotas;

        }

        public List<NotaTurma> GetListaNotaTurma(int idTurma)
        {
            var conexao = Conexao.getConexao();

            List<NotaTurma> listaNotas = new List<NotaTurma>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT AVG(NI.qtdAcertos) AS 'AcertosTurma',T.id 'idTurma', T.nome 'Turma', Ni.Semestre from tbNotaIndividual NI");
            sb.AppendLine("JOIN tbAluno A ON A.id = NI.idAluno");
            sb.AppendLine("JOIN tbTurma T ON T.id = A.idTurma");
            sb.AppendFormat("where T.id = {0}", idTurma);
            sb.AppendFormat("group by T.id, T.nome, Ni.semestre");


            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var nota = new NotaTurma();
                        nota.idTurma = Convert.ToInt32(reader["idTurma"]);
                        nota.nota = Convert.ToInt32(reader["AcertosTurma"]);
                        nota.Semestre = Convert.ToInt32(reader["Semestre"]);

                        listaNotas.Add(nota);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listaNotas;

        }

        public List<NotaTurma> GetListaNotaTurmaCoordenador(int idCurso)
        {
            var conexao = Conexao.getConexao();

            List<NotaTurma> listaNotas = new List<NotaTurma>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT AVG(NI.qtdAcertos) AS 'AcertosTurma',T.id, T.nome 'Turma' , T.Semestre from tbNotaIndividual NI");
            sb.AppendLine("JOIN tbAluno A ON A.id = NI.idAluno");
            sb.AppendLine("JOIN tbTurma T ON T.id = A.idTurma");
            sb.AppendLine("JOIN tbCurso C ON C.id = T.idCurso");
            sb.AppendFormat("where C.id = {0}", idCurso);
            sb.AppendFormat("group by T.id, T.nome, T.semestre");


            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var nota = new NotaTurma();
                        nota.nota = Convert.ToInt32(reader["AcertosTurma"]);
                        nota.Semestre = Convert.ToInt32(reader["Semestre"]);

                        listaNotas.Add(nota);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listaNotas;

        }



        public List<NotaTurma> GetListaNotaCurso(int idCurso, int semestre)
        {
            var conexao = Conexao.getConexao();

            List<NotaTurma> listaNotasCurso = new List<NotaTurma>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select AVG(N.qtdAcertos) 'NotaCurso', N.semestre , C.id 'idCurso'");
            sb.AppendLine("FROM tbNotaIndividual N");
            sb.AppendLine("JOIN tbAluno A on A.id = N.idAluno");
            sb.AppendLine("JOIN tbTurma T on T.id = A.idTurma");
            sb.AppendLine("JOIN tbCurso C on C.id = T.idCurso");
            sb.AppendFormat("WHERE T.idCurso = {0}", idCurso);
            sb.AppendFormat("AND A.semestre  = {0}", semestre);
            sb.AppendLine("GROUP BY N.semestre, C.id");

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var nota = new NotaTurma();
                        nota.nota = Convert.ToInt32(reader["NotaCurso"]);
                        nota.Semestre = Convert.ToInt32(reader["Semestre"]);
                        nota.idCurso = Convert.ToInt32(reader["idCurso"]);
                        listaNotasCurso.Add(nota);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listaNotasCurso;

        }

        public List<NotaTurma> GetListaNotaCurso(int idCurso)
        {
            var conexao = Conexao.getConexao();

            List<NotaTurma> listaNotasCurso = new List<NotaTurma>();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select AVG(N.qtdAcertos) 'NotaCurso', N.semestre  ");
            sb.AppendLine("FROM tbNotaIndividual N  ");
            sb.AppendLine("JOIN tbAluno A ON A.id = N.idAluno");
            sb.AppendLine("JOIN tbTurma T ON A.idturma = T.id");
            sb.AppendLine("JOIN tbCurso C on C.id = T.idCurso");
            sb.AppendFormat("WHERE C.id = {0}", idCurso);
            sb.AppendLine("GROUP BY N.semestre");

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var nota = new NotaTurma();
                        nota.nota = Convert.ToInt32(reader["NotaCurso"]);
                        nota.Semestre = Convert.ToInt32(reader["Semestre"]);

                        listaNotasCurso.Add(nota);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listaNotasCurso;

        }


        public DataTable ExportarPlanilha(int ra)
        {
            var conexao = Conexao.getConexao();

            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet(); //conn is opened by dataadapter

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select T.nome as 'Turma', I.semestre, I.qtdAcertos, U.nome, U.ra");
            sb.AppendLine("FROM tbNotaIndividual I ");
            sb.AppendLine("JOIN tbAluno A on A.id = I.idAluno");
            sb.AppendLine("JOIN tbUsuario U on U.id = A.idUsuario");
            sb.AppendLine("JOIN tbTurma T on T.id = A.idTurma");
            sb.AppendFormat("WHERE U.ra = {0}", ra);
            sb.AppendLine("ORDER BY T.semestre");

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dataTable);

                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return ds.Tables[0];
        }

        public DataTable ExportarPlanilhaIndicadoresTurma(int idTurma)
        {
            var conexao = Conexao.getConexao();

            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet(); 

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT AVG(NI.qtdAcertos) AS 'AcertosTurma',T.id 'idTurma', T.nome 'Turma', Ni.Semestre from tbNotaIndividual NI");
            sb.AppendLine("JOIN tbAluno A ON A.id = NI.idAluno ");
            sb.AppendLine("JOIN tbTurma T ON T.id = A.idTurma");
            sb.AppendFormat("where T.id = {0}", idTurma);
            sb.AppendLine("group by T.id, T.nome, Ni.semestre");

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dataTable);

                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return ds.Tables[0];
        }

        public DataTable ExportarPlanilhaIndicadoresCurso(int idCurso, int semestre)
        {
            var conexao = Conexao.getConexao();

            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select AVG(N.qtdAcertos) 'AcertosCurso', N.semestre , C.id 'idCurso', C.nome 'NomeCurso' ");
            sb.AppendLine("FROM tbNotaIndividual N ");
            sb.AppendLine("JOIN tbAluno A on A.id = N.idAluno");
            sb.AppendLine("JOIN tbTurma T on T.id = A.idTurma");
            sb.AppendLine("JOIN tbCurso C on C.id = T.idCurso");
            sb.AppendFormat("where T.idCurso = {0}", idCurso);
            sb.AppendFormat("AND A.semestre = {0}", semestre);
            sb.AppendLine("GROUP BY N.semestre, C.id, C.nome");

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dataTable);

                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return ds.Tables[0];
        }


    }
}
