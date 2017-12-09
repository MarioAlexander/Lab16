using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab16Github.Models;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Web.Helpers;
using System.Collections;

namespace Lab16Github.Controllers
{
    public class PersonController : Controller
    {

        private SchoolEntities _contexto;
        // GET: Person


        public SchoolEntities Contexto
        {
            set { _contexto = value; }
            get
            {
                if (_contexto == null)
                    _contexto = new SchoolEntities();
                return _contexto;
            }
        }
        public ActionResult Index()
        {
            return View(Contexto.Person.ToList());
        }

        public ActionResult Reporte()
        {
            List<Person> listado = new List<Person>();
            listado = Contexto.Person.ToList();

            var rptviewer = new ReportViewer();
            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reporte.rdlc";

            ReportDataSource rptdatasource = new ReportDataSource("dsPersona", listado);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            rptviewer.SizeToReportContent = true;

            ViewBag.ReportViewer = rptviewer;
            return View();
        }

        [HttpPost]
        public ActionResult Reporte(string FirstName)
        {
            List<Person> listado = new List<Person>();
            listado = (from p in Contexto.Person
                       where p.FirstName.Contains(FirstName)
                       select p).ToList();

            var rptviewer = new ReportViewer();
            rptviewer.ProcessingMode = ProcessingMode.Local;
            rptviewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reporte.rdlc";

            ReportDataSource rptdatasource = new ReportDataSource("dsPersona", listado);
            rptviewer.LocalReport.DataSources.Add(rptdatasource);
            rptviewer.SizeToReportContent = true;

            ViewBag.ReportViewer = rptviewer;
            return View();
        }

        public ActionResult Graficos()
        {
            return View();
        }

        public ActionResult GraficoColumnas()
        {
            ArrayList x = new ArrayList();
            ArrayList y = new ArrayList();

            var query = (from c in Contexto.Course select c);
            query.ToList().ForEach(r => x.Add(r.Title));
            query.ToList().ForEach(r => y.Add(r.Credits));
            new Chart(width: 600, height: 400, theme: ChartTheme.Green)
                .AddTitle("Columnas")
                .AddSeries("Default", chartType: "Column", xValue: x, yValues: y)
                .Write("bmp");
            return null;
        }
        public ActionResult GraficoBarras()
        {
            ArrayList x = new ArrayList();
            ArrayList y = new ArrayList();

            var query = (from c in Contexto.Course select c);
            query.ToList().ForEach(r => x.Add(r.Title));
            query.ToList().ForEach(r => y.Add(r.Credits));
            new Chart(width: 600, height: 400, theme: ChartTheme.Vanilla3D)
                .AddTitle("Barras")
                .AddSeries("Default", chartType: "Bar", xValue: x, yValues: y)
                .Write("bmp");
            return null;
        }
        public ActionResult GraficoTarta()
        {
            ArrayList x = new ArrayList();
            ArrayList y = new ArrayList();

            var query = (from c in Contexto.Course select c);
            query.ToList().ForEach(r => x.Add(r.Title));
            query.ToList().ForEach(r => y.Add(r.Credits));
            new Chart(width: 600, height: 400, theme: ChartTheme.Blue)
                .AddTitle("P I E")
                .AddSeries("Default", chartType: "Pie", xValue: x, yValues: y)
                .Write("bmp");
            return null;
        }
        public ActionResult GraficoRadar()
        {
            ArrayList x = new ArrayList();
            ArrayList y = new ArrayList();

            var query = (from c in Contexto.Course select c);
            query.ToList().ForEach(r => x.Add(r.Title));
            query.ToList().ForEach(r => y.Add(r.Credits));
            new Chart(width: 600, height: 400, theme: ChartTheme.Blue)
                .AddTitle("Radar")
                .AddSeries("Default", chartType: "Radar", xValue: x, yValues: y)
                .Write("bmp");
            return null;
        }
    }
}