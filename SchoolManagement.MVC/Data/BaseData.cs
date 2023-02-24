namespace SchoolManagement.MVC.Data;

public abstract class BaseData
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}