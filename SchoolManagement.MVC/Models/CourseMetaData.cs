using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.MVC.Data;

public class CourseMetaData
{
    public string Name { get; set; } = null!;

    public string? Code { get; set; }

    public int? Credits { get; set; }
}

[ModelMetadataType(typeof(CourseMetaData))]
public partial class Course { }

