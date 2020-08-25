using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Models;
using DLL.Repositories;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IStudentService
    {
        Task<Student> InsertStudentAsync(StudentInsertRequestViewModel studentRequest);
        IQueryable<Student> GetAllAsync();
        Task<Student> GetAsync(string email);
        Task<Student> UpdateAsync(string email, Student student);
        Task<Student> DeleteAsync(string email);
        Task<bool> EmailExists(string email);
        Task<bool> IsIdExists(int id);
    }

    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;


        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Student> InsertStudentAsync(StudentInsertRequestViewModel studentRequest)
        {
            var student = new Student()
            {
                Email = studentRequest.Email,
                Name = studentRequest.Name,
                DepartmentId = studentRequest.DepartmentId
                
                
            };
            
            await _unitOfWork.StudentRepository.CreateAsync(student);
            if (await _unitOfWork.SaveChangesAsync())
            {
                return student;
            }
            throw new ApplicationValidationException("There is a problem in inserting student.");
        }

        public IQueryable<Student> GetAllAsync()
        {
            return _unitOfWork.StudentRepository.QueryAll();
        }

        public async Task<Student> GetAsync(string email)
        {
            return await _unitOfWork.StudentRepository.FindSingleAsync(x=>x.Email==email);
        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            var dbStudent =await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);
            if (dbStudent==null)
            {
                throw new ApplicationValidationException("Could not find the student you're looking for in database!");
            }

            dbStudent.Name = student.Name;
            _unitOfWork.StudentRepository.Update(dbStudent);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return dbStudent;
            }
            throw new ApplicationValidationException("There is a problem in updating student.");
        }

        public async Task<Student> DeleteAsync(string email)
        {
            var dbStudent = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);
            if (dbStudent == null)
            {
                throw new ApplicationValidationException("Could not find the student you're looking for in database!");
            }

            _unitOfWork.StudentRepository.Delete(dbStudent);
            if (await _unitOfWork.SaveChangesAsync())
            {
                return dbStudent;
            }
            throw new ApplicationValidationException("There is a problem deleting student!");
        }
        public async Task<bool> EmailExists(string email)
        {
            var student = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.Email == email);

            if (student == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsIdExists(int id)
        {
            var student = await _unitOfWork.StudentRepository.FindSingleAsync(x => x.StudentId == id);

            if (student == null)
            {
                return true;
            }

            return false;
        }
    }
}
