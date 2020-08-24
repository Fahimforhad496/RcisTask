using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.DatabaseContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        
    }

    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
