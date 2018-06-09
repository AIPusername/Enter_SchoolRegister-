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
        IEnumerable<CourseVm> GetCourses(int teacherId);
        bool AddCourse(AddCourseVm addCourseVm);
        void RemoveCourse(RemoveCourseVm removeCourseVm);

        IEnumerable<CourseStudentVm> GetAttendances(int teacherId);
        bool AddCourseStudent(AddRemoveCourseStudentVm model);
        void RemoveCourseStudent(AddRemoveCourseStudentVm model);
        bool SomeoneIsAttending(int courseId);

        IEnumerable<GradeVm> GetListOfGrades(int courseId, int studentId);
        void AddGrade(GradingVm model);
        void RemoveGrade(GradingVm model);
    }
}
