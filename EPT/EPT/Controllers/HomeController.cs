using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using EPTDal;
using EPTEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UsuarioRA"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            GetNotas();

            return View();
        }
        public void GetNotas()
        {
            NotaDAL notaDAL = new NotaDAL();

            var ra = Convert.ToInt32(Session["UsuarioRA"]);

            List<Nota> listaNotas = notaDAL.GetListaNotas(ra);
            string semestreChart = string.Empty;
            string notaChart = string.Empty;

            foreach (var item in listaNotas)
            {
                notaChart += item.nota + ",";
                semestreChart += item.Semestre + " Semestre , ";
            }

            ViewBag.Semestre = semestreChart;
            ViewBag.nota = notaChart;

        }

        public void ExportarNotas() 
        {
            NotaDAL notaDAL = new NotaDAL();

            var ra = Convert.ToInt32(Session["UsuarioRA"]);

            var listaNotas = notaDAL.ExportarPlanilha(ra);

            ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
            wbook.Worksheets.Add(listaNotas, "tab1");
          
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Provide you file name here
            Response.AddHeader("content-disposition", "attachment;filename=\"MinhasNotas.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbook.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                memoryStream.Close();
            }

            Response.End();
        }

    }
}