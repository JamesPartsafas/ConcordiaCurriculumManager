using AutoMapper;
using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<User, RegisterDTO>().ReverseMap();
        CreateMap<CourseComponent, CourseComponentDTO>().ReverseMap();
    }

}
