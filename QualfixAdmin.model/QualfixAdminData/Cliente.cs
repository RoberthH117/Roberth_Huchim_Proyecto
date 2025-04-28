using QualfixAdmin.model.EntitiSecurity;
using System;
using System.Collections.Generic;

namespace QualfixAdmin.model.QualfixAdminData;

public   class Cliente
{
    public int ClienteId { get; set; }

    public string? Direccion { get; set; }

    public string? NombreComercial { get; set; }

    public string? Cp { get; set; }

    public string? Telefonos { get; set; }

    public string? Fax { get; set; }

    public string? CorreoElectronico { get; set; }

    public int? InformacionFiscalId { get; set; }

    public int? CiudadId { get; set; }

    public int? EstadoId { get; set; }

    public int? PaisId { get; set; }

    public string? Calle { get; set; }

    public string? NumeroExterior { get; set; }

    public string? NumeroInterior { get; set; }

    public string? Cruzamientos { get; set; }

    public string? Colonia { get; set; }

    public virtual Ciudad? Ciudad { get; set; }

    public virtual Estado? Estado { get; set; }

    public virtual InformacionFiscals? InformacionFiscal { get; set; }

    public virtual Pai? Pais { get; set; }





}
