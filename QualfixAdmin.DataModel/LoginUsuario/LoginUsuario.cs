using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.DataModel.LoginUsuario
{
    public class LoginUsuario
    {

        public string correo { get;set; }
        
        public string passwordHash { get;set; }  

        public bool status { get;set; }


    }
}
