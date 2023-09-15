using System;
using System.Collections.Generic;

namespace DddoTest.Models;

public partial class SucursalesDdo
{
    public int IdSucursal { get; set; }

    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public int? IdMoneda { get; set; }

    public virtual MonedaDdo? IdMonedaNavigation { get; set; }
}
