using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Request
{
    public class StudentInsertRequestViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
    }

    public class StudentInsertRequestViewModelValidator : AbstractValidator<StudentInsertRequestViewModel>
    {
        private readonly IServiceProvider _serviceProvider;


        public StudentInsertRequestViewModelValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            RuleFor(x => x.Name).NotNull()
                .NotEmpty().MinimumLength(4).MaximumLength(50);
            RuleFor(x => x.Email).NotNull()
                .NotEmpty().MinimumLength(3).EmailAddress().MustAsync(EmailExists)
                .WithMessage("This Email Already exists.");
            RuleFor(x => x.DepartmentId).GreaterThan(0).MustAsync(DepartmentExists)
                .WithMessage("Department is not exist in this system.");

        }

        private async Task<bool> EmailExists(string email, CancellationToken arg2)
        {
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<IStudentService>();

            return await requiredService.EmailExists(email);



        }

        private async Task<bool> DepartmentExists(int id, CancellationToken arg2)
        {

            if (id == 0)
            {
                return true;
            }

            var requiredService = _serviceProvider.GetRequiredService<IDepartmentService>();

            return !await requiredService.IsIdExists(id);
        }
    }
}