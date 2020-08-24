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
            await _departmentRepository.CreateAsync(aDepartment);
            if (await _departmentRepository.SaveCompletedAsync())
            {
                return aDepartment;
            }

            throw new ApplicationValidationException("Department insertion has some problems.");

        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _departmentRepository.GetList();
        }

        public async Task<Department> GetAsync(string code)
        {
            var department =  await _departmentRepository.FindSingleAsync(x=>x.Code==code);

            if (department == null)
            {
                throw new ApplicationValidationException("Department not found!");
            }

            return department;
        }

        public async Task<Department> UpdateAsync(string code, Department aDepartment)
        {
            var department = await _departmentRepository.FindSingleAsync(x=>x.Code==code);
            if (department == null)
            {
                throw new ApplicationValidationException("Department doesn't exist.");
            }

            if (!string.IsNullOrWhiteSpace(aDepartment.Code))
            {
                var codeAlreadyExists = await _departmentRepository.FindSingleAsync(x => x.Code == code);
                if (codeAlreadyExists != null)
                {
                    throw new ApplicationValidationException("Your updated code already exists.");
                }

                department.Code = aDepartment.Code;
            }

            if (!string.IsNullOrWhiteSpace(aDepartment.Name))
            {
                var nameAlreadyExists = await _departmentRepository.FindSingleAsync(x => x.Name == aDepartment.Name);
                if (nameAlreadyExists != null)
                {
                    throw new ApplicationValidationException("Your updated name already exists.");
                }

                department.Name = aDepartment.Name;
            }

            _departmentRepository.Update(department);

            if (await _departmentRepository.SaveCompletedAsync())
            {
                return department;
            }
            
            throw new ApplicationValidationException("There is some problem updating the department.");
        }

        public async Task<Department> DeleteAsync(string code)
        {
            var department = await _departmentRepository.FindSingleAsync(x => x.Code == code);
            if (department == null)
            {
                throw new ApplicationValidationException("Department doesn't exist.");
            }

            _departmentRepository.Delete(department);
            if (await _departmentRepository.SaveCompletedAsync())
            {
                return department;
            }

            throw new ApplicationValidationException("Cannot delete data!");
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var department = await _departmentRepository.FindSingleAsync(x => x.Code == code);
            if (department == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var department = await _departmentRepository.FindSingleAsync(x => x.Name == name);
            if (department == null)
            {
                return true;
            }

            return false;
        }
    }
}
