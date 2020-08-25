using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.DatabaseContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repositories
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
        
    }

    public class CourseRepository : RepositoryBase<Course>,ICourseRepository        
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
