using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class Manegeer
{
    public int ManegeerId { get; set; }

    public int? Ueserss { get; set; }

    public int? CliniccId { get; set; }

    public virtual User Manegeer1 { get; set; } = null!;

    public virtual Clinic ManegeerNavigation { get; set; } = null!;
}
