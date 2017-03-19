using System;
using System.ComponentModel.DataAnnotations.Schema;

public class RegistrationItem
{
    public int ID { get; set; }

    [ForeignKey("RegistrationHeader")]
    public int RegistrationHeaderID { get; set; }
    public virtual RegistrationHeader RegistrationHeader { get; set; }

    public string Salutation { get; set; }
    public string FullName { get; set; }
    public string idType { get; set; }
    public string idNo { get; set; }
    public string email { get; set; }
    public string telMobile { get; set; }

    [ForeignKey("Class")]
    public int ClassId { get; set; }
    public virtual Class Class { get; set; }

    [ForeignKey("Student")]
    public int StudentId { get; set; }
    public virtual Student Student { get; set; }

    [ForeignKey("Company")]
    public int EmployerId { get; set; }
    public virtual Company Company { get; set; }

    public string EmployerName { get; set; }
    public decimal CourseFee { get; set; }

}
