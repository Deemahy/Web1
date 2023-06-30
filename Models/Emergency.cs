using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class Emergency
{
    public int EmergencyId { get; set; }

    public int? PId { get; set; }

    public int? DId { get; set; }

    public string? PatientMassages { get; set; }

    public string? DoctorResponse { get; set; }

    public virtual Patient Emergency1 { get; set; } = null!;

    public virtual Doctor EmergencyNavigation { get; set; } = null!;
}
