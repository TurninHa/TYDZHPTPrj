using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Entity.Interface
{
    public abstract class BulApplication
    {
        public abstract IHttpContextAccessor HttpContextAccessor { get; }
    }
}
