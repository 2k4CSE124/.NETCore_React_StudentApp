using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.FamilyMember
{
    public class List
    {
        public class Query : IRequest<List<FamilyMemberInfo>> { }
        public class Handler : IRequestHandler<Query, List<FamilyMemberInfo>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<FamilyMemberInfo>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.FamilyMembers.ToListAsync();
            }
        }


        /*--------------------------------------------------------*/
        public class GetFamilyNationalityQuery : IRequest<Country>
        {
            public int FamilyId { get; set; }
        }
        public class FamilyNationalityHandler : IRequestHandler<GetFamilyNationalityQuery, Country>
        {
            private readonly DataContext _context;
            public FamilyNationalityHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<Country> Handle(GetFamilyNationalityQuery request, CancellationToken cancellationToken)
            {
                var family = await _context.FamilyMembers.FindAsync(request.FamilyId)
                                    ?? throw new KeyNotFoundException("Student not found");

                return family.Nationality;
            }
        }


    }
}