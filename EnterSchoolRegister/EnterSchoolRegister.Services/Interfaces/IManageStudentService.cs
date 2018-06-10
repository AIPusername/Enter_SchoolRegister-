using EnterSchoolRegister.BLL.Entities;
using EnterSchoolRegister.ViewModels.EntitiesViewModels;
using EnterSchoolRegister.ViewModels.ServicesViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterSchoolRegister.Services.Interfaces
{
    public interface IManageStudentService
    {
        IEnumerable<StudentVm> GetStudents();
        IEnumerable<StudentVm> GetStudentsByParent(int parentId);
        IEnumerable<StudentVm> GetStudentsByCourse(int courseId);
        IEnumerable<StudentVm> GetStudentsByGrade(int courseId);
        bool AddStudent(AddRemoveStudentVm model);
        void RemoveStudent(AddRemoveStudentVm model);
        bool IsAttendingSomething(int studentSn);
        bool HasAnyGrade(int courseId, int studentSn);
    }
}
