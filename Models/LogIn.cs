using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class LogIn
{
    public int LogInId { get; set; }

    public int? UersId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public virtual User LogIn1 { get; set; } = null!;

    public virtual Role LogInNavigation { get; set; } = null!;
}
