using System;
using System.Collections.Generic;

public class RegistrationHeader
  {
    public int ID { get; set; }
    public string BookingNumber { get; set; }
    public DateTime BookedOn { get; set; }
    public string Sponsorship { get; set; }

    public int SponsorCompanyId { get; set; }

    
    //public virtual Company Company { get; set; }

    public string SponsorCompanyName { get; set; }
    public string SponsorContactPerson { get; set; }
    public string SponsorContactPhone { get; set; }
    public string SponsorContactEmail { get; set; }
    public string SponsorAddress { get; set; }

    //public ICollection<RegistrationItem> RegistrationItems { get; set; }
    
}
