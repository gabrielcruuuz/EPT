using EPTDal;
using EPTEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPT.Controllers
{
    public class IndicadoresController : Controller
    {
        // GET: Indicadores
        public ActionResult Index()
        {
            GetNotas();
            CarregarComboBox();

            return View();
        }

        public ActionResult FiltrarIndicadores( int idTurma , int semestre = 0, int idCurso = 0)
        {
            var usuario = (Usuario)Session["Usuario"];

            if (usuario.tipoPerfil == 1)
            {
                GetNotasTurma(idTurma);
            }
            else
            {
                GetNotas(idCurso, idTurma, semestre);
            }

            CarregarComboBox();
            return View("Index");
        }

        public ActionResult GetSemestre(int idCurso)
        {
            TurmaDAL turmaDAL = new TurmaDAL();

            List<Semestre> listaSemestres = turmaDAL.GetSemestres(idCurso);

            return Json(listaSemestres, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTurmasSemestre(int idCurso, int Semestre)
        {
            TurmaDAL turmaDAL = new TurmaDAL();

            var listaTurmas = turmaDAL.GetTurmas(idCurso, Semestre);
            return Json(listaTurmas, JsonRequestBehavior.AllowGet);
        }

        
        public void GetNotas()
        {
            NotaDAL notaDAL = new NotaDAL();
            TurmaDAL turmaDAL= new TurmaDAL();
            CursoDAL cursoDAL = new CursoDAL();

            var usuario = (Usuario)Session["Usuario"];

            if (usuario.id != 0)
            {
                List<Nota> listaNotas = new List<Nota>();
                List<NotaTurma> listaNotaTurma = new List<NotaTurma>();
                List<NotaTurma> listaNotaCurso = new List<NotaTurma>();
                string nomeTurma = string.Empty;
                string nomeCurso = string.Empty;

                if (usuario.tipoPerfil == 1)
                {
                    var aluno = (Aluno)(Session["Aluno"]);
                    listaNotas = notaDAL.GetListaNotas(usuario.RA);
                    listaNotaTurma = notaDAL.GetListaNotaTurmaAluno(aluno.idTurma);
                    listaNotaCurso = notaDAL.GetListaNotaCurso(aluno.idCurso, aluno.Semestre);

                    nomeTurma = turmaDAL.GetTurma(aluno.idTurma).Nome;
                    nomeCurso = cursoDAL.GetCurso(aluno.idCurso).Nome;

                }

                else if (usuario.tipoPerfil == 2)
                {
                    var professor = (Professor)(Session["Professor"]);

                    listaNotaTurma = notaDAL.GetListaNotaTurma(professor.listaTurmas.FirstOrDefault().id);
                    listaNotaCurso = notaDAL.GetListaNotaCurso(professor.listaCurso.FirstOrDefault().id);

                    nomeTurma = turmaDAL.GetTurma(professor.listaTurmas.FirstOrDefault().id).Nome;
                    nomeCurso = cursoDAL.GetCurso(professor.listaCurso.FirstOrDefault().id).Nome;


                }
                else
                {
                    var coordenador = (Coordenador)(Session["Coordenador"]);

                    listaNotaTurma = notaDAL.GetListaNotaTurmaCoordenador(coordenador.listaCurso.FirstOrDefault().id);
                    listaNotaCurso = notaDAL.GetListaNotaCurso(coordenador.listaCurso.FirstOrDefault().id);

                }

                string semestreChart = string.Empty;
                string notaChart = string.Empty;
                string notaTurmaChart = string.Empty;
                string notaCursoChart = string.Empty;

                foreach (var item in listaNotas)
                {
                    notaChart += item.nota + ",";
                    semestreChart += item.Semestre + " Semestre , ";
                }

                foreach (var item in listaNotaTurma)
                {
                    notaTurmaChart += item.nota + ",";
                }

                foreach (var item in listaNotaCurso)
                {
                    notaCursoChart += item.nota + ",";
                }

                ViewBag.Semestre = semestreChart;
                ViewBag.nota = notaChart;
                ViewBag.notaTurma = notaTurmaChart;
                ViewBag.notaCurso = notaCursoChart;

                ViewBag.NomeCurso = nomeCurso;
                ViewBag.NomeTurma = nomeTurma;
            }
        }

        public void GetNotas(int idCurso, int idTurma, int semestre)
        {
            NotaDAL notaDAL = new NotaDAL();
            TurmaDAL turmaDAL = new TurmaDAL();
            CursoDAL cursoDAL = new CursoDAL();

            var usuario = (Usuario)Session["Usuario"];

            if (usuario.id != 0)
            {
                string semestreChart = string.Empty;
                string notaTurmaChart = string.Empty;
                string notaCursoChart = string.Empty;

                string nomeTurma = string.Empty;
                string nomeCurso = string.Empty;

                List<NotaTurma> listaNotaTurma = notaDAL.GetListaNotaTurma(idTurma);
                List<NotaTurma> listaNotaCurso = notaDAL.GetListaNotaCurso(idCurso, semestre);

       
                nomeTurma = turmaDAL.GetTurma(idTurma).Nome;
              
                nomeCurso = cursoDAL.GetCurso(idCurso).Nome;
                


                foreach (var item in listaNotaTurma)
                {
                    notaTurmaChart += item.nota + ",";
                }

                foreach (var item in listaNotaCurso)
                {
                    notaCursoChart += item.nota + ",";
                }
                ViewBag.notaCurso = notaCursoChart;
                ViewBag.Semestre = semestreChart;
                ViewBag.notaTurma = notaTurmaChart;

                ViewBag.NomeCurso = nomeCurso;
                ViewBag.NomeTurma = nomeTurma;
            }
        }

        public void GetNotasCurso(int idCurso)
        {
            NotaDAL notaDAL = new NotaDAL();

            List<NotaTurma> listaNotaCurso = notaDAL.GetListaNotaCurso(idCurso);

            string semestreChart = string.Empty;
            string notaChart = string.Empty;
            string notaTurmaChart = string.Empty;
            string notaCursoChart = string.Empty;
            
            foreach (var item in listaNotaCurso)
            {
                notaCursoChart += item.nota + ",";
            }

            ViewBag.Semestre = semestreChart;
            ViewBag.notaCurso = notaCursoChart;
        }

        public void GetNotasTurma(int idTurma)
        {
            NotaDAL notaDAL = new NotaDAL();
            TurmaDAL turmaDAL = new TurmaDAL();
            CursoDAL cursoDAL = new CursoDAL();

            var usuario = (Usuario)Session["Usuario"];

            if (usuario.id != 0)
            {
                List<Nota> listaNotas = new List<Nota>();
                List<NotaTurma> listaNotaTurma = new List<NotaTurma>();
                List<NotaTurma> listaNotaCurso = new List<NotaTurma>();
                string nomeTurma = string.Empty;
                string nomeCurso = string.Empty;

                if (usuario.tipoPerfil == 1)
                {
                    var aluno = (Aluno)(Session["Aluno"]);
                    listaNotas = notaDAL.GetListaNotas(usuario.RA);
                    listaNotaTurma = notaDAL.GetListaNotaTurma(idTurma);
                    listaNotaCurso = notaDAL.GetListaNotaCurso(aluno.idCurso, aluno.Semestre);

                    nomeTurma = turmaDAL.GetTurma(idTurma).Nome;
                    nomeCurso = cursoDAL.GetCurso(aluno.idCurso).Nome;

                }

                else if (usuario.tipoPerfil == 2)
                {
                    var professor = (Professor)(Session["Professor"]);

                    listaNotaTurma = notaDAL.GetListaNotaTurma(professor.listaTurmas.FirstOrDefault().id);
                    listaNotaCurso = notaDAL.GetListaNotaCurso(professor.listaCurso.FirstOrDefault().id);

                    nomeTurma = turmaDAL.GetTurma(idTurma).Nome;
                    nomeCurso = cursoDAL.GetCurso(professor.listaCurso.FirstOrDefault().id).Nome;


                }
                else
                {
                    var coordenador = (Coordenador)(Session["Coordenador"]);

                    listaNotaTurma = notaDAL.GetListaNotaTurmaCoordenador(coordenador.listaCurso.FirstOrDefault().id);
                    listaNotaCurso = notaDAL.GetListaNotaCurso(coordenador.listaCurso.FirstOrDefault().id);

                }

                string semestreChart = string.Empty;
                string notaChart = string.Empty;
                string notaTurmaChart = string.Empty;
                string notaCursoChart = string.Empty;

                foreach (var item in listaNotas)
                {
                    notaChart += item.nota + ",";
                    semestreChart += item.Semestre + " Semestre , ";
                }

                foreach (var item in listaNotaTurma)
                {
                    notaTurmaChart += item.nota + ",";
                }

                foreach (var item in listaNotaCurso)
                {
                    notaCursoChart += item.nota + ",";
                }

                ViewBag.Semestre = semestreChart;
                ViewBag.nota = notaChart;
                ViewBag.notaTurma = notaTurmaChart;
                ViewBag.notaCurso = notaCursoChart;

                ViewBag.NomeCurso = nomeCurso;
                ViewBag.NomeTurma = nomeTurma;
            }
        }



        public void ExportarNotasIndicadores(int idTurma ,int idCurso = 0, int Semestre = 0)
        {

            try
            {
                NotaDAL notaDAL = new NotaDAL();

                var usuario = (Usuario)Session["Usuario"];

                DataTable listaNotasTurma = new DataTable();
                DataTable listaNotasCurso = new DataTable();
                DataTable listaNotasAluno = new DataTable();

                ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();

                if (usuario.tipoPerfil == 1)
                {
                    var aluno = (Aluno)Session["aluno"];
                    listaNotasCurso = notaDAL.ExportarPlanilhaIndicadoresCurso(aluno.idCurso, aluno.Semestre);
                    listaNotasAluno = notaDAL.ExportarPlanilha(usuario.RA);

                    wbook.Worksheets.Add(listaNotasAluno, "Acertos Aluno");
                }
                else
                {
                    listaNotasCurso = notaDAL.ExportarPlanilhaIndicadoresCurso(idCurso, Semestre);
                }

                listaNotasTurma = notaDAL.ExportarPlanilhaIndicadoresTurma(idTurma);

                wbook.Worksheets.Add(listaNotasTurma, "Acertos Turma");
                wbook.Worksheets.Add(listaNotasCurso, "Acertos Curso");

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Provide you file name here
                Response.AddHeader("content-disposition", "attachment;filename=\"MinhasNotasIndicadores.xlsx\"");

                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    memoryStream.Close();
                }

                Response.End();
            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }



        private void CarregarComboBox()
        {
            TurmaDAL turmaDAL = new TurmaDAL();
            CursoDAL cursoDAL = new CursoDAL();
            CampusDAL campusDAL = new CampusDAL();

            List<Turma> listaTurmas = new List<Turma>();
            List<Curso> listaCursos = new List<Curso>();

            var usuario = (Usuario)Session["Usuario"];

            if (usuario.tipoPerfil == 1)
            {
                var aluno = (Aluno)Session["Aluno"];

                var listaTurmasPorCurso = turmaDAL.GetTurmas(aluno.idCurso, aluno.Semestre);
                listaTurmas = listaTurmasPorCurso;
                ViewBag.listaTurmas = listaTurmas.Select(t => new SelectListItem { Value = t.id.ToString(), Text = t.Nome});
            }
            else if (usuario.tipoPerfil == 2)
            {
                var prof = (Professor)Session["Professor"];

                foreach (var item in prof.listaTurmas)
                {
                    listaTurmas.Add(item);
                }

                foreach (var item in prof.listaCurso)
                {
                    listaCursos.Add(item);
                }

            }
            else
            {
                listaTurmas = turmaDAL.GetTurmas();
                listaCursos = cursoDAL.GetCursos();
            }

           
            ViewBag.listaCursos = listaCursos.Select(c => new SelectListItem { Value = c.id.ToString(), Text = c.Nome + " - " + c.Turno });

        }

    }
}