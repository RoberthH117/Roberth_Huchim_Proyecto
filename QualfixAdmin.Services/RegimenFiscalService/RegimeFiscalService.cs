using QualfixAdmin.dal.DBContext;
using QualfixAdmin.model.QualfixAdminData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.Services.RegimenFiscalService
{
    public class RegimeFiscalService
    {
        private readonly QualfixAdminContext _dbContext;

        public RegimeFiscalService(QualfixAdminContext qualfixAdminContext)
        {
            _dbContext = qualfixAdminContext;
        }

        public List<RegimenFiscal> ObtenerTodasInformacionFiscals()
        {
            return _dbContext.RegimenFiscals.ToList();
        }
    }
}
