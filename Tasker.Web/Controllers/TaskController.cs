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
    public class TaskController : Controller
    {
        private readonly TaskerDbContext _dbContext;
        private readonly ITaskService _taskService;

        public TaskController(TaskerDbContext dbContext, ITaskService taskService)
        {
            _dbContext = dbContext;
            _taskService = taskService;
        }

        // GET: Tasks
        public ActionResult Index()
        {
            Guid userId;
            if (!Guid.TryParse(System.Web.HttpContext.Current.User.Identity.GetUserId(), out userId))
            {
                return View();
            }

            var model = _taskService.GetAll(userId);

            return View(model);
        }

        // GET: Tasks/Details/5
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

            var task = _taskService.FindById(id.Value, userId);
            if (task == null)
            {
                return HttpNotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DateEnd,Status,Created,Modifed")] Task task)
        {
            if (ModelState.IsValid)
            {
                task.Id = Guid.NewGuid();
                _dbContext.Tasks.Add(task);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(task);
        }

        // GET: Tasks/Edit/5
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

            var task = _taskService.FindById(id.Value, userId);
            if (task == null)
            {
                return HttpNotFound();
            }

            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DateEnd,Status,Created,Modifed")] Task task)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(task).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        // GET: Tasks/Delete/5
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

            var task = _taskService.FindById(id.Value, userId);
            if (task == null)
            {
                return HttpNotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var task = _dbContext.Tasks.Find(id);
            _dbContext.Tasks.Remove(task);
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