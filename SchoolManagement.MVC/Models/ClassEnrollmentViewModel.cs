using SchoolManagement.MVC.Data;

namespace SchoolManagement.MVC.Models;

public class ClassEnrollmentViewModel
{
    public ClassViewModel? Classes { get; set; }
    public List<StudentEnrollmentViewModel> StudentEnrollment { get; set; } = new List<StudentEnrollmentViewModel>();
}
