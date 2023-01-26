using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.MVC.Data;

public class ClassesMetaData
{
    [Display(Name = "Lecturer")]
    public int LectureId { get; set; }
    
    [Display(Name = "Course")]
    public int CourseId { get; set; }
}

[ModelMetadataType(typeof(ClassesMetaData))]
public partial class Classes { }

