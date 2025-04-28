using System;
using System.Collections.Generic;

namespace QualfixAdmin.model.QualfixAdminData;

public   class ConfiguracionCfdi
{
    public int Id { get; set; }

    public string? NameFileCer { get; set; }

    public string? FileCer { get; set; }

    public string? FileKey { get; set; }

    public string? FilePasswordKey { get; set; }

    public string? NameFileKey { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public string? Cuenta { get; set; }

    public string? PasswordUsuario { get; set; }

    public string? Usuario { get; set; }
}
