using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Students
{
    public class Details
    {
        /*--------------------------------------------------------*/
        public class GetStudentNationalityQuery : IRequest<Result<Country>>
        {
            public int StudentId { get; set; }
        }
        public class StudentNationalityHandler : IRequestHandler<GetStudentNationalityQuery, Result<Country>>
        {
            private readonly DataContext _context;
            public StudentNationalityHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Country>> Handle(GetStudentNationalityQuery request, CancellationToken cancellationToken)
            {

                var studentObj = await _context.StudentInfos.FindAsync(request.StudentId);
                if (studentObj == null) return null;

                var student = _context.StudentInfos.Include(s => s.Nationality)
                                        .FirstOrDefault(s => s.Id == request.StudentId)
                                        ?? throw new KeyNotFoundException("Student family member not found");

                // var student = await _context.StudentInfos.FindAsync(request.StudentId)
                // ?? throw new KeyNotFoundException("Student not found");

                return Result<Country>.Success(student.Nationality);
            }
        }

        /*--------------------------------------------------------*/
        public class GetStudentFamilyQuery : IRequest<Result<List<FamilyMemberInfo>>>
        {
            public int StudentId { get; set; }
        }
        public class StudentFamilyHandler : IRequestHandler<GetStudentFamilyQuery, Result<List<FamilyMemberInfo>>>
        {
            private readonly DataContext _context;
            public StudentFamilyHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<FamilyMemberInfo>>> Handle(GetStudentFamilyQuery request, CancellationToken cancellationToken)
            {
                var studentObj = await _context.StudentInfos.FindAsync(request.StudentId);
                if (studentObj == null) return null;

                // var familyMemberList = _context.FamilyMembers.Where(s => s.StudentId == request.StudentId).ToList()
                //                         ?? throw new KeyNotFoundException("Student family member not found");

                var familyMemberList = _context.StudentInfos.Include(s => s.FamilyMembers)
                                            .FirstOrDefault(s => s.Id == request.StudentId)
                                         ?? throw new KeyNotFoundException("Student family member not found");

                return Result<List<FamilyMemberInfo>>.Success(familyMemberList.FamilyMembers.ToList());
            }
        }

    }
}