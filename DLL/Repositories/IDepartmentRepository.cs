using System;
using System.Collections.Generic;
using System.Text;
using DLL.Models;

namespace DLL.Repositories
{
    public interface IDepartmentRepository
    {
        Department InsertDepartment(Department department);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        public Department InsertDepartment(Department department)
        {
            throw new System.NotImplementedException();
        }

    }
}
