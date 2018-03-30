using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManyToManyMVC5.Models;
using ManyToManyMVC5.ViewModels;

namespace ManyToManyMVC5.Controllers
{
    public class JobPostController : Controller
    {
        private readonly JobPortalEntities _db = new JobPortalEntities();

        // GET: /JobPost/
        public ActionResult Index()
        {
			var jobposts = _db.JobPosts
				.Include(j => j.Employer);
            return View(jobposts.ToList());
        }

        // GET: /JobPost/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPost jobpost = _db.JobPosts.Find(id);
            if (jobpost == null)
            {
                return HttpNotFound();
            }
            return View(jobpost);
        }

        // GET: /JobPost/Create
        public ActionResult Create()
        {
            ViewBag.EmployerID = new SelectList(_db.Employers, "Id", "FullName");
            return View();
        }

        // POST: /JobPost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Title,EmployerID")] JobPost jobpost)
        {
            if (ModelState.IsValid)
            {
                _db.JobPosts.Add(jobpost);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployerID = new SelectList(_db.Employers, "Id", "FullName", jobpost.EmployerID);
            return View(jobpost);
        }

        // GET: /JobPost/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
	        var jobpostViewModel = new JobPostViewModel{
		        JobPost = _db.JobPosts.Include(i => i.JobTags).First(i => i.Id == id) };
			
			if (jobpostViewModel.JobPost == null)
				return HttpNotFound();
			var allJobTagsList = _db.JobTags.ToList();
			
			jobpostViewModel.AllJobTags = allJobTagsList.Select(o => new SelectListItem
			{
				Text = o.Tag,
				Value = o.Id.ToString()
			});

			ViewBag.EmployerID = new SelectList(_db.Employers, "Id", "FullName", jobpostViewModel.JobPost.EmployerID);
			return View(jobpostViewModel);
        }


		// Not bind if the name is jobpost
        // POST: /JobPost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
		[ValidateAntiForgeryToken]//[Bind(Include = "Title,Id,EmployerID,SelectedJobTags")]
		public ActionResult Edit(JobPostViewModel jobpostView)
        {
	    
			if (jobpostView == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			

            if (ModelState.IsValid)
            {
				var jobToUpdate = _db.JobPosts
					.Include(i => i.JobTags).First(i => i.Id == jobpostView.JobPost.Id);

	            if (TryUpdateModel(jobToUpdate,"JobPost",new string[]{"Title","EmployerID"} ))
	            {
		            var newJobTags = _db.JobTags.Where(
						m => jobpostView.SelectedJobTags.Contains(m.Id)).ToList();
					var updatedJobTags = new HashSet<int>(jobpostView.SelectedJobTags);
					foreach (JobTag jobTag in _db.JobTags)
					{
						if (!updatedJobTags.Contains(jobTag.Id))
						{
							jobToUpdate.JobTags.Remove(jobTag);
						}
						else
						{
							jobToUpdate.JobTags.Add((jobTag));
						}
					}

					_db.Entry(jobToUpdate).State = System.Data.Entity.EntityState.Modified;
					_db.SaveChanges();
	            }
			     
                return RedirectToAction("Index");
            }
            ViewBag.EmployerID = new SelectList(_db.Employers, "Id", "FullName", jobpostView.JobPost.EmployerID);
            return View(jobpostView);
        }

        // GET: /JobPost/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



	        JobPost jobpost = _db.JobPosts
		        .Include(j => j.JobTags)
		        .First(j => j.Id == id);

            if (jobpost == null)
            {
                return HttpNotFound();
            }
            return View(jobpost);
        }

        // POST: /JobPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {


			JobPost jobpost = _db.JobPosts
				  .Include(j => j.JobTags)
				  .First(j => j.Id == id);
            _db.JobPosts.Remove(jobpost);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
