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
using System.Linq.Expressions;
using System.Text;

namespace EnterSchoolRegister.Services.Services
{
    public class CourseService : BaseService, ICourseService
    {
        private readonly IManageStudentService _studentService;

        public CourseService(IUnitOfWork uow, ILoggerFactory loggerFactory, IManageStudentService mss) : base(uow, loggerFactory)
        {
            _studentService = mss;
        }

        public IEnumerable<CourseVm> GetCourses(int teacherId)
        {
            IEnumerable<Course> courses = UoW.Repository<Course>().GetRange(
                filterPredicate: c => c.TeacherId == teacherId && c.Active, orderByPredicate: x => 
                x.OrderBy(c => c.Name), enableTracking: false);
            IEnumerable<CourseVm> coursesVm = AutoMapper.Mapper.Map<IEnumerable<CourseVm>>(courses);
            return coursesVm;
        }

        public bool AddCourse(AddCourseVm addCourseVm)
        {
            var course = Mapper.Map<Course>(addCourseVm);
            var exists = UoW.Repository<Course>().Get(c => (c.Name.ToUpper().Equals(course.Name.ToUpper())
                                                                    && c.TeacherId == course.TeacherId ));
            if (exists == null)
            {
                course.Active = true;
                UoW.Repository<Course>().Add(course);
                UoW.Save();
                return true;
            } else if ( !exists.Active )
            {
                course.Id = exists.Id;
                course.Active = true;
                UoW.Repository<Course>().AddOrUpdate(c => c.Id == course.Id, course);
                UoW.Save();
                return true;
            }
            return false;
        }

        public void RemoveCourse(RemoveCourseVm removeCourseVm)
        {
            var partial = Mapper.Map<Course>(removeCourseVm);
            var course = UoW.Repository<Course>().Get(c => (c.Name.ToUpper().Equals(partial.Name.ToUpper())
                                                                    && c.TeacherId == partial.TeacherId));
            course.Active = false;
            if(SomeoneIsAttending(course.Id))
            {
                IEnumerable<CourseStudent> attendances = UoW.Repository<CourseStudent>().GetRange(
                                                            filterPredicate: cs => cs.CourseId == course.Id && cs.Active, enableTracking: false);
                
                foreach(var cS in attendances)
                {
                    cS.Active = false;
                    UoW.Repository<CourseStudent>().AddOrUpdate(cs => cs.Id == cS.Id, cS);
                }
            }
            UoW.Repository<Course>().AddOrUpdate(c => (c.Name.ToUpper().Equals(course.Name.ToUpper())
                                                     && c.TeacherId == course.TeacherId), course);
            UoW.Save();
        }


        public IEnumerable<CourseStudentVm> GetAttendances(int teacherId)
        {
            IEnumerable<Course> courses = UoW.Repository<Course>().GetRange(
                filterPredicate: c => c.TeacherId == teacherId && c.Active, enableTracking: false);
            IEnumerable<Student> students = UoW.Repository<Student>().GetRange(filterPredicate: s => s.Active, enableTracking: false);
            IEnumerable<CourseStudent> courseStudents = UoW.Repository<CourseStudent>().GetRange(filterPredicate: cs => cs.Active, enableTracking: false);

            var partial =
                from cs in courseStudents
                join c in courses on cs.CourseId equals c.Id
                select new
                {
                    cId = c.Id,
                    cn = c.Name,
                    ssn = cs.StudentSerialNumber
                };

            var join =
                from p in partial
                join s in students on p.ssn equals s.SerialNumber
                orderby p.cn
                select new
                {
                    CourseId = p.cId,
                    CourseName = p.cn,
                    StudentSerialNumber = p.ssn,
                    StudentLast = s.LastName,
                    StudentFirst = s.FirstName
                };

            List<CourseStudentVm> courseStudentsVm = new List<CourseStudentVm>();
            foreach(var raw in join)
            {
                CourseStudentVm cs = new CourseStudentVm(raw.CourseId, raw.CourseName, raw.StudentSerialNumber, raw.StudentLast, raw.StudentFirst);
                courseStudentsVm.Add(cs);
            }
            return courseStudentsVm.OrderBy(cs => cs.CourseName);
        }

        public bool AddCourseStudent(AddRemoveCourseStudentVm model)
        {
            var courseStudent = Mapper.Map<CourseStudent>(model);
            var exists = UoW.Repository<CourseStudent>().Get(cs => cs.CourseId == courseStudent.CourseId &&
                                                                   cs.StudentSerialNumber == courseStudent.StudentSerialNumber);
            if(exists == null)
            {
                courseStudent.Active = true;
                UoW.Repository<CourseStudent>().Add(courseStudent);
                UoW.Save();
                return true;
            }else if(!exists.Active)
            {
                courseStudent.Id = exists.Id;
                courseStudent.Active = true;
                UoW.Repository<CourseStudent>().AddOrUpdate(cs => cs.Id == courseStudent.Id, courseStudent);
                UoW.Save();
                return true;
            }
            return false;
        }

        public void RemoveCourseStudent(AddRemoveCourseStudentVm model)
        {
            var courseStudent = Mapper.Map<CourseStudent>(model);
            courseStudent.Active = false;
            UoW.Repository<CourseStudent>().AddOrUpdate(cs => cs.CourseId == courseStudent.CourseId &&
                                                              cs.StudentSerialNumber == courseStudent.StudentSerialNumber, courseStudent);
            UoW.Save();
        }

        public bool SomeoneIsAttending(int courseId)
        {
            var attendances = UoW.Repository<CourseStudent>().GetRange(filterPredicate: cs => cs.CourseId == courseId && cs.Active, enableTracking: false);
            return attendances.Count<CourseStudent>() != 0;
        }

        public IEnumerable<GradeVm> GetListOfGrades(int courseId, int studentId)
        {
            throw new NotImplementedException();
        }

        public void AddGrade(GradingVm addGradeVm)
        {
            throw new NotImplementedException();
        }

        public void RemoveGrade(RemoveGradeVm removeGradeVm)
        {
            throw new NotImplementedException();
        }
    }
}
