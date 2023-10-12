using AutoMapper;
using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<User, RegisterDTO>().ReverseMap();
        CreateMap<CourseComponent, CourseComponentDTO>().ReverseMap();
        CreateMap<Course, CourseCreationInitiationDTO>().ReverseMap();
        CreateMap<Dossier, DossierDTO>().ReverseMap();
        CreateMap<CourseCreationRequest, CourseCreationRequestDTO>().ReverseMap();
        CreateMap<CourseModificationRequest, CourseModificationRequestDTO>().ReverseMap();
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        CreateMap<Group, GroupDTO>();
    }

}
