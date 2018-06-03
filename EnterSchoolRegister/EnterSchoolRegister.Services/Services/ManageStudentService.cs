using AutoMapper;
using DataAccessLayer.Core.Interfaces.UoW;
using EnterSchoolRegister.BLL.Entities;
using EnterSchoolRegister.Services.Interfaces;
using EnterSchoolRegister.ViewModels.EntitiesViewModels;
using EnterSchoolRegister.ViewModels.ServicesViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterSchoolRegister.Services.Services
{
    //Has to be written
    public class ManageStudentService : BaseService, IManageStudentService
    {
        public ManageStudentService(IUnitOfWork uow, ILoggerFactory loggerFactory) : base(uow, loggerFactory)
        {
        }

        public void AddStudent(AddStudentVm addStudentVm)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentVm> GetStudents()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentVm> GetStudentsByCourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentVm> GetStudentsByParent(int parentId)
        {
            throw new NotImplementedException();
        }

        public void RemoveStudent(RemoveStudentVm removeStudentVm)
        {
            throw new NotImplementedException();
        }
    }
}
