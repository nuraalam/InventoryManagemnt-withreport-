using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using InventoryApp.Content;
using InventoryApp.Models;

namespace InventoryApp.Controllers
{
    public class ClientInformationController : Controller
    {


        private ProjectDb db = new ProjectDb();




        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


        public ActionResult GenerateReport()
        {
            List<ClientInformation> list = db.ClientInformations.ToList();

           // CrystalDecisions.CrystalReports.Engine.ReportClass rptH = new ReportClass();
            ReportClass rptH = new ReportClass();
            rptH.FileName = Server.MapPath("/Content/ClientInformationReport.rpt");

            rptH.Load();

            DataTable table = ToDataTable(list);
            rptH.SetDataSource(table);

            System.IO.Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return File(stream, "application/pdf");


            //SqlConnection con =
            //    new SqlConnection("Data Source=Hafiz;Initial Catalog=InventoryDb;MultipleActiveResultSets=True;Integrated Security=True");

            //DataTable dt = new DataTable();

            //try

            //{

            //    con.Open();

            //    SqlCommand cmd = new SqlCommand("SELECT * FROM ClientInformations", con);

            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);

            //    adp.Fill(dt);



            //}

            //catch (Exception ex)

            //{
            //}

            //ReportClass rptH = new ReportClass();
            //rptH.FileName = Server.MapPath("/Content/ClientInformationReport.rpt");
            //rptH.Load();

            //rptH.SetDataSource();
            //Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            ////return File(stream, "application/pdf");
            //return File(stream, "application/pdf");


        }

        // GET: /ClientInformation/
        public ActionResult Index()
        {
            return View(db.ClientInformations.ToList());
        }

        // GET: /ClientInformation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientInformation clientinformation = db.ClientInformations.Find(id);
            if (clientinformation == null)
            {
                return HttpNotFound();
            }
            return View(clientinformation);
        }

        // GET: /ClientInformation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ClientInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "ClientInformationId,ClientCode,ClientName,ContactPerson,Address,Email,MobileNumber")] ClientInformation clientinformation)
        {
            clientinformation.CreatedDateTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.ClientInformations.Add(clientinformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clientinformation);
        }

        // GET: /ClientInformation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientInformation clientinformation = db.ClientInformations.Find(id);
            if (clientinformation == null)
            {
                return HttpNotFound();
            }
            return View(clientinformation);
        }

        // POST: /ClientInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "ClientInformationId,ClientCode,ClientName,ContactPerson,Address,Email,MobileNumber")] ClientInformation clientinformation)
        {
            clientinformation.CreatedDateTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(clientinformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientinformation);
        }

        // GET: /ClientInformation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientInformation clientinformation = db.ClientInformations.Find(id);
            if (clientinformation == null)
            {
                return HttpNotFound();
            }
            return View(clientinformation);
        }

        // POST: /ClientInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientInformation clientinformation = db.ClientInformations.Find(id);
            db.ClientInformations.Remove(clientinformation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        public JsonResult ClientCodeExist(ClientInformation aClientInformation)
        {
            return
                Json(!db.ClientInformations.Where(c => c.ClientInformationId != aClientInformation.ClientInformationId)
                    .Any(c => c.ClientCode == aClientInformation.ClientCode),
                    JsonRequestBehavior.AllowGet);

        }

        public JsonResult ClientNameExist(ClientInformation aClientInformation)
        {
            return
                Json(!db.ClientInformations.Where(c => c.ClientInformationId != aClientInformation.ClientInformationId)
                    .Any(c => c.ClientName == aClientInformation.ClientName),
                    JsonRequestBehavior.AllowGet);
        }


        public JsonResult EmailExist(ClientInformation aClientInformation)
        {
            return
                Json(!db.ClientInformations.Where(c => c.ClientInformationId != aClientInformation.ClientInformationId)
                    .Any(c => c.Email == aClientInformation.Email),
                    JsonRequestBehavior.AllowGet);

        }

        public JsonResult MobileNumberExist(ClientInformation aClientInformation)
        {
            return
                Json(!db.ClientInformations.Where(c => c.ClientInformationId != aClientInformation.ClientInformationId)
                    .Any(c => c.MobileNumber == aClientInformation.MobileNumber),
                    JsonRequestBehavior.AllowGet);
        }
    }
}
