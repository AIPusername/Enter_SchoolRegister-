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
                filterPredicate: c => c.TeacherId == teacherId, orderByPredicate: x => 
                x.OrderBy(c => c.Name), enableTracking: false);
            IEnumerable<CourseVm> coursesVm = Mapper.Map<IEnumerable<CourseVm>>(courses);
            return coursesVm;
        }

        public bool AddCourse(AddCourseVm addCourseVm)
        {
            var course = Mapper.Map<Course>(addCourseVm);
            var exists = UoW.Repository<Course>().Get(c => (c.Name.ToUpper().Equals(course.Name.ToUpper())
                                                                    && c.TeacherId == course.TeacherId ));
            if (exists == null)
            {
                UoW.Repository<Course>().Add(course);
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
            if(SomeoneIsAttending(course.Id))
            {
                IEnumerable<CourseStudent> attendances = UoW.Repository<CourseStudent>().GetRange(
                                                            filterPredicate: cs => cs.CourseId == course.Id, enableTracking: false);
                foreach(var cS in attendances)
                {
                    UoW.Repository<CourseStudent>().Delete(cS);
                }
                if(SomeoneHasGrade(course.Id))
                {
                    IEnumerable<Grade> grades = UoW.Repository<Grade>().GetRange(g => g.CourseId == course.Id);
                    foreach(var g in grades)
                    {
                        UoW.Repository<Grade>().Delete(g);
                    }
                }
            }
            UoW.Repository<Course>().Delete(course);
            UoW.Save();
        }


        public IEnumerable<CourseStudentVm> GetAttendances(int teacherId)
        {
            IEnumerable<Course> courses = UoW.Repository<Course>().GetRange(
                filterPredicate: c => c.TeacherId == teacherId, enableTracking: false);
            IEnumerable<Student> students = UoW.Repository<Student>().GetRange(enableTracking: false);
            IEnumerable<CourseStudent> courseStudents = UoW.Repository<CourseStudent>().GetRange( enableTracking: false);

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
                UoW.Repository<CourseStudent>().Add(courseStudent);
                UoW.Save();
                return true;
            }
            return false;
        }

        public void RemoveCourseStudent(AddRemoveCourseStudentVm model)
        {
            var courseStudent = Mapper.Map<CourseStudent>(model);
            courseStudent = UoW.Repository<CourseStudent>().Get(filterPredicate: cs => cs.CourseId == courseStudent.CourseId &&
                                                              cs.StudentSerialNumber == courseStudent.StudentSerialNumber);
            if(HasGrades(courseStudent.CourseId, courseStudent.StudentSerialNumber))
            {
                var grades = UoW.Repository<Grade>().GetRange(g => g.CourseId == courseStudent.CourseId && g.StudentSerialNumber == courseStudent.StudentSerialNumber);
                foreach(var g in grades)
                {
                    bool succeded = UoW.Repository<Grade>().Delete(g);
                }
            }
            UoW.Repository<CourseStudent>().Delete(courseStudent);
            UoW.Save();
        }

        public bool SomeoneIsAttending(int courseId)
        {
            IEnumerable<CourseStudent> attendances = UoW.Repository<CourseStudent>().GetRange(filterPredicate: cs => cs.CourseId == courseId, enableTracking: false);
            return attendances.Any();
        }

        public IEnumerable<GradeVm> GetListOfGrades(int teachedId)
        {
            IEnumerable<Grade> grades = UoW.Repository<Grade>().GetRange(enableTracking: false);
            IEnumerable<Course> courses = UoW.Repository<Course>().GetRange(
                filterPredicate: c => c.TeacherId == teachedId, enableTracking: false);
            IEnumerable<Student> students = UoW.Repository<Student>().GetRange(enableTracking: false);

            var partial =
                from g in grades
                join c in courses on g.CourseId equals c.Id
                select new
                {
                    Mark = g.Mark,
                    Date = g.Date,
                    Comment = g.Comment,
                    CourseId = c.Id,
                    CourseName = c.Name,
                    StudentSerialNumber = g.StudentSerialNumber
                };

            var join =
                from p in partial
                join s in students on p.StudentSerialNumber equals s.SerialNumber
                select new
                {
                    Mark = p.Mark,
                    Date = p.Date,
                    Comment = p.Comment,
                    CourseId = p.CourseId,
                    CourseName = p.CourseName,
                    StudentSerialNumber = s.SerialNumber,
                    StudentLast = s.LastName,
                    StudentFirst = s.FirstName
                };

            List<GradeVm> gradesVm = new List<GradeVm>();
            foreach (var raw in join)
            {
                GradeVm g = new GradeVm(raw.Mark, raw.Date, raw.Comment, raw.CourseId, raw.CourseName, raw.StudentSerialNumber, raw.StudentLast, raw.StudentFirst);
                gradesVm.Add(g);
            }
            return gradesVm.OrderBy(g => g.CourseName);
        }

        public IEnumerable<GradeVm> GetListOfGrades(int courseId, int studentSn)
        {
            IEnumerable<Grade> grades = UoW.Repository<Grade>().GetRange(filterPredicate: g => g.CourseId == courseId &&
                                                                g.StudentSerialNumber == studentSn, enableTracking: false);
            Course course = UoW.Repository<Course>().Get(filterPredicate: c => c.Id == courseId, enableTracking: false);
            Student student = UoW.Repository<Student>().Get(filterPredicate: s => s.SerialNumber == studentSn, enableTracking: false);

            List<GradeVm> list = new List<GradeVm>();
            foreach(var g in grades)
            {
                list.Add(new GradeVm(g.Mark, g.Date, g.Comment, course.Id, course.Name, student.SerialNumber, student.LastName, student.FirstName));
            }
            return list.OrderBy(g => g.Date);
        }

        public bool AddGrade(GradingVm model)
        {
            var grade = Mapper.Map<Grade>(model);
            var exists = UoW.Repository<Grade>().Get(g => g.CourseId == grade.CourseId &&
                                                          g.StudentSerialNumber == grade.StudentSerialNumber &&
                                                          g.Date.Equals(grade.Date));
            if (exists == null)
            {
                var student = UoW.Repository<Student>().Get(s => s.SerialNumber == grade.StudentSerialNumber);
                student.Grades.Add(grade);
                UoW.Repository<Student>().AddOrUpdate(s => s.SerialNumber == student.SerialNumber, student);
                UoW.Save();
                return true;
            }
            return false;
        }

        public void RemoveGrade(GradingVm model)
        {
            var grade = Mapper.Map<Grade>(model);
            grade = UoW.Repository<Grade>().Get(g => g.StudentSerialNumber == grade.StudentSerialNumber &&
                                                        g.CourseId == grade.CourseId && g.Date.Equals(grade.Date));
            UoW.Repository<Grade>().Delete(grade);
            UoW.Save();
        }

        public bool SomeoneHasGrade(int courseId)
        {
            IEnumerable<Grade> grades = UoW.Repository<Grade>().GetRange(g => g.CourseId == courseId);
            int count = grades.Count();
            return grades.Any();
        }

        public bool HasGrades(int courseId, int studentSn)
        {
            IEnumerable<Grade> grades = UoW.Repository<Grade>().GetRange(g => g.CourseId == courseId && g.StudentSerialNumber == studentSn);
            return grades.Any();
        }
    }
}
