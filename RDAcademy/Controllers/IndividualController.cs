using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDAcademy.Models;
using RDAcademy.DAL;

namespace RDAcademy.Controllers
{
    public class IndividualController : Controller
    {
        private IndividualContext db = new IndividualContext();

        //
        // GET: /Individual/

        public ActionResult Index()
        {
            return View(db.Individuals.ToList());
        }

        //
        // GET: /Individual/Details/5

        public ActionResult Details(int id = 0)
        {
            Individual individual = db.Individuals.Find(id);
            if (individual == null)
            {
                return HttpNotFound();
            }
            return View(individual);
        }

        //
        // GET: /Individual/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Individual/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Individual individual)
        {
            if (ModelState.IsValid)
            {
                db.Individuals.Add(individual);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(individual);
        }

        //
        // GET: /Individual/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Individual individual = db.Individuals.Find(id);
            if (individual == null)
            {
                return HttpNotFound();
            }
            return View(individual);
        }

        //
        // POST: /Individual/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Individual individual)
        {
            if (ModelState.IsValid)
            {
                db.Entry(individual).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(individual);
        }

        //
        // GET: /Individual/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Individual individual = db.Individuals.Find(id);
            if (individual == null)
            {
                return HttpNotFound();
            }
            return View(individual);
        }

        //
        // POST: /Individual/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Individual individual = db.Individuals.Find(id);
            db.Individuals.Remove(individual);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}