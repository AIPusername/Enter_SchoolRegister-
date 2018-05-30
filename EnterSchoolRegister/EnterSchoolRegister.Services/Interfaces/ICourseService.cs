using EnterSchoolRegister.BLL.Entities;
using EnterSchoolRegister.ViewModels.EntitiesViewModels;
using EnterSchoolRegister.ViewModels.ServicesViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EnterSchoolRegister.Services.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<CourseVm> GetCourses(Expression<Func<Course, bool>> filterPredicate = null);
        void AddCourse(AddCourseVm addCourseVm);
        void RemoveCourse(RemoveCourseVm removeCourseVm);
    }
}
