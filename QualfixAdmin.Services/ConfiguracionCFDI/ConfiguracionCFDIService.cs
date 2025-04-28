using QualfixAdmin.dal.DBContext;
using QualfixAdmin.model.QualfixAdminData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.Services.ConfiguracionCFDI
{
    public class ConfiguracionCFDIService
    {
        private readonly QualfixAdminContext _dbContext;

        public ConfiguracionCFDIService(QualfixAdminContext qualfixAdminContext)
        {
            _dbContext = qualfixAdminContext;
        }

        public List<ConfiguracionCfdi> ObtenerTodasConfiguracionesCFDI()
        {
            return _dbContext.ConfiguracionCfdis.ToList();
        }
        public ConfiguracionCfdi ObtenerConfiguracionCFDI(int id)
        {
            using (var newContext = new QualfixAdminContext())
            {
                return _dbContext.ConfiguracionCfdis.FirstOrDefault(u => u.Id == id);
            }

        }
        public void AgregarConfiguracionCFDI(ConfiguracionCfdi configuracionCfdi)
        {

            _dbContext.ConfiguracionCfdis.Add(configuracionCfdi);
            _dbContext.SaveChanges();
        }

        public void ActualizarConfiguracionCFDI(ConfiguracionCfdi configuracionCfdi)
        {

            _dbContext.ConfiguracionCfdis.Update(configuracionCfdi);
            _dbContext.SaveChanges();
        }

        public void EliminarConfiguracionCFDI(int id)
        {

            var configuracionCfdiente = _dbContext.ConfiguracionCfdis.FirstOrDefault(u => u.Id == id);

            if (configuracionCfdiente != null)
            {
                _dbContext.ConfiguracionCfdis.Remove(configuracionCfdiente);
                _dbContext.SaveChanges();
            }

        }
    }
}
