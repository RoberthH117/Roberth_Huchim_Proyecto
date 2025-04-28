using QualfixAdmin.dal.DBContext;
using QualfixAdmin.model.QualfixAdminData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.Services.EstadoService
{
    public class EstadoService
    {
        private readonly QualfixAdminContext _dbContext;

        public EstadoService(QualfixAdminContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Estado> ObtenerTodosEstados()
        {
            return _dbContext.Estados.ToList();
        }
    }
}
