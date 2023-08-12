using MediatR;
using Persistence;
using Domain;
using Application.Core;

namespace Application.FamilyMember
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int StudentId { get; set; }
            public List<FamilyMemberInfo> FamilyMember { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var studentObj = await _context.StudentInfos.FindAsync(request.StudentId);

                if (studentObj == null) return null;

                await _context.FamilyMembers.AddRangeAsync(request.FamilyMember);

                // foreach (var familyMember in request.FamilyMember)
                // {
                //     // Perform validation using FluentValidation
                //     var validator = new FamilyMemberValidator();
                //     var validationResult = await validator.ValidateAsync(familyMember);

                //     if (!validationResult.IsValid)
                //     {
                //         return Result<Unit>.Failure(validationResult.Errors.ToString());
                //     }

                //     // Save the family member to the database
                //     _context.FamilyMembers.Add(familyMember);
                // }

                var res = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (!res)
                    return Result<Unit>.Failure("Failed to create student record");

                return Result<Unit>.Success(Unit.Value);

            }
        }

    }
}