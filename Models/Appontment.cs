using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class Appontment
{
    public int AppontmentId { get; set; }

    public int? Ciliid { get; set; }

    public int? DocId { get; set; }

    public int? PatId { get; set; }

    public int? AvailabiltyDateId { get; set; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    public string? Medesine { get; set; }

    public string? MidCase { get; set; }

    public virtual Clinic Appontment1 { get; set; } = null!;

    public virtual Doctor Appontment2 { get; set; } = null!;

    public virtual Patient Appontment3 { get; set; } = null!;

    public virtual AvailabiltyDate AppontmentNavigation { get; set; } = null!;
}
