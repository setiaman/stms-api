using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Class
    {
    public int ID { get; set; }
    public string Title { get; set; }
    public string ClassCode { get; set; }
    public decimal CourseFee { get; set; }
    public DateTime StartedOn { get; set; }
    public DateTime EndedOn { get; set; }

    // New code
    [ForeignKey("Course")]
    public int CourseId  { get; set; }
    public virtual Course Course { get; set; }
}