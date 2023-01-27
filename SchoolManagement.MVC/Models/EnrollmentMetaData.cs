using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.MVC.Data
    ;

public class EnrollmentMetaData
{
    [Display(Name = "Student")]
    public int? StudentId { get; set; }
    
    [Display(Name = "Class")]
    public int? ClassId { get; set; }

    public virtual Classes? Class { get; set; }

    public virtual Student? Student { get; set; }
}
[ModelMetadataType(typeof(EnrollmentMetaData))]
public partial class Enrollment { }
