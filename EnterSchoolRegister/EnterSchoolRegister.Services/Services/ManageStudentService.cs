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

        public IEnumerable<StudentVm> GetStudents()
        {
            IEnumerable<Student> students = UoW.Repository<Student>().GetRange(filterPredicate: s => s.Active,
                orderByPredicate: x => x.OrderBy(s => s.LastName), enableTracking: false);
            IEnumerable<StudentVm> studentsVm = AutoMapper.Mapper.Map<IEnumerable<StudentVm>>(students);
            return studentsVm;
        }

        public IEnumerable<StudentVm> GetStudentsByCourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentVm> GetStudentsByParent(int parentId)
        {
            IEnumerable<Student> students = UoW.Repository<Student>().GetRange(
                filterPredicate: s => s.ParentId == parentId && s.Active, orderByPredicate: x =>
                x.OrderBy(s => s.LastName), enableTracking: false);
            IEnumerable<StudentVm> studentsVm = AutoMapper.Mapper.Map<IEnumerable<StudentVm>>(students);
            return studentsVm;
        }

        public bool AddStudent(AddRemoveStudentVm model)
        {
            var student = Mapper.Map<Student>(model);
            var exists = UoW.Repository<Student>().Get(s => s.LastName.ToUpper().Equals(student.LastName.ToUpper()) &&
                                                            s.FirstName.ToUpper().Equals(student.FirstName.ToUpper()) &&
                                                            s.ParentId == student.ParentId);
            if( exists == null )
            {
                student.Active = true;
                UoW.Repository<Student>().Add(student);
                UoW.Save();
                return true;
            } else if( !exists.Active )
            {
                student.SerialNumber = exists.SerialNumber;
                student.Active = true;
                UoW.Repository<Student>().AddOrUpdate(s => s.SerialNumber == student.SerialNumber, student);
                UoW.Save();
                return true;
            }
            return false;
        }

        public void RemoveStudent(AddRemoveStudentVm model)
        {
            var student = Mapper.Map<Student>(model);
            student.Active = false;
            UoW.Repository<Student>().AddOrUpdate(s => s.LastName.ToUpper().Equals(student.LastName.ToUpper()) &&
                                                            s.FirstName.ToUpper().Equals(student.FirstName.ToUpper()) &&
                                                            s.ParentId == student.ParentId, student);
            UoW.Save();
        }
    }
}
