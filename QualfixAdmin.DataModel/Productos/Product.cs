using System;
using System.Collections.Generic;

namespace QualfixAdmin.DataModel.Productos;

public partial class Product
{
    public string Id { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;
}
