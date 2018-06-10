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
            IEnumerable<StudentVm> studentsVm = Mapper.Map<IEnumerable<StudentVm>>(students);
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

        public IEnumerable<StudentVm> GetStudentsByGrade(int courseId)
        {
            IEnumerable<StudentVm> partial = GetStudentsByCourse(courseId);

            List<StudentVm> students = new List<StudentVm>();
            foreach(var student in partial)
            {
                if(HasAnyGrade(courseId, student.SerialNumber))
                {
                    students.Add(student);
                }
            }

            return students.OrderBy(s => s.LastName);
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
                    if(HasAnyGrade(cS.CourseId, student.SerialNumber))
                    {
                        IEnumerable<Grade> grades = UoW.Repository<Grade>().GetRange(filterPredicate: gr => gr.CourseId == cS.CourseId &&
                                                                gr.StudentSerialNumber == student.SerialNumber && gr.Active);
                        int count = grades.Count();
                        foreach (var g in grades)
                        {
                            g.Active = false;
                            UoW.Repository<Grade>().AddOrUpdate(gr => gr.Id == g.Id, g);
                        }
                    }
                }
            }
            UoW.Repository<Student>().AddOrUpdate(s => s.SerialNumber == student.SerialNumber, student);
            UoW.Save();
        }


        public bool IsAttendingSomething(int studentSn)
        {
            IEnumerable<CourseStudent> attendances = UoW.Repository<CourseStudent>().GetRange(filterPredicate: cs =>
                                    cs.StudentSerialNumber == studentSn && cs.Active, enableTracking: false);
            return attendances.Any();
        }

        public bool HasAnyGrade(int courseId, int studentSn)
        {
            IEnumerable<Grade> grades = UoW.Repository<Grade>().GetRange(filterPredicate: g =>
                                    g.CourseId == courseId && g.StudentSerialNumber == studentSn &&
                                    g.Active, enableTracking: false);
            int count = grades.Count();
            return count != 0;
        }

        public IEnumerable<GradeVm> Status(int parentId)
        {
            IEnumerable<Student> students = UoW.Repository<Student>().GetRange(
                filterPredicate: s => s.ParentId == parentId && s.Active, enableTracking: false);
            IEnumerable<Course> courses = UoW.Repository<Course>().GetRange(filterPredicate: c => c.Active, enableTracking: false);
            IEnumerable<Grade> grades = UoW.Repository<Grade>().GetRange(filterPredicate: g => g.Active, enableTracking: false);

            var partial =
                from gr in grades
                join st in students on gr.StudentSerialNumber equals st.SerialNumber
                select new
                {
                    Mark = gr.Mark,
                    Date = gr.Date,
                    Comment = gr.Comment,
                    CourseId = gr.CourseId,
                    StudentSerialNumber = st.SerialNumber,
                    StudentLast = st.LastName,
                    StudentFirst = st.FirstName
                };

            var join =
                from p in partial
                join c in courses on p.CourseId equals c.Id
                select new
                {
                    Mark = p.Mark,
                    Date = p.Date,
                    Comment = p.Comment,
                    CourseId = c.Id,
                    CourseName = c.Name,
                    StudentSerialNumber = p.StudentSerialNumber,
                    StudentLast = p.StudentLast,
                    StudentFirst = p.StudentFirst
                };

            List<GradeVm> list = new List<GradeVm>();
            foreach(var raw in join)
            {
                list.Add(new GradeVm(raw.Mark, raw.Date, raw.Comment, raw.CourseId, raw.CourseName, raw.StudentSerialNumber, raw.StudentLast, raw.StudentFirst));
            }
            return list.OrderBy(g => g.StudentLast);
        }

        public IEnumerable<CourseVm> Courses()
        {
            IEnumerable<Course> partial = UoW.Repository<Course>().GetRange(filterPredicate: c => c.Active, enableTracking: false);
            IEnumerable<CourseVm> courses = Mapper.Map<IEnumerable<CourseVm>>(partial);
            return courses.OrderBy(c => c.Name);
        }
    }
}
