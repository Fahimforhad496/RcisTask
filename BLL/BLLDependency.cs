using System;
using System.Collections.Generic;
using System.Text;
using BLL.Request;
using BLL.Services;
using DLL;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class BLLDependency
    {
        public static void AllDependency(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<ICourseStudentService, CourseStudentService>();
            services.AddTransient<ITransactionService, TransactionService>();
            

            AllFluentValidationDependency(services);
        }

        private static void AllFluentValidationDependency(IServiceCollection services)
        {
            services
                .AddTransient<IValidator<StudentInsertRequestViewModel>, StudentInsertRequestViewModelValidator
                >();

            services
                .AddTransient<IValidator<DepartmentInsertRequestViewModel>, DepartmentInsertRequestViewModelValidator
                >();
            services
                .AddTransient<IValidator<CourseInsertRequestViewModel>, CourseInsertRequestViewModelValidator
                >();
            services
                .AddTransient<IValidator<CourseAssignInsertViewModel>, CourseAssignInsertViewModelValidator
                >();
        }
    }
}
