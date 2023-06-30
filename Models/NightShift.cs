using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class NightShift
{
    public int NightShiftId { get; set; }

    public int? DocIdd { get; set; }

    public int? ClinIdd { get; set; }

    public DateTime? Date { get; set; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    public virtual Doctor NightShift1 { get; set; } = null!;

    public virtual Clinic NightShiftNavigation { get; set; } = null!;
}
