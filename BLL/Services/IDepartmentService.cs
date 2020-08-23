using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Models;
using DLL.Repositories;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IDepartmentService
    {
        Task<Department> InsertDepartmentAsync(DepartmentInsertRequestViewModel request);
        Task<List<Department>> GetAllAsync();
        Task<Department> GetAsync(string code);
        Task<Department> UpdateAsync(string code, Department department);
        Task<Department> DeleteAsync(string code);

        Task<bool> IsCodeExists(string code);
        Task<bool> IsNameExists(string name);
    }

    public class DepartmentService : IDepartmentService 
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> InsertDepartmentAsync(DepartmentInsertRequestViewModel request)
        {
            Department aDepartment = new Department();
            aDepartment.Code = request.Code;
            aDepartment.Name = request.Name;
            return await _departmentRepository.InsertDepartmentAsync(aDepartment);
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<Department> GetAsync(string code)
        {
            var department =  await _departmentRepository.GetAsync(code);

            if (department == null)
            {
                throw new ApplicationValidationException("Department not found!");
            }

            return department;
        }

        public async Task<Department> UpdateAsync(string code, Department aDepartment)
        {
            var department = await _departmentRepository.GetAsync(code);
            if (department == null)
            {
                throw new ApplicationValidationException("Department doesn't exist.");
            }

            if (!string.IsNullOrWhiteSpace(aDepartment.Code))
            {
                var codeAlreadyExists = await _departmentRepository.FindByCode(aDepartment.Code);
                if (codeAlreadyExists != null)
                {
                    throw new ApplicationValidationException("Your updated code already exists.");
                }

                department.Code = aDepartment.Code;
            }

            if (!string.IsNullOrWhiteSpace(aDepartment.Name))
            {
                var nameAlreadyExists = await _departmentRepository.FindByCode(aDepartment.Name);
                if (nameAlreadyExists != null)
                {
                    throw new ApplicationValidationException("Your updated name already exists.");
                }

                department.Name = aDepartment.Name;
            }

            if (await _departmentRepository.UpdateAsync(department))
            {
                return department;
            }
            throw new ApplicationValidationException("There is some problem updating the department.");
        }

        public async Task<Department> DeleteAsync(string code)
        {
            var department = await _departmentRepository.GetAsync(code);
            if (department == null)
            {
                throw new ApplicationValidationException("Department doesn't exist.");
            }

            if (await _departmentRepository.DeleteAsync(department))
            {
                return department;
            }
            throw new ApplicationValidationException("Cannot delete data!");
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var department = await _departmentRepository.FindByCode(code);
            if (department == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var department = await _departmentRepository.FindByName(name);
            if (department == null)
            {
                return true;
            }

            return false;
        }
    }
}
