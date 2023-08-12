using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Students
{
    public class List
    {
        public class Query : IRequest<Result<List<StudentInfo>>> { }
        public class Handler : IRequestHandler<Query, Result<List<StudentInfo>>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<StudentInfo>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = await _context.StudentInfos.ToListAsync();
                return Result<List<StudentInfo>>.Success(response);
            }
        }
    }
}