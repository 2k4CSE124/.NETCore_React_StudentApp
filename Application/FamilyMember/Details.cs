using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.FamilyMember
{
    public class Details
    {
        public class Query : IRequest<Result<Country>>
        {
            public int FamilyMemberId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<Country>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Country>> Handle(Query request, CancellationToken cancellationToken)
            {
                var familyMemberObj = await _context.FamilyMembers.FindAsync(request.FamilyMemberId);

                if (familyMemberObj == null) return null;

                var familyMember = _context.FamilyMembers.Include(s => s.Nationality)
                                        .FirstOrDefault(s => s.Id == request.FamilyMemberId);

                return Result<Country>.Success(familyMember.Nationality);
            }
        }
    }
}