
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using QualfixAdmin.dal.DBContext;
using QualfixAdmin.model.QualfixAdminData;
using QualfixAdmin.model;


namespace QualfixAdmin.Services.Producto
{
    public class ProductoServices
    {
        private readonly QualfixAdminContext _dbContext;

        public ProductoServices(QualfixAdminContext qualfixAdminContext)
        {
            _dbContext = qualfixAdminContext;
        }

        public List<Productos> ObtenerTodosProductos(int pagina)
        {
            int tamañoPagina = 8;
            int skip = (pagina - 1) * tamañoPagina;

            return _dbContext.Productos
                .OrderBy(p => p.Id) // Asegúrate de ordenar los productos para una paginación coherente
                .Skip(skip)
                .Take(tamañoPagina)
                .ToList();
             
        }

        public Productos ObtenerproductoPorId(int  id)
        {
            using (var newContext = new QualfixAdminContext())
            {
                return _dbContext.Productos.FirstOrDefault(u => u.Id == id);
            }

        }

        public void AgregarProducto(Productos producto)
        {

            _dbContext.Productos.Add(producto);
            _dbContext.SaveChanges();
        }

        public void ActualizarProducto(Productos producto)
        {
             
               _dbContext.Productos.Update(producto);
            _dbContext.SaveChanges();
            

        }

        public void Eliminarproducto(int id)
        {
            using (var newContext = new QualfixAdminSecurityContext())
            {
                var producto = _dbContext.Productos.FirstOrDefault(u => u.Id == id);

                if (producto != null)
                {
                    _dbContext.Productos.Remove(producto);
                    _dbContext.SaveChanges();
                }
            }
        }




    }
}
