using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Tasker.Data.Helpers;
using Tasker.Data.Interfaces;
using Tasker.Data.Managers;
using Tasker.Data.Model;
using Tasker.Data.Model.Enum;
using Tasker.Web.Models;

namespace Tasker.Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IDateService _dateService;
        private readonly CustomUserManager _userManager;

        public TaskController(ITaskService taskService, IDateService dateService)
        {
            _taskService = taskService;
            _dateService = dateService;
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
        public ActionResult Create(TaskViewModel model)
        {
            Guid userId;
            if (!Guid.TryParse(System.Web.HttpContext.Current.User.Identity.GetUserId(), out userId))
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                var user = _userManager.FindById(userId);
                _taskService.Save(new Task
                {
                    Name = model.Name,
                    DateEnd = model.DateEnd,
                    Status = TaskStatus.New,
                    AssignedToUser = user,
                    Created = _dateService.GetCurrentDateUtc(),
                    CreatedBy = user
                });

                return RedirectToAction("Index");
            }

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

            var model = new TaskViewModel
            {
                Id = task.Id,
                DateEnd = task.DateEnd,
                Name = task.Name,
                Status = task.Status ?? TaskStatus.None
            };

            return View(model);
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

            var model = new TaskViewModel
            {
                Id = task.Id,
                DateEnd = task.DateEnd,
                Name = task.Name,
                Status = task.Status ?? TaskStatus.None
            };

            return View(model);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskViewModel model)
        {
            Guid userId;
            if (!Guid.TryParse(System.Web.HttpContext.Current.User.Identity.GetUserId(), out userId))
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                var task = _taskService.FindById(model.Id, userId);
                task.DateEnd = model.DateEnd;
                task.Name = model.Name;
                task.Status = model.Status;

                _taskService.Save(task);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET and POST
        public ActionResult Index(string mask, int? page)
        {
            ViewBag.TaskNameMask = mask;

            Guid userId;
            if (!Guid.TryParse(System.Web.HttpContext.Current.User.Identity.GetUserId(), out userId))
            {
                return View();
            }

            var tasks = _taskService.FindByName(mask, userId)
                .Select(x => new TaskViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    DateEnd = x.DateEnd,
                    Status = x.Status ?? TaskStatus.None,
                    Color = GetTaskColor(x)
                });
            var model = tasks.AsPagedList(page);

            return View(model);
        }

        private string GetTaskColor(Task x)
        {
            var currentDate = _dateService.GetCurrentDateUtc();
            if (x.Status == TaskStatus.Done) return "Green";
            if (x.DateEnd.HasValue && x.DateEnd.Value.Date == currentDate.Date) return "Blue";
            if (x.DateEnd < currentDate && x.Status != TaskStatus.Done) return "Red";

            return "Grey";
        }
    }
}