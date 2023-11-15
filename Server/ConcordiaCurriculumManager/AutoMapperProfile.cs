using AutoMapper;
using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs;
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
        CreateMap<Dossier, DossierDetailsDTO>().ReverseMap();
        CreateMap<CourseCreationRequest, CourseCreationRequestDTO>().ReverseMap();
        CreateMap<CourseModificationRequest, CourseModificationRequestDTO>().ReverseMap();
        CreateMap<CourseDeletionRequest, CourseDeletionRequestDTO>().ReverseMap();
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        CreateMap<Group, GroupDTO>();
        CreateMap<SupportingFile, SupportingFileDTO>();
        CreateMap<Course, CourseDataDTO>()
            .ForMember(dest => dest.SupportingFiles, opt => opt.MapFrom<SupportingFilesResolver>())
            .ForMember(dest => dest.ComponentCodes, opt => opt.MapFrom<ComponentCodeResolver>())
            .ReverseMap();
    }

}
