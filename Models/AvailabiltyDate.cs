using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class AvailabiltyDate
{
    public int AvailabiltyDateId { get; set; }

    public int? Doctorid { get; set; }

    public DateTime? Date { get; set; }

    public virtual Appontment? Appontment { get; set; }

    public virtual Doctor AvailabiltyDateNavigation { get; set; } = null!;
}
