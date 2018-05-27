using EnterSchoolRegister.ViewModels.ServicesViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterSchoolRegister.Services.Interfaces
{
    public interface ICourseService
    {
        void AddCourse(AddCourseVm addCourseVm);
        void RemoveCourse(RemoveCourseVm removeCourseVm);
    }
}
