using System;
using System.Collections.Generic;

namespace DddoTest.Models;

public partial class MonedaDdo
{
    public int IdMoneda { get; set; }

    public string Simbolo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<SucursalesDdo> SucursalesDdos { get; set; } = new List<SucursalesDdo>();
}
