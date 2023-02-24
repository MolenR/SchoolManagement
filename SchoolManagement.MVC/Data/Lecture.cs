using System;
using System.Collections.Generic;

namespace SchoolManagement.MVC.Data;

public partial class Lecture : BaseData
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual ICollection<Classes> Classes { get; } = new List<Classes>();
}
