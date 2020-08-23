using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.DatabaseContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> InsertStudentAsync(Student student);
        Task<List<Student>> GetAllAsync();
        Task<Student> GetAsync(string email);
        Task<Student> UpdateAsync(string email, Student student);
        Task<Student> DeleteAsync(string email);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student> InsertStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> DeleteAsync(string email)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Email == email);

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return student;

        }

        public async Task<Student> GetAsync(string email)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Email == email);


            return student;

        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            var findStudent = await _context.Students.FirstOrDefaultAsync(x => x.Email == email);
            findStudent.Name = student.Name;
            findStudent.Email = student.Email;
            _context.Students.Update(findStudent);
            await _context.SaveChangesAsync();
            return student;

        }
    }
}
