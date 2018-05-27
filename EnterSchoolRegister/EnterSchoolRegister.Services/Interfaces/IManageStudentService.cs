using EnterSchoolRegister.ViewModels.ServicesViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterSchoolRegister.Services.Interfaces
{
    public interface IManageStudentService
    {
        void AddStudent(AddStudentVm addStudentVm);
        void RemoveStudent(RemoveStudentVm removeStudentVm);

        void AddGrade(GradingVm addGradeVm);
        void RemoveGrade(RemoveGradeVm removeGradeVm);
    }
}
