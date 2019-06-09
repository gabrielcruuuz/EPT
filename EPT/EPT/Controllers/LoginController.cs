using EPTDal;
using EPTEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPT.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {           
            return View();
        }

        public ActionResult Entrar(Usuario login)
        {
            UsuarioDAL UsuarioDAL = new UsuarioDAL();
            Usuario usuarioLogado = UsuarioDAL.GetUsuario(login);

            if (usuarioLogado.Nome == null || usuarioLogado.RA == 0)
            {
                ViewBag.SemPermissao = "Usuario ou senha incorretas";
                return View("Index");
            }

            Session["UsuarioRA"] = usuarioLogado.RA;
            Session["UsuarioNome"] = usuarioLogado.Nome;
            Session["Usuario"] = usuarioLogado;

            if (usuarioLogado.tipoPerfil == 1)
            {
                Session["Aluno"] = UsuarioDAL.GetAluno(usuarioLogado.id);
                return RedirectToAction("Index", "Home");
            }

            else if (usuarioLogado.tipoPerfil == 2)
            {
                Professor prof = UsuarioDAL.GetProfessor(usuarioLogado.id);

                ProfessorDAL profDAL = new ProfessorDAL();

                prof.listaTurmas = profDAL.GetTurmasProfessor(prof.idProfessor);
                prof.listaCurso = profDAL.GetCursosProfessor(prof.idProfessor);

                Session["Professor"] = prof;

                return RedirectToAction("Index", "Indicadores");
            }

            else
            {

                Coordenador coord = UsuarioDAL.GetCoordenador(usuarioLogado.id);

                TurmaDAL turmasDAL = new TurmaDAL();
                CursoDAL cursoDAL = new CursoDAL();

                coord.listaCurso = cursoDAL.GetCursos();
                coord.listaTurma = turmasDAL.GetTurmas();

                Session["Coordenador"] = coord;

                return RedirectToAction("Index", "Indicadores");
            }

        }


        public ActionResult Sair()
        {
            Session.Abandon();
            return RedirectToAction("Index");

        }
    }
}