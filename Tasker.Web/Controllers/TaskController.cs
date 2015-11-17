using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Tasker.Data.DAL;
using Tasker.Data.Manager;
using Tasker.Data.Model;
using Tasker.Services.Intefaces;

namespace Tasker.Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly TaskerDbContext _dbContext;
        private readonly ITaskService _taskService;
        private readonly CustomUserManager _userManager;

        public TaskController(TaskerDbContext dbContext, ITaskService taskService)
        {
            _dbContext = dbContext;
            _taskService = taskService;
            _userManager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<CustomUserManager>();
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Task task)
        {
            Guid userId;
            if (!Guid.TryParse(System.Web.HttpContext.Current.User.Identity.GetUserId(), out userId))
            {
                return View();
            }

            //if (ModelState.IsValid)
            {
                task.Id = Guid.NewGuid();
                task.Created = DateTime.UtcNow;
                task.AssignedToUser = _userManager.FindById(userId);

                _dbContext.Tasks.Add(task);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(task);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Task task)
        {
            Guid userId;
            if (!Guid.TryParse(System.Web.HttpContext.Current.User.Identity.GetUserId(), out userId))
            {
                return View();
            }

            //if (ModelState.IsValid)
            {
                var user = _userManager.FindById(userId);
                task.AssignedToUser = user;
                task.Modifed = DateTime.UtcNow;
                task.ModifedBy = user;

                _taskService.Save(task);

                return RedirectToAction("Index");
            }

            return View(task);
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