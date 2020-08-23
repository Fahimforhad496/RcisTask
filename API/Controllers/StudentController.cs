using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StudentController : MainApiController
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(StudentStatic.GetStudents());

        }

        [HttpGet("{email}")]
        public IActionResult GetA(string email)
        {
            return Ok(StudentStatic.GetAStudent(email));
        }

        [HttpPost]
        public IActionResult Insert(Student student)
        {
            return Ok(StudentStatic.InsertStudent(student));
        }

        [HttpPut("{email}")]
        public IActionResult Update(string email, Student student)
        {
            return Ok(StudentStatic.UpdateStudent(email, student));
        }

        [HttpDelete("{email}")]
        public IActionResult Delete(string email)
        {
            return Ok(StudentStatic.DeleteStudent(email));
        }
    }

    public static class StudentStatic
    {
        private static List<Student> Students { get; set; } = new List<Student>();

        public static Student InsertStudent(Student student)
        {
            Students.Add(student);
            return student;
        }

        public static List<Student> GetStudents()
        {
            return Students;
        }

        public static Student GetAStudent(string email)
        {
            return Students.FirstOrDefault(x => x.Email == email);
        }

        public static Student UpdateStudent(string name, Student student)
        {
            Student result = new Student();
            foreach (var aStudent in Students)
            {
                if (name == aStudent.Name)
                {
                    aStudent.Name = student.Name;
                    result = aStudent;
                }

            }

            return result;
        }

        public static Student DeleteStudent(string email)
        {
            var student = Students.FirstOrDefault(x => x.Email == email);
            Students = Students.Where(x => x.Email != student.Email).ToList();
            return student;
        }
    }
}
