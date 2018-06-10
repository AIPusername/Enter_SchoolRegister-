using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Core.Interfaces.UoW;
using EnterSchoolRegister.BLL.Entities;
using EnterSchoolRegister.Services.Interfaces;
using EnterSchoolRegister.Services.Services;
using EnterSchoolRegister.ViewModels.EntitiesViewModels;
using EnterSchoolRegister.ViewModels.ServicesViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EnterSchoolRegister.Web.Controllers
{
    public class CourseController : BaseController
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<User> _userManager;

        public CourseController(IUnitOfWork uow, ILoggerFactory loggerFactory,
                                   ICourseService cc, UserManager<User> um) : base(uow, loggerFactory)
        {
            _courseService = cc;
            _userManager = um;
        }

        public IActionResult CoursesMain()
        {
            IEnumerable<CourseVm> coursesVm = _courseService.GetCourses(_userManager
                .FindByNameAsync(HttpContext.User.Identity.Name).Result.Id);
            if (HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
                return PartialView(coursesVm);
            else
                return View(coursesVm);
        }

        [HttpPost]
        public JsonResult AddCourse(AddCourseVm addCourseVm)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            addCourseVm.TeacherId = user.Id;
            bool added = _courseService.AddCourse(addCourseVm);
            return Json(new { success = added });
        }

        [HttpPost]
        public JsonResult RemoveCourse(RemoveCourseVm removeCourseVm)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            removeCourseVm.TeacherId = user.Id;
            _courseService.RemoveCourse(removeCourseVm);
            return Json(new { success = true });
        }

        public IActionResult CourseStudent()
        {
            IEnumerable<CourseStudentVm> courseStudentsVm = _courseService.GetAttendances(_userManager
                .FindByNameAsync(HttpContext.User.Identity.Name).Result.Id);
            if (HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
                return PartialView(courseStudentsVm);
            else
                return View(courseStudentsVm);
        }
        
        [HttpPost]
        public JsonResult AddCourseStudent(AddRemoveCourseStudentVm model)
        {
            bool added = _courseService.AddCourseStudent(model);
            return Json(new { success = added });
        }

        [HttpPost]
        public JsonResult RemoveCourseStudent(AddRemoveCourseStudentVm model)
        {
            _courseService.RemoveCourseStudent(model);
            return Json(new { success = true });
        }

        public IActionResult GradeMain()
        {
            IEnumerable<GradeVm> gradeVm = _courseService.GetListOfGrades(_userManager
               .FindByNameAsync(HttpContext.User.Identity.Name).Result.Id);
            if (HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
                return PartialView(gradeVm);
            else
                return View(gradeVm);
        }

        [HttpGet]
        public IEnumerable<GradeVm> ListOfGrades(AddRemoveCourseStudentVm model)
        {
            IEnumerable<GradeVm> gradesVm = _courseService.GetListOfGrades(model.CourseId, model.StudentSerialNumber);
            return gradesVm;
        }

        [HttpPost]
        public JsonResult Grade(GradingVm model)
        {
            bool added = _courseService.AddGrade(model);
            return Json(new { success = added });
        }

        [HttpPost]
        public JsonResult RemoveGrade(GradingVm model)
        {
            _courseService.RemoveGrade(model);
            return Json(new { success = true });
        }
    }
}