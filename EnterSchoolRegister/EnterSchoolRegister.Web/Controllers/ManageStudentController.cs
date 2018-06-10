using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Core.Interfaces.UoW;
using EnterSchoolRegister.BLL.Entities;
using EnterSchoolRegister.Services.Interfaces;
using EnterSchoolRegister.ViewModels.EntitiesViewModels;
using EnterSchoolRegister.ViewModels.ServicesViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EnterSchoolRegister.Web.Controllers
{
    public class ManageStudentController : BaseController
    {
        private readonly IManageStudentService _studentService;
        private readonly UserManager<User> _userManager;

        public ManageStudentController(IUnitOfWork uow, ILoggerFactory loggerFactory,
                                        IManageStudentService mss, UserManager<User> um) : base(uow, loggerFactory)
        {
            _studentService = mss;
            _userManager = um;
        }

        public IActionResult StudentsMain()
        {
            IEnumerable<StudentVm> studentsVm = _studentService.GetStudentsByParent(_userManager
                .FindByNameAsync(HttpContext.User.Identity.Name).Result.Id);
            if (HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
                return PartialView(studentsVm);
            else
                return View(studentsVm);
        }

        [HttpGet]
        public IEnumerable<StudentVm> StudentsByCourse(AddRemoveCourseStudentVm model)
        {
            IEnumerable<StudentVm> studentsVm = _studentService.GetStudentsByCourse(model.CourseId);
            return studentsVm;
        }

        [HttpGet]
        public IEnumerable<StudentVm> StudentsByGrade(AddRemoveCourseStudentVm model)
        {
            IEnumerable<StudentVm> studentsVm = _studentService.GetStudentsByGrade(model.CourseId);
            return studentsVm;
        }

        [HttpPost]
        public JsonResult AddStudent(AddRemoveStudentVm model)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            model.ParentId = user.Id;
            bool added = _studentService.AddStudent(model);
            return Json(new { success = added });
        }

        [HttpPost]
        public JsonResult RemoveStudent(AddRemoveStudentVm model)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            model.ParentId = user.Id;
            _studentService.RemoveStudent(model);
            return Json(new { success = true });
        }
    }
}