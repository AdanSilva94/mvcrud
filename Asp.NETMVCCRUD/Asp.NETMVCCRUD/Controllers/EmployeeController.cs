using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;
using System.Data.Entity;

namespace Asp.NETMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DataBaseModelo db = new DataBaseModelo())
            {
                List<contingencias> empList = db.contingencias.ToList<contingencias>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new contingencias());
            else
            {
                using (DataBaseModelo db = new DataBaseModelo())
                {
                    return View(db.contingencias.Where(x => x.idcontingencia==id).FirstOrDefault<contingencias>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(contingencias emp)
        {
            using (DataBaseModelo db = new DataBaseModelo())
            {
                if (emp.idcontingencia == 0)
                {
                    db.contingencias.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DataBaseModelo db = new DataBaseModelo())
            {
                contingencias emp = db.contingencias.Where(x => x.idcontingencia == id).FirstOrDefault<contingencias>();
                db.contingencias.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}