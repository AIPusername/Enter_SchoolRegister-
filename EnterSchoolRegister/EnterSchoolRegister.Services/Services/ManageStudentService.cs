using AutoMapper;
using DataAccessLayer.Core.Interfaces.UoW;
using EnterSchoolRegister.BLL.Entities;
using EnterSchoolRegister.Services.Interfaces;
using EnterSchoolRegister.ViewModels.ServicesViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterSchoolRegister.Services.Services
{
    public class ManageStudentService : BaseService, IManageStudentService
    {
        public ManageStudentService(IUnitOfWork uow, ILoggerFactory loggerFactory) : base(uow, loggerFactory)
        {
        }

        public void AddGrade(GradingVm addGradeVm)
        {
            var grade = Mapper.Map<Grade>(addGradeVm);
            UoW.Repository<Grade>().Add(grade);
            UoW.Save();
        }

        public void AddStudent(AddStudentVm addStudentVm)
        {
            var student = Mapper.Map<Student>(addStudentVm);
            UoW.Repository<Student>().Add(student);
            UoW.Save();
        }

        public void RemoveGrade(RemoveGradeVm removeGradeVm)
        {
            var grade = Mapper.Map<Grade>(removeGradeVm);
            UoW.Repository<Grade>().Delete(grade);
            UoW.Save();
        }

        public void RemoveStudent(RemoveStudentVm removeStudentVm)
        {
            var student = Mapper.Map<Student>(removeStudentVm);
            UoW.Repository<Student>().Delete(student);
            UoW.Save();
        }
    }
}
