using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using BTCD_System.Models;
using BTCD_System.BTCD_DLL.Reports;

namespace BTCD_System.Controllers
{
    public class ReportsTrnController : Controller
    {
        clsReports reports = new clsReports();
        // GET: Reports

        [Authorize(Roles = "Reports - Reports")]
        public ActionResult Index()
        {
            List<Reports> lstEmsReportses; //= reports.SelectAllReportslist();
            return View();
        }

        [HttpPost]
        public ActionResult Details(string reportName, string serverPath, string reportFolder)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Pixel(1100);
            reportViewer.Height = Unit.Pixel(525);

            reportViewer.ServerReport.ReportServerUrl = new Uri(serverPath);
            reportViewer.ServerReport.ReportPath = @"/" + reportFolder + @"/" + reportName ;

            ViewBag.ReportViewer = reportViewer;

            return View();
            //return Redirect("http://www.google.com");
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reports/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reports/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
