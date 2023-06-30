using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public int? UserId { get; set; }

    public string? Speclized { get; set; }

    public string? OtherInfo { get; set; }

    public int? ClinicId { get; set; }

    public virtual Appontment? Appontment { get; set; }

    public virtual AvailabiltyDate? AvailabiltyDate { get; set; }

    public virtual User Doctor1 { get; set; } = null!;

    public virtual Clinic DoctorNavigation { get; set; } = null!;

    public virtual Emergency? Emergency { get; set; }

    public virtual NightShift? NightShift { get; set; }
}
