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
    public class CourseService : BaseService, ICourseService
    {
        public CourseService(IUnitOfWork uow, ILoggerFactory loggerFactory) : base(uow, loggerFactory)
        {
        }

        public void AddCourse(AddCourseVm addCourseVm)
        {
            var course = Mapper.Map<Course>(addCourseVm);
            UoW.Repository<Course>().Add(course);
            UoW.Save();
        }

        public void RemoveCourse(RemoveCourseVm removeCourseVm)
        {
            var course = Mapper.Map<Course>(removeCourseVm);
            UoW.Repository<Course>().Delete(course);
            UoW.Save();
        }
    }
}
