using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.MVC.Data;

public class LectureMetaData
{
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;
}

[ModelMetadataType(typeof(LectureMetaData))]
public partial class Lecture { }
