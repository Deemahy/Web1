using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateBearth { get; set; }

    public string? Imagpath { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual LogIn? LogIn { get; set; }

    public virtual Manegeer? Manegeer { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual Reseption? Reseption { get; set; }
}
