using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tasker.Common.Intefaces;
using Tasker.Data.DAL;
using Tasker.Data.Model;

namespace Tasker.Web.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private readonly TaskerDbContext _dbContext;
        private readonly IJobService _jobService;

        public JobsController(TaskerDbContext dbContext, IJobService jobService)
        {
            _dbContext = dbContext;
            _jobService = jobService;
        }

        // GET: Jobs
        public ActionResult Index()
        {
            Guid userId;
            if (!Guid.TryParse(System.Web.HttpContext.Current.User.Identity.GetUserId(), out userId))
            {
                return View();
            }

            var model = _jobService.GetAll(userId);

            return View(model);
        }

        // GET: Jobs/Details/5
        public ActionResult Details(Guid? id)
        {
            Guid userId;
            if (!Guid.TryParse(System.Web.HttpContext.Current.User.Identity.GetUserId(), out userId))
            {
                return View();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = _jobService.FindById(id.Value, userId);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DateEnd,Status,Created,Modifed")] Job job)
        {
            if (ModelState.IsValid)
            {
                job.Id = Guid.NewGuid();
                _dbContext.Jobs.Add(job);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            Guid userId;
            if (!Guid.TryParse(System.Web.HttpContext.Current.User.Identity.GetUserId(), out userId))
            {
                return View();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = _jobService.FindById(id.Value, userId);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DateEnd,Status,Created,Modifed")] Job job)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(job).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            Guid userId;
            if (!Guid.TryParse(System.Web.HttpContext.Current.User.Identity.GetUserId(), out userId))
            {
                return View();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = _jobService.FindById(id.Value, userId);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var job = _dbContext.Jobs.Find(id);
            _dbContext.Jobs.Remove(job);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}