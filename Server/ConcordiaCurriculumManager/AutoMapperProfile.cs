using AutoMapper;
using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.DTO.CourseGrouping;
using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.CourseGroupingRequests;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs;
using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
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
        CreateMap<CourseCreationRequest, CourseCreationRequestCourseDetailsDTO>().ReverseMap();
        CreateMap<CourseModificationRequest, CourseModificationRequestCourseDetailsDTO>().ReverseMap();
        CreateMap<ApprovalStage, ApprovalStageDTO>();
        CreateMap<ApprovalHistory, ApprovalHistoryDTO>();
        CreateMap<DossierDiscussion, DossierDiscussionDTO>().ReverseMap();
        CreateMap<CreateDossierDiscussionMessageDTO, DiscussionMessage>();
        CreateMap<DiscussionMessage, DossierDiscussionMessageDTO>();
        CreateMap<CourseDeletionRequest, CourseDeletionRequestCourseDetailsDTO>().ReverseMap();
        CreateMap<DossierReport, DossierReportDTO>()
            .ForMember(dest => dest.InitiatorId, opt => opt.MapFrom(d => d.Dossier.InitiatorId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(d => d.Dossier.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(d => d.Dossier.Description))
            .ForMember(dest => dest.State, opt => opt.MapFrom(d => d.Dossier.State))
            .ForMember(dest => dest.CourseCreationRequests, opt => opt.MapFrom(d => d.Dossier.CourseCreationRequests))
            .ForMember(dest => dest.CourseModificationRequests, opt => opt.MapFrom(d => d.Dossier.CourseModificationRequests))
            .ForMember(dest => dest.CourseDeletionRequests, opt => opt.MapFrom(d => d.Dossier.CourseDeletionRequests))
            .ForMember(dest => dest.ApprovalStages, opt => opt.MapFrom(d => d.Dossier.ApprovalStages));
        CreateMap<CourseGroupingReference, CourseGroupingReferenceDTO>();
        CreateMap<CourseGrouping, CourseGroupingDTO>();
        CreateMap<CourseIdentifier, CourseIdentifierDTO>();
        CreateMap<CourseGroupingRequest, CourseGroupingRequestDTO>();
        CreateMap<CourseChanges, CourseChangesDTO>();
    }
}
