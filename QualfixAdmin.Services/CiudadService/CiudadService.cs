using QualfixAdmin.dal.DBContext;
using QualfixAdmin.model.QualfixAdminData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.Services.CiudadService
{
    public class CiudadService
    {
        private readonly QualfixAdminContext _dbContext;

        public CiudadService(QualfixAdminContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Ciudad> ObtenerTodosCiudades()
        {
            return _dbContext.Ciudads.ToList();
        }
    }
}
