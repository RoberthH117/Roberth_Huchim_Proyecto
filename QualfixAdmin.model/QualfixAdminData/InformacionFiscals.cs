using System;
using System.Collections.Generic;

namespace QualfixAdmin.model.QualfixAdminData;

public   class InformacionFiscals
{
    public int InformacionFiscalId { get; set; }

    public string? RazonSocial { get; set; }

    public string? Rfc { get; set; }

    public string? Direccion { get; set; }

    public string? Cp { get; set; }

    public int? CiudadId { get; set; }

    public int? EstadoId { get; set; }

    public int? PaisId { get; set; }

    public string? NumeroInterior { get; set; }

    public string? NumeroExterior { get; set; }

    public string? Colonia { get; set; }

    public string? Localidad { get; set; }

    public string? Referencia { get; set; }

    public string? Municipio { get; set; }

    public int? RegFiscalId { get; set; }

    public virtual Ciudad? Ciudad { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual Estado? Estado { get; set; }

    public virtual Pai? Pais { get; set; }

    public virtual RegimenFiscal? RegFiscal { get; set; }
}
