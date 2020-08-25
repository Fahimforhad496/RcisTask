using BLL.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL.Models;
using DLL.Repositories;
using FluentValidation.Internal;
using Utility.Exceptions;
using Utility.Models;

namespace BLL.Services
{
    public interface ICourseStudentService
    {
        Task<ApiSuccessResponse> InsertCourseAsync(CourseAssignInsertViewModel request);
        
    }

    public class CourseStudentService : ICourseStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseStudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiSuccessResponse> InsertCourseAsync(CourseAssignInsertViewModel request)
        {
            var isStudentAlreadyEnroll = await _unitOfWork.CourseStudentRepository.FindSingleAsync(x =>
                x.CourseId == request.CourseId && x.StudentId == request.StudentId);
            if (isStudentAlreadyEnroll != null)
            {
                throw new ApplicationValidationException("This student already enrolled in this course");
            }

            var courseStudent = new CourseStudent()
            {
                CourseId = request.CourseId,
                StudentId = request.StudentId
            };

            await _unitOfWork.CourseStudentRepository.CreateAsync(courseStudent);
            if (await _unitOfWork.SaveChangesAsync())
            {
                return new ApiSuccessResponse()
                {
                    StatusCode = 200,
                    Message = "Student enroll successful."
                };
            }
            throw new ApplicationValidationException("Enrollment has some problems.");
        }
       
        
    }
}
