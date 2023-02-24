using System;
using System.Collections.Generic;

namespace SchoolManagement.MVC.Data;

public partial class Enrollment : BaseData
{
    public int? StudentId { get; set; }

    public int? ClassId { get; set; }

    public string? Grade { get; set; }

    public virtual Classes? Class { get; set; }

    public virtual Student? Student { get; set; }
}
