﻿using Bul.Authority.DBConnection.DbContextBaseService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Service
{
    public class SqRoleServices : BaseService
    {
        public SqRoleServices(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }
    }
}
