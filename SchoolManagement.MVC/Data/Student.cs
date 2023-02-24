using System;
using System.Collections.Generic;

namespace SchoolManagement.MVC.Data;

public partial class Student : BaseData
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; } = new List<Enrollment>();
}
