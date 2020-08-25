using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Models;
using DLL.Repositories;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface ICourseService
    {
        Task<Course> InsertAsync(CourseInsertRequestViewModel request);
        Task<List<Course>> GetAllAsync();
        Task<Course> UpdateAsync(string code, Course course);
        Task<Course> DeleteAsync(string code);
        Task<Course> GetAAsync(string code);

        Task<bool> IsCodeExists(string code);
        Task<bool> IsNameExists(string name);
        Task<bool> IsIdExists(int id);
    }

    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Course> InsertAsync(CourseInsertRequestViewModel request)
        {
            var course = new Course();
            course.Code = request.Code;
            course.Name = request.Name;
            course.Credit = request.Credit;
            await _unitOfWork.CourseRepository.CreateAsync(course);

            if (await _unitOfWork.SaveChangesAsync())
            {
                return course;
            }

            throw new ApplicationValidationException("There is some problems in course insertion.");
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _unitOfWork.CourseRepository.GetList();
        }

        public async Task<Course> UpdateAsync(string code, Course aCourse)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);

            if (course == null)
            {
                throw new ApplicationValidationException("Couldn't find the course");
            }

            if (!string.IsNullOrWhiteSpace(aCourse.Code))
            {
                var existsAlreadyCode = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationException("Code already present in the system.");
                }

                course.Code = aCourse.Code;
            }

            if (!string.IsNullOrWhiteSpace(aCourse.Name))
            {
                var existsAlreadyCode = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Name == aCourse.Name);
                if (existsAlreadyCode != null)
                {
                    throw new ApplicationValidationException("Name already present in the system.");
                }

                course.Name = aCourse.Name;
            }

            _unitOfWork.CourseRepository.Update(course);
            if (await _unitOfWork.SaveChangesAsync())
            {
                return course;
            }

            throw new ApplicationValidationException("There is some issue in updating.");
        }

        public async Task<Course> DeleteAsync(string code)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);

            if (course == null)
            {
                throw new ApplicationValidationException("Couldn't find the course.");
            }

            _unitOfWork.CourseRepository.Delete(course);
            if (await _unitOfWork.SaveChangesAsync())
            {
                return course;
            }

            throw new ApplicationValidationException("There is a problem deleting this data.");
        }

        public async Task<Course> GetAAsync(string code)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);

            if (course == null)
            {
                throw new ApplicationValidationException("course not found");
            }

            return course;
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code); ;

            if (course == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Name == name); ;

            if (course == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsIdExists(int id)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.CourseId == id); ;

            if (course == null)
            {
                return true;
            }

            return false;
        }
    }
}
