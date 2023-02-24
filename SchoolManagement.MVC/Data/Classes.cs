namespace SchoolManagement.MVC.Data;

public partial class Classes : BaseData
{
    public int? LectureId { get; set; }

    public int? CourseId { get; set; }

    public TimeSpan? Time { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; } = new List<Enrollment>();

    public virtual Lecture? Lecture { get; set; }
}
