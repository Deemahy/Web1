using System;
using System.Collections.Generic;

namespace Web1.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual LogIn? LogIn { get; set; }
}
