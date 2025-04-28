using Microsoft.EntityFrameworkCore;
using QualfixAdmin.dal.DBContext;
using QualfixAdmin.model.QualfixAdminData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.Services.InformacionFiscal
{
    public class InformacionFiscalService
    {
        private readonly QualfixAdminContext _dbContext;

        public InformacionFiscalService(QualfixAdminContext qualfixAdminContext)
        {
            _dbContext = qualfixAdminContext;
        }

        public List<InformacionFiscals> ObtenerTodasInformacionesFiscales()
        {
            return _dbContext.InformacionFiscal.ToList();
        }
        public InformacionFiscals ObtenerinfoFiscalPorId(int id)
        {
            using (var newContext = new QualfixAdminSecurityContext())
            {
                return _dbContext.InformacionFiscal
                    .Include(info => info.Pais)
                    .Include(info => info.Estado)
                    .Include(info => info.Ciudad)
                    .Include(info => info.RegFiscal)
                    .FirstOrDefault(u => u.InformacionFiscalId == id);
            }

        }
        public int AgregarInformacionFiscal(InformacionFiscals infoFiscal)
        {
            _dbContext.InformacionFiscal.Add(infoFiscal);
            _dbContext.SaveChanges();

            // Devolver el ID del nuevo registro
            return infoFiscal.InformacionFiscalId;
        }

        public void ActualizarInformacionFiscal(InformacionFiscals infoFiscal)
        {

            _dbContext.InformacionFiscal.Update(infoFiscal);
            _dbContext.SaveChanges();
        }



        public void EliminarInformacionFiscal(int id)
        {

            var infoFiscal = _dbContext.InformacionFiscal.FirstOrDefault(u => u.InformacionFiscalId == id);

            if (infoFiscal != null)
            {
                _dbContext.InformacionFiscal.Remove(infoFiscal);
                _dbContext.SaveChanges();
            }

        }

    }
}
