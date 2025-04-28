using System;
using System.Collections.Generic;

namespace QualfixAdmin.model.QualfixAdminData;

public   class Estado
{
    public int EstadoId { get; set; }

    public string? Nombre { get; set; }

    public int PaisId { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<InformacionFiscals> InformacionFiscals { get; set; } = new List<InformacionFiscals>();
}
