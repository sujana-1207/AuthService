using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repository
{
    public class LoginRepository : ILogin
    {
        private DiagnosticLabContext _cxt;
        
        public LoginRepository()
        {
            _cxt = new DiagnosticLabContext();
        }
        public User GetUser(string uname)
        {
            User u = new User();
            u.Username = "sujana";
            u.Password = "psujana";
            return u;
        }
    }
}
