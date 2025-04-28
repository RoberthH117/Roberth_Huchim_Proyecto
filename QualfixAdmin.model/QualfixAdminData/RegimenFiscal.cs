using System;
using System.Collections.Generic;

namespace QualfixAdmin.model.QualfixAdminData;

public   class RegimenFiscal
{
    public int Id { get; set; }

    public int? CodeRegimenFiscal { get; set; }

    public string? Descripcion { get; set; }

    public bool? Fisica { get; set; }

    public bool? Moral { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<InformacionFiscals> InformacionFiscals { get; set; } = new List<InformacionFiscals>();
}
