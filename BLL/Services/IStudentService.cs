using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.Models;
using DLL.Repositories;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IStudentService
    {
        Task<Student> InsertStudentAsync(Student student);
        Task<List<Student>> GetAllAsync();
        Task<Student> GetAsync(string email);
        Task<Student> UpdateAsync(string email, Student student);
        Task<Student> DeleteAsync(string email);
    }

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;


        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> InsertStudentAsync(Student student)
        {
            await _studentRepository.CreateAsync(student);
            if (await _studentRepository.SaveCompletedAsync())
            {
                return student;
            }
            throw new ApplicationValidationException("There is a problem in inserting student.");
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _studentRepository.GetList();
        }

        public async Task<Student> GetAsync(string email)
        {
            return await _studentRepository.FindSingleAsync(x=>x.Email==email);
        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            var dbStudent =await _studentRepository.FindSingleAsync(x => x.Email == email);
            if (dbStudent==null)
            {
                throw new ApplicationValidationException("Could not find the student you're looking for in database!");
            }

            dbStudent.Name = student.Name;
            _studentRepository.Update(dbStudent);

            if (await _studentRepository.SaveCompletedAsync())
            {
                return dbStudent;
            }
            throw new ApplicationValidationException("There is a problem in updating student.");
        }

        public async Task<Student> DeleteAsync(string email)
        {
            var dbStudent = await _studentRepository.FindSingleAsync(x => x.Email == email);
            if (dbStudent == null)
            {
                throw new ApplicationValidationException("Could not find the student you're looking for in database!");
            }

            _studentRepository.Delete(dbStudent);
            if (await _studentRepository.SaveCompletedAsync())
            {
                return dbStudent;
            }
            throw new ApplicationValidationException("There is a problem deleting student!");
        }
    }
}
