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
        IEnumerable<CourseVm> GetCourses();
        IEnumerable<CourseVm> GetCourses(int teacherId);
        bool AddCourse(AddCourseVm addCourseVm);
        void RemoveCourse(RemoveCourseVm removeCourseVm);

        /*Add methods, ViewModels and anything needed to add/remove a student into a course*/

        IEnumerable<GradeVm> GetListOfGrades(int courseId, int studentId);
        void AddGrade(GradingVm addGradeVm);
        void RemoveGrade(RemoveGradeVm removeGradeVm);
    }
}
