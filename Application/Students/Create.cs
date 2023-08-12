using Application.Core;
using Application.FamilyMember;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Students
{
    public class Create
    {
        public class Command : IRequest<Result<StudentInfo>>
        {
            public StudentInfo StudentInfo { get; set; }
        }

        // Apply validation on StudenInfo Model
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.StudentInfo).SetValidator(new StudentValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<StudentInfo>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }

            public async Task<Result<StudentInfo>> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.StudentInfos.Add(request.StudentInfo);
                var res = await _context.SaveChangesAsync() > 0;

                if (!res) return Result<StudentInfo>.Failure("Failed to create student record");

                return Result<StudentInfo>.Success(request.StudentInfo);
            }
        }

        /*------------------------------------------------------------------------------------*/

        public class StudentFamilyMemberCommand : IRequest<Result<Unit>>
        {
            public int StudentId { get; set; }
            public List<FamilyMemberInfo> FamilyMember { get; set; }
        }

        public class StudentFamilyMemberHandler : IRequestHandler<StudentFamilyMemberCommand, Result<Unit>>
        {
            private readonly DataContext _context;
            public StudentFamilyMemberHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(StudentFamilyMemberCommand request, CancellationToken cancellationToken)
            {
                var studentObj = await _context.StudentInfos.FindAsync(request.StudentId);
                if (studentObj == null) return null;

                // await _context.FamilyMembers.AddRangeAsync(request.FamilyMember);

                foreach (var familyMember in request.FamilyMember)
                {
                    familyMember.StudentId = request.StudentId;

                    // Perform validation using FluentValidation
                    var validator = new FamilyMemberValidator();
                    var validationResult = await validator.ValidateAsync(familyMember);

                    if (!validationResult.IsValid)
                    {
                        return Result<Unit>.Failure(validationResult.Errors.ToString());
                    }

                    // Save the family member to the database
                    _context.FamilyMembers.Add(familyMember);
                }

                var res = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!res)
                    return Result<Unit>.Failure("Failed to create student record");

                return Result<Unit>.Success(Unit.Value);

            }
        }

    }
}