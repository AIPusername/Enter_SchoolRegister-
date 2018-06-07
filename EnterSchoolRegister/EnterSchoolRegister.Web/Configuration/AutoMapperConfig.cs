using AutoMapper;
using EnterSchoolRegister.BLL.Entities;
using EnterSchoolRegister.ViewModels;
using EnterSchoolRegister.ViewModels.EntitiesViewModels;
using EnterSchoolRegister.ViewModels.ServicesViewModels;
using Microsoft.AspNetCore.Identity;

namespace EnterSchoolRegister.Web.Configuration
{
    public static class AutoMapperConfig
    {
        public static IMapperConfigurationExpression Mapping(this IMapperConfigurationExpression configurationExpression, UserManager<User> userManager)
        {
            Mapper.Initialize(cfg =>
            {
                // Maps
                cfg.CreateMap<User, BaseUserVm>()
                    .ForMember(dest => dest.Roles, member => member.MapFrom(src =>
                         userManager.GetRolesAsync(src).Result
                    ));

                // Basic
                cfg.CreateMap<Course, CourseVm>();
                cfg.CreateMap<Student, StudentVm>();
                cfg.CreateMap<Grade, GradeVm>();

                //Services maps
                cfg.CreateMap<AddCourseVm, Course>();
                cfg.CreateMap<RemoveCourseVm, Course>();

                cfg.CreateMap<AddRemoveStudentVm, Student>();

                cfg.CreateMap<AddRemoveCourseStudentVm, CourseStudent>();

                cfg.CreateMap<GradingVm, Grade>();
                cfg.CreateMap<RemoveGradeVm, Grade>();
            });
            return configurationExpression;
        }
    }
}
