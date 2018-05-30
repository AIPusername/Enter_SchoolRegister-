using EnterSchoolRegister.ViewModels.ServicesViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterSchoolRegister.Services.Interfaces
{
    public interface IManageStudentService
    {
        //Add method to get list based on course and, if need, to get singe student
        //Add method to get list based on parent
        void AddStudent(AddStudentVm addStudentVm);
        void RemoveStudent(RemoveStudentVm removeStudentVm);

        //Add method to get list
        void AddGrade(GradingVm addGradeVm);
        void RemoveGrade(RemoveGradeVm removeGradeVm);
    }
}
