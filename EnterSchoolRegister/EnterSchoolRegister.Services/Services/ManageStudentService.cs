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

        public IEnumerable<StudentVm> GetStudentsByParent(int parentId)
        {
            IEnumerable<Student> students = UoW.Repository<Student>().GetRange(
                filterPredicate: s => s.ParentId == parentId && s.Active, orderByPredicate: x =>
                x.OrderBy(s => s.LastName), enableTracking: false);
            IEnumerable<StudentVm> studentsVm = AutoMapper.Mapper.Map<IEnumerable<StudentVm>>(students);
            return studentsVm;
        }

        public IEnumerable<StudentVm> GetStudentsByCourse(int courseId)
        {
            IEnumerable<Student> students = UoW.Repository<Student>().GetRange(filterPredicate: s => s.Active, enableTracking: false);
            IEnumerable<CourseStudent> attendances = UoW.Repository<CourseStudent>().GetRange(filterPredicate: cs =>
                                                        cs.CourseId == courseId && cs.Active, enableTracking: false);
            var join =
                from cs in attendances
                join s in students on cs.StudentSerialNumber equals s.SerialNumber
                select new
                {
                    SerialNumber = s.SerialNumber,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    ParentId = s.ParentId
                };
            
            List<StudentVm> studentsVm = new List<StudentVm>();
            foreach (var raw in join)
            {
                StudentVm s = new StudentVm(raw.SerialNumber, raw.FirstName, raw.LastName, raw.ParentId);
                studentsVm.Add(s);
            }
            return studentsVm.OrderBy(s => s.LastName);
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
            var partial = Mapper.Map<Student>(model);
            var student = UoW.Repository<Student>().Get(s => s.LastName.ToUpper().Equals(partial.LastName.ToUpper()) &&
                                                            s.FirstName.ToUpper().Equals(partial.FirstName.ToUpper()) &&
                                                            s.ParentId == partial.ParentId);
            student.Active = false;
            if(IsAttendingSomething(student.SerialNumber))
            {
                IEnumerable<CourseStudent> attendances = UoW.Repository<CourseStudent>().GetRange(filterPredicate: cs =>
                                                            cs.StudentSerialNumber == student.SerialNumber && cs.Active, enableTracking: false);

                foreach (var cS in attendances)
                {
                    cS.Active = false;
                    UoW.Repository<CourseStudent>().AddOrUpdate(cs => cs.Id == cS.Id, cS);
                }
            }
            UoW.Repository<Student>().AddOrUpdate(s => s.LastName.ToUpper().Equals(student.LastName.ToUpper()) &&
                                                            s.FirstName.ToUpper().Equals(student.FirstName.ToUpper()) &&
                                                            s.ParentId == student.ParentId, student);
            UoW.Save();
        }


        public bool IsAttendingSomething(int studentSn)
        {
            var attendances = UoW.Repository<CourseStudent>().GetRange(filterPredicate: cs =>
                                    cs.StudentSerialNumber == studentSn && cs.Active, enableTracking: false);
            return attendances.Count<CourseStudent>() != 0;
        }
    }
}
