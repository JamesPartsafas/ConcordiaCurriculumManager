using NpgsqlTypes;

namespace ConcordiaCurriculumManager.Models.Curriculum;

public class CourseComponent : BaseModel
{
    public required ComponentCodeEnum ComponentCode { get; set; }

    public required string ComponentName { get; set; }

    public ICollection<CourseCourseComponent>? CourseCourseComponents { get; set; }
}

public enum ComponentCodeEnum
{
    [PgName(nameof(CON))]
    CON,

    [PgName(nameof(FLD))]
    FLD,

    [PgName(nameof(FWK))]
    FWK,

    [PgName(nameof(IND))]
    IND,

    [PgName(nameof(LAB))]
    LAB,

    [PgName(nameof(LEC))]
    LEC,

    [PgName(nameof(MOD))]
    MOD,

    [PgName(nameof(ONL))]
    ONL,

    [PgName(nameof(PRA))]
    PRA,

    [PgName(nameof(PST))]
    PST,

    [PgName(nameof(REA))]
    REA,

    [PgName(nameof(REG))]
    REG,

    [PgName(nameof(RSC))]
    RSC,

    [PgName(nameof(SEM))]
    SEM,

    [PgName(nameof(STU))]
    STU,

    [PgName(nameof(THE))]
    THE,

    [PgName(nameof(TUT))]
    TUT,

    [PgName(nameof(TL))]
    TL,

    [PgName(nameof(WKS))]
    WKS
}

public static class ComponentCodeMapping
{
    public static IReadOnlyDictionary<ComponentCodeEnum, string> GetComponentCodeMapping()
    {
        return new Dictionary<ComponentCodeEnum, string>
        {
            { ComponentCodeEnum.CON, "Conference" },
            { ComponentCodeEnum.FLD, "Field Studies" },
            { ComponentCodeEnum.FWK, "Fieldwork" },
            { ComponentCodeEnum.IND, "Independent Study" },
            { ComponentCodeEnum.LAB, "Laboratory" },
            { ComponentCodeEnum.LEC, "Lecture" },
            { ComponentCodeEnum.MOD, "Modular" },
            { ComponentCodeEnum.ONL, "Online" },
            { ComponentCodeEnum.PRA, "Practicum/Internship/Work-Term" },
            { ComponentCodeEnum.PST, "Private Studies" },
            { ComponentCodeEnum.REA, "Reading" },
            { ComponentCodeEnum.REG, "Regular" },
            { ComponentCodeEnum.RSC, "Research" },
            { ComponentCodeEnum.SEM, "Seminar" },
            { ComponentCodeEnum.STU, "Studio" },
            { ComponentCodeEnum.THE, "Thesis Research" },
            { ComponentCodeEnum.TUT, "Tutorial" },
            { ComponentCodeEnum.TL, "Tutorial/Lab" },
            { ComponentCodeEnum.WKS, "Workshop" }
        };
    }

    public static string GetComponentCodeMapping(ComponentCodeEnum code)
    {
        return GetComponentCodeMapping()[code];
    }

    public static IEnumerable<CourseComponent> GetComponentCodeMapping(IEnumerable<ComponentCodeEnum> codes)
    {
        var subset = new List<CourseComponent>();
        var mappings = GetComponentCodeMapping();

        foreach (var code in codes)
        {
            subset.Add(new CourseComponent { ComponentCode = code, ComponentName = mappings[code] });
        }

        return subset;
    }
}
