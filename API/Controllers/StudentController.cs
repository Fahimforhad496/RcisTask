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
    public class StudentController : MainApiController
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _studentRepository.GetAllAsync());

        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetA(string email)
        {
            return Ok(await _studentRepository.GetAsync(email));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Student student)
        {
            return Ok(await _studentRepository.InsertStudentAsync(student));
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Update(string email, Student student)
        {
            return Ok(await _studentRepository.UpdateAsync(email, student));
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            return Ok(await _studentRepository.DeleteAsync(email));
        }
    }

    
}
