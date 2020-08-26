using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CourseEnrollController : MainApiController
    {
        private readonly ICourseStudentService _courseStudentService;

        public CourseEnrollController(ICourseStudentService courseStudentService)
        {
            _courseStudentService = courseStudentService;
        }
        [HttpPost]
        public async Task<IActionResult> InsertCourse(CourseAssignInsertViewModel request)
        {
            return Ok(await _courseStudentService.InsertCourseAsync(request));
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> CourseList(int studentId)
        {
            return Ok(await _courseStudentService.CourseListAsync(studentId));
        }
        

    }
}
