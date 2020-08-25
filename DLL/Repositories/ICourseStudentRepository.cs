using System;
using System.Collections.Generic;
using System.Text;
using DLL.DatabaseContext;
using DLL.Models;

namespace DLL.Repositories
{
    public interface ICourseStudentRepository:IRepositoryBase<CourseStudent>
    {

    }

    public class CourseStudentRepository : RepositoryBase<CourseStudent>, ICourseStudentRepository
    {
        public CourseStudentRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
