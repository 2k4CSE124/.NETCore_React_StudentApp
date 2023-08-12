using Application.Core;
using MediatR;
using Persistence;

namespace Application.Students
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
                var studentObj = await _context.StudentInfos.FindAsync(request.Id);

                if (studentObj == null) return null;

                _context.Remove(studentObj);
                var res = await _context.SaveChangesAsync() > 0;

                if (!res)
                    return Result<Unit>.Failure("Failed to delete the record");

                return Result<Unit>.Success(Unit.Value);
            }
        }

    }
}