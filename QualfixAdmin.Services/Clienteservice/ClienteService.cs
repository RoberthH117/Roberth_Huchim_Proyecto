
using Microsoft.EntityFrameworkCore;
using QualfixAdmin.dal.DBContext;
using QualfixAdmin.model.QualfixAdminData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.Services.ClienteService
{
    public class ClienteService
    {
        private readonly QualfixAdminContext _dbContext;

        public ClienteService(QualfixAdminContext qualfixAdminContext)
        {
            _dbContext = qualfixAdminContext;
        }

        public List<Cliente> ObtenerTodosClientes()
        {
           return _dbContext.Clientes.ToList();
        }

        public Cliente ObtenerClientePorId(int id)
        {
            using (var newContext = new QualfixAdminSecurityContext())
            {
                return _dbContext.Clientes
                    .Include(info => info.Pais)
                    .Include(info => info.Estado)
                    .Include(info => info.Ciudad)
                    .FirstOrDefault(u => u.ClienteId == id);
            }

        }

        public void AgregarCliente(Cliente cliente)
        {

            _dbContext.Clientes.Add(cliente);
            _dbContext.SaveChanges();
        }

        public void ActualizarCliente(Cliente cliente)
        {
            
                _dbContext.Clientes.Update(cliente);
                _dbContext.SaveChanges();
        }

        

        public void EliminarCliente(int id)
        {
           
                var cliente = _dbContext.Clientes.FirstOrDefault(u => u.ClienteId == id);

                if (cliente != null)
                {
                    _dbContext.Clientes.Remove(cliente);
                    _dbContext.SaveChanges();
                }
             
        }

    }
}
