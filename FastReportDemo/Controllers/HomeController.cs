using FastReport;
using FastReport.Export.PdfSimple;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace FastReportDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public FileResult Generate()
        {
            FastReport.Utils.Config.WebMode = true;

            Report rep = new Report();
            rep.Load(Server.MapPath("~/Employee.frx"));

            List<Models.Employee> emp = new List<Models.Employee>
    {
        new Models.Employee {FirstName="Khalil", LastName="Rahman", ContactNo="01917343357" },
        new Models.Employee { FirstName="Hamid", LastName="Khan", ContactNo="01917343357" },
        new Models.Employee { FirstName="Abul", LastName="Khan", ContactNo="01917343357" }
    };

            rep.RegisterData(emp, "EmployeeRef");

            //var ds = rep.GetDataSource("Employee");
            //ds.Enabled = true;
            //ds.Init();

            rep.Prepare();

            PDFSimpleExport pdf = new PDFSimpleExport();
            MemoryStream ms = new MemoryStream();

            rep.Export(pdf, ms);

            ms.Position = 0;
            return File(ms.ToArray(), "application/pdf");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}