using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public int? Users { get; set; }

    public string? Alargie { get; set; }

    public string? BloodType { get; set; }

    public virtual Appontment? Appontment { get; set; }

    public virtual Emergency? Emergency { get; set; }

    public virtual PatentVacc? PatentVacc { get; set; }

    public virtual User PatientNavigation { get; set; } = null!;
}
