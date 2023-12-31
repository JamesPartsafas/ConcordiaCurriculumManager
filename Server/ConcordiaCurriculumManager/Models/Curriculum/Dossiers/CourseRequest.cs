﻿using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;
using ConcordiaCurriculumManager.Filters.Exceptions;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

public abstract class CourseRequest : BaseModel
{
    public required Guid DossierId { get; set; }

    public Dossier? Dossier { get; set; }

    public required string Rationale { get; set; }

    public required string ResourceImplication { get; set; }

    public string? Comment { get; set; }

    public string? Conflict { get; set; }


    public void EditRequestData (CourseInitiationDTO dto)
    {
        Rationale = dto.Rationale;
        ResourceImplication = dto.ResourceImplication;
        Comment = dto.Comment;
    }
}
