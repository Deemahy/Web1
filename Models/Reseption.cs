using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class Reseption
{
    public int ReseptionId { get; set; }

    public int? Userssid { get; set; }

    public int? Clinicssid { get; set; }

    public virtual User Reseption1 { get; set; } = null!;

    public virtual Clinic ReseptionNavigation { get; set; } = null!;
}
