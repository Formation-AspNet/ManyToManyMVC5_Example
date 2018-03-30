using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManyToManyMVC5.Models;

namespace ManyToManyMVC5.Controllers
{
    public class JobTagsController : Controller
    {
        private JobPortalEntities db = new JobPortalEntities();

        // GET: JobTags
        public ActionResult Index()
        {
            return View(db.JobTags.ToList());
        }

        // GET: JobTags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTag jobTag = db.JobTags.Find(id);
            if (jobTag == null)
            {
                return HttpNotFound();
            }
            return View(jobTag);
        }

        // GET: JobTags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobTags/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tag")] JobTag jobTag)
        {
            if (ModelState.IsValid)
            {
                db.JobTags.Add(jobTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobTag);
        }

        // GET: JobTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTag jobTag = db.JobTags.Find(id);
            if (jobTag == null)
            {
                return HttpNotFound();
            }
            return View(jobTag);
        }

        // POST: JobTags/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tag")] JobTag jobTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobTag);
        }

        // GET: JobTags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTag jobTag = db.JobTags.Find(id);
            if (jobTag == null)
            {
                return HttpNotFound();
            }
            return View(jobTag);
        }

        // POST: JobTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobTag jobTag = db.JobTags.Find(id);
            db.JobTags.Remove(jobTag);
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
    }
}
