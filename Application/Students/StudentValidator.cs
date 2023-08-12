using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using FluentValidation;

namespace Application.Students
{
    public class StudentValidator : AbstractValidator<StudentInfo>
    {

        public StudentValidator()
        {
            // RuleFor(x => x.FName).NotEmpty().WithMessage("Family member name is required.");

            RuleFor(x => x.FName).NotEmpty();
            RuleFor(x => x.LName).NotEmpty();
            RuleFor(x => x.DOB).NotEmpty();
        }
    }
}