using Bul.Authority.Application.ApplicationBase;
using Bul.Authority.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application
{
    public class SqRoleApplication : BulAuthorityApplication
    {
        private readonly SqRoleServices _services;
        public SqRoleApplication(SqRoleServices sqRoleServices)
        {
            this._services = sqRoleServices;
        }


    }
}
