using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.DatabaseContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
        
    }

    public class DepartmentRepository : RepositoryBase<Department>,IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
