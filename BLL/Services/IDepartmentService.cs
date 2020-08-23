using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.Models;
using DLL.Repositories;

namespace BLL.Services
{
    public interface IDepartmentService
    {
        Task<Department> InsertDepartmentAsync(Department department);
        Task<List<Department>> GetAllAsync();
        Task<Department> GetAsync(string code);
        Task<Department> UpdateAsync(string code, Department department);
        Task<Department> DeleteAsync(string code);
    }

    public class DepartmentService : IDepartmentService 
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> InsertDepartmentAsync(Department department)
        {
            return await _departmentRepository.InsertDepartmentAsync(department);
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<Department> GetAsync(string code)
        {
            return await _departmentRepository.GetAsync(code);
        }

        public async Task<Department> UpdateAsync(string code, Department department)
        {
            return await _departmentRepository.UpdateAsync(code, department);

        }

        public async Task<Department> DeleteAsync(string code)
        {
            return await _departmentRepository.DeleteAsync(code);
        }
    }
}
