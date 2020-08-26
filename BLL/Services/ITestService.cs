﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using DLL.DatabaseContext;
using DLL.Models;
using DLL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface ITestService
    {
        Task InsertData();
        Task DummyData1();
        Task DummyData2();
        Task AddNewRoles();
    }

    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TestService(IUnitOfWork unitOfWork, ApplicationDbContext context,RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _roleManager = roleManager;
        }

        

        

        public async Task InsertData()
        {
            var department = new Department()
            {
                Code = "arts",
                Name = "art department"
            };

            var student = new Student()
            {
                Email = "art@gmail.com",
                Name = "mr arts"
            };
            await _unitOfWork.DepartmentRepository.CreateAsync(department);
            await _unitOfWork.StudentRepository.CreateAsync(student);

            await _unitOfWork.SaveChangesAsync();
        }
        
        public async Task DummyData1()
        {
            var studentDummy = new Faker<Student>()
                .RuleFor(u => u.Name, (f, u) => f.Name.FullName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name));
            var departmentDummy = new Faker<Department>()
                .RuleFor(o => o.Name, f => f.Name.FirstName())
                .RuleFor(o => o.Code, f => f.Name.LastName())
                .RuleFor(u => u.Students, f => studentDummy.Generate(50).ToList());

            var departmentListWithStudent = departmentDummy.Generate(100).ToList();
            await _context.Departments.AddRangeAsync(departmentListWithStudent);

            await _context.SaveChangesAsync();
        }

        
        public async Task DummyData2()
        {
            var courseDummy = new Faker<Course>()
                 .RuleFor(o => o.Name, f => f.Name.FirstName())
                 .RuleFor(o => o.Code, f => f.Name.LastName())
                 .RuleFor(u => u.Credit, f => f.Random.Number(1, 10));
             var courseDummyList = courseDummy.Generate(50).ToList();
             await _context.Courses.AddRangeAsync(courseDummyList);
             await _context.SaveChangesAsync();

            var allStudentId = await _context.Students.Select(x => x.StudentId).ToListAsync();
            var allCourseId = await _context.Courses.Select(x => x.CourseId).ToListAsync();
            int count = 0;

            foreach (var course in allCourseId)
            {
                var courseStudent = new List<CourseStudent>();
                var students = allStudentId.Skip(count).Take(5);
                foreach (var aStudent in students)
                {
                    courseStudent.Add(new CourseStudent()
                    {
                        CourseId = course,
                        StudentId = aStudent
                    });
                }

                await _context.CourseStudents.AddRangeAsync(courseStudent);
                await _context.SaveChangesAsync();
                count += 5;
            }




        }

        public async Task AddNewRoles()
        {
            var roleList = new List<string>()
            {
                "admin",
                "manager",
                "supervisor"

            };

            foreach (var role in roleList)
            {
                var exists = await _roleManager.FindByNameAsync(role);

                if (exists == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = role
                    });
                }
            }
        }
    }
}
