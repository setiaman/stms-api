using System.Collections.Generic;

public class Course
{
    public int ID { get; set; }
    public string Title { get; set; }
    public decimal CourseFee { get; set; }
    public string CourseCode { get; set; }

    public ICollection<Class> Classes { get; set; }
}
