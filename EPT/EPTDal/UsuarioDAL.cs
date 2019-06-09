using EPTEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPTDal
{
    public class UsuarioDAL
    {
        public Usuario GetUsuario(Usuario login)
        {
            var conexao = Conexao.getConexao();
            var usuario = new Usuario();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select * from tbUsuario");
            sb.AppendFormat("where ra = {0}", login.RA);
            sb.AppendFormat("and senha = {0}", login.Senha);

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        usuario.id = Convert.ToInt32(reader["id"]);
                        usuario.Nome = reader["nome"].ToString(); ;
                        usuario.RA = Convert.ToInt32(reader["ra"]);
                        usuario.tipoPerfil = Convert.ToInt32(reader["tipoPerfil"]);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return usuario;

        }

        public Aluno GetAluno(int idUsuario)
        {
            var conexao = Conexao.getConexao();
            var aluno = new Aluno();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select * from tbAluno");
            sb.AppendFormat("where idUsuario = {0}", idUsuario);

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        aluno.id = Convert.ToInt32(reader["id"]);
                        aluno.idCurso= Convert.ToInt32(reader["idCurso"]);
                        aluno.idTurma = Convert.ToInt32(reader["idTurma"]);
                        aluno.idUsuario = Convert.ToInt32(reader["idUsuario"]);
                        aluno.Semestre = Convert.ToInt32(reader["Semestre"]);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return aluno;

        }

        public Professor GetProfessor(int idUsuario)
        {
            var conexao = Conexao.getConexao();
            var professor = new Professor();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select * from tbProfessor");
            sb.AppendFormat("where idUsuario = {0}", idUsuario);

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        professor.idProfessor = Convert.ToInt32(reader["id"]);
                        professor.id = Convert.ToInt32(reader["idUsuario"]);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return professor;

        }

        public Coordenador GetCoordenador(int idUsuario)
        {
            var conexao = Conexao.getConexao();
            var coordenador = new Coordenador();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select * from tbCoordenador");
            sb.AppendFormat("where idUsuario = {0}", idUsuario);

            using (conexao)
            {
                SqlCommand command = new SqlCommand(sb.ToString(), conexao);
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        coordenador.id = Convert.ToInt32(reader["idUsuario"]);
                        coordenador.idCoordenador = Convert.ToInt32(reader["id"]);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return coordenador;

        }



    }
}
