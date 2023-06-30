using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class Vaccsine
{
    public int VaccsineId { get; set; }

    public string? VName { get; set; }

    public string? Manufacture { get; set; }

    public string? ApprovedBy { get; set; }

    public string? RecomendAge { get; set; }

    public string? DoseNumber { get; set; }

    public string? Symptoms { get; set; }

    public virtual PatentVacc? PatentVacc { get; set; }
}
