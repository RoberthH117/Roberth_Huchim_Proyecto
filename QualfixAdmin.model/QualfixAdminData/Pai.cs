using System;
using System.Collections.Generic;

namespace QualfixAdmin.model.QualfixAdminData;

public   class Pai
{
    public int PaisId { get; set; }

    public string? Nombre { get; set; }

    public string? TipoMoneda { get; set; }

    public string? SimboloMoneda { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<InformacionFiscals> InformacionFiscals { get; set; } = new List<InformacionFiscals>();
}
