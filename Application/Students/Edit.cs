using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Students
{
    public class Edit
    {
        public class Command : IRequest<Result<StudentInfo>>
        {
            public StudentInfo StudentInfo { get; set; }
        }

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
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<StudentInfo>> Handle(Command request, CancellationToken cancellationToken)
            {
                var studentObj = await _context.StudentInfos.FindAsync(request.StudentInfo.Id);

                if (studentObj == null) return null;

                // Mapping StudentInfo model
                _mapper.Map(request.StudentInfo, studentObj);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                { }

                // var res = await _context.SaveChangesAsync() > 0;
                // if (!res) return Result<StudentInfo>.Failure("Failed to update the record");

                return Result<StudentInfo>.Success(request.StudentInfo);

            }
        }

        /*----------------------------------------------------------------------------*/

        public class EditNationalityCommand : IRequest<Result<StudentInfo>>
        {
            public int StudentId { get; set; }
            public int NationalityId { get; set; }
        }

        public class EditNationalityHandler : IRequestHandler<EditNationalityCommand, Result<StudentInfo>>
        {
            private readonly DataContext _context;
            public EditNationalityHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<StudentInfo>> Handle(EditNationalityCommand request, CancellationToken cancellationToken)
            {
                var studentObj = await _context.StudentInfos.FindAsync(request.StudentId);

                if (studentObj == null) return null;

                studentObj.NationalityId = request.NationalityId;


                var res = await _context.SaveChangesAsync() > 0;

                if (!res) return Result<StudentInfo>.Failure("Failed to update the record");

                return Result<StudentInfo>.Success(studentObj);

            }
        }


    }
}