using FastReport;
using FastReport.Export.PdfSimple;
using FastReportDemo.Models;
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
            Report rep= new Report();
            string path = Server.MapPath("~/Employee.frx");

            rep.Load(path);

            List<Employee> emp= new List<Employee>();
            emp.Add(new Employee() { FirstName = "Khalil", LastName = "Rahman", ContactNo = "01917343357" });
            emp.Add(new Employee() { FirstName = "Hamid", LastName = "Khan", ContactNo = "01917343357" });
            emp.Add(new Employee() { FirstName = "Abul", LastName = "Khan", ContactNo = "01917343357" });

            rep.SetParameterValue("parm1", "This is frist Param");
            rep.SetParameterValue("parm2", "This is Second Param");

            rep.RegisterData(emp, "EmployeeRef");
              
            if(rep.Report.Prepare())
            {
                PDFSimpleExport pdfExport = new PDFSimpleExport();
                pdfExport.ShowProgress = true;
                pdfExport.Subject = "Deno Reports";
                pdfExport.Title = "Report Title";

                MemoryStream ms = new MemoryStream();

                rep.Report.Export(pdfExport,ms);
                rep.Dispose();
                pdfExport.Dispose();

                ms.Position = 0;

                return File(ms,"application/pdf", "myreport.pdf");

            }
            else
            {
                return null;
            }
;
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