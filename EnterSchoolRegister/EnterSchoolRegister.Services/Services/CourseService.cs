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
        public CourseService(IUnitOfWork uow, ILoggerFactory loggerFactory) : base(uow, loggerFactory)
        {
        }

        public IEnumerable<CourseVm> GetCourses()
        {
            IEnumerable<Course> courses = UoW.Repository<Course>().GetRange(filterPredicate: c => c.Active==true, 
                orderByPredicate: x => x.OrderByDescending(c => c.Name), enableTracking: false);
            IEnumerable<CourseVm> coursesVm = AutoMapper.Mapper.Map<IEnumerable<CourseVm>>(courses);
            return coursesVm.Reverse();
        }

        public IEnumerable<CourseVm> GetCourses(int teacherId)
        {
            IEnumerable<Course> courses = UoW.Repository<Course>().GetRange(
                filterPredicate: c => c.TeacherId == teacherId && c.Active == true, orderByPredicate: x => 
                x.OrderByDescending(c => c.Name), enableTracking: false);
            IEnumerable<CourseVm> coursesVm = AutoMapper.Mapper.Map<IEnumerable<CourseVm>>(courses);
            return coursesVm.Reverse();
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
            var course = Mapper.Map<Course>(removeCourseVm);
            course.Active = false;
            UoW.Repository<Course>().AddOrUpdate(c => (c.Name.ToUpper().Equals(course.Name.ToUpper())
                                                     && c.TeacherId == course.TeacherId), course);
            UoW.Save();
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
