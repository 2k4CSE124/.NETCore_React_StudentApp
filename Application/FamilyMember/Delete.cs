using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.FamilyMember
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
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
                var familyMembersToDelete = await _context.FamilyMembers
                                            .Where(fm => fm.StudentId == request.Id).ToListAsync();

                if (familyMembersToDelete.Any())
                {
                    _context.FamilyMembers.RemoveRange(familyMembersToDelete);

                    var res = await _context.SaveChangesAsync() > 0;

                    if (!res)
                        return Result<Unit>.Failure("Failed to delete the record");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }

    }
}