using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class PatentVacc
{
    public int PatientVacId { get; set; }

    public int? PatientId { get; set; }

    public int? VaccId { get; set; }

    public DateTime? VacDay { get; set; }

    public string? Width { get; set; }

    public string? Headcircumference { get; set; }

    public string? Hight { get; set; }

    public virtual Patient PatientVac { get; set; } = null!;

    public virtual Vaccsine PatientVacNavigation { get; set; } = null!;
}
