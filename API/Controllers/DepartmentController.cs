using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.Models;
using DLL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class DepartmentController : MainApiController
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(DepartmentStatic.GetDepartments());

        }

        [HttpGet("{code}")]
        public IActionResult GetA(string code)
        {
            return Ok(DepartmentStatic.GetADepartment(code));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Department department)
        {
            return Ok(await _departmentRepository.InsertDepartment(department));
        }

        [HttpPut("{code}")]
        public IActionResult Update(string code, Department department)
        {
            return Ok(DepartmentStatic.UpdateDepartment(code, department));
        }

        [HttpDelete("{code}")]
        public IActionResult Delete(string code)
        {
            return Ok(DepartmentStatic.DeleteDepartment(code));
        }

    }

    public static class DepartmentStatic
    {
        private static List<Department> Departments { get; set; } = new List<Department>();

        public static Department InsertDepartment(Department department)
        {
            Departments.Add(department);
            return department;
        }

        public static List<Department> GetDepartments()
        {
            return Departments;
        }

        public static Department GetADepartment(string code)
        {
            return Departments.FirstOrDefault(x => x.Code == code);
        }

        public static Department UpdateDepartment(string code, Department department)
        {
            Department result = new Department();
            foreach (var aDepartment in Departments)
            {
                if (code == aDepartment.Code)
                {
                    aDepartment.Name = department.Name;
                    result = aDepartment;
                }

            }

            return result;
        }

        public static Department DeleteDepartment(string code)
        {
            var department = Departments.FirstOrDefault(x => x.Code == code);
            Departments = Departments.Where(x => x.Code != department.Code).ToList();
            return department;
        }
    }
}
