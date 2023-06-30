using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class Clinic
{
    public int ClinicId { get; set; }

    public string? ClinicName { get; set; }

    public string? ClinicAddress { get; set; }

    public string? ClinicPhone { get; set; }

    public string? ClinicLocation { get; set; }

    public virtual Appontment? Appontment { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Manegeer? Manegeer { get; set; }

    public virtual NightShift? NightShift { get; set; }

    public virtual Reseption? Reseption { get; set; }
}
