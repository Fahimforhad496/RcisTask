using System;
using System.Collections.Generic;
using System.Text;
using BLL.Services;
using DLL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class BLLDependency
    {
        public static void AllDependency(IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddTransient<IDepartmentService, DepartmentService>();
        }
    }
}
