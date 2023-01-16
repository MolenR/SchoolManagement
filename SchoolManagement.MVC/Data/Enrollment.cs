using System;
using System.Collections.Generic;

namespace SchoolManagement.MVC.Data;

public partial class Enrollment
{
    public int Id { get; set; }

    public int? StudentsId { get; set; }

    public int? ClassId { get; set; }

    public string? Grade { get; set; }

    public virtual Class? Class { get; set; }

    public virtual Student? Students { get; set; }
}
