using System;
using System.Collections.Generic;

namespace SchoolManagement.MVC.Data;

public partial class Course : BaseData
{
    public string Name { get; set; } = null!;

    public string? Code { get; set; }

    public int? Credits { get; set; }

    public virtual ICollection<Classes> Classes { get; } = new List<Classes>();
}
