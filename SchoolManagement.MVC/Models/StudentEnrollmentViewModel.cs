namespace SchoolManagement.MVC.Models;

public class StudentEnrollmentViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!; 
    public string LastName { get; set; } = null!; 
    public bool IsEnrolled { get; set; }
}