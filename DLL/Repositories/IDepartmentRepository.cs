using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.DatabaseContext;
using DLL.Models;

namespace DLL.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department> InsertDepartment(Department department);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Department> InsertDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

    }
}
