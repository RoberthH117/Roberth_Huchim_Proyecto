using QualfixAdmin.dal.DBContext;
using QualfixAdmin.model.QualfixAdminData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.Services.PaisService
{
    public class PaisService
    {
        private readonly QualfixAdminContext _dbContext;
        
        public PaisService(QualfixAdminContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Pai> ObtenerTodosPaises() 
        {
            return _dbContext.Pais.ToList();
        }
    }
}
