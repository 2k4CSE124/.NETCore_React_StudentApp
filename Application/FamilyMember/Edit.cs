using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.FamilyMember
{
    public class Edit
    {
        /*----------------- Edit List of FamilyMembers ----------------------------------*/
        public class Command : IRequest<Result<Unit>>
        {
            public int StudentId { get; set; }
            public List<FamilyMemberInfo> FamilyMember { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                 var student = await _context.StudentInfos.Include(s => s.FamilyMembers).FirstOrDefaultAsync(s => s.Id == request.StudentId);

                // Remove/delete family members
                var deletedFamilyMembers = from itemA in student.FamilyMembers
                                           where !request.FamilyMember.Any(itemB => itemB.Id == itemA.Id)
                                           select itemA;

                foreach (var deletedFamilyMember in deletedFamilyMembers)
                {
                    student.FamilyMembers.Remove(deletedFamilyMember);
                }

                // Update existing family members and add new ones
                foreach (var item in request.FamilyMember)
                {
                    var existingFamilyMember = student.FamilyMembers.FirstOrDefault(x => (x.Id == item.Id) && (item.Id != 0));
                    if (existingFamilyMember != null)
                    {
                        existingFamilyMember.Name = item.Name;
                        existingFamilyMember.RelationId = item.RelationId;
                        existingFamilyMember.NationalityId = item.NationalityId;
                        existingFamilyMember.StudentId = item.StudentId;
                    }
                    else
                    {
                        // Add new family member
                        student.FamilyMembers.Add(item);
                    }
                }

                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }

        /*----------------- Edit Single Record ----------------------------------*/

        public class SingleRecordCommand : IRequest<Result<FamilyMemberInfo>>
        {
            public int FamilyMemberId { get; set; }
            public FamilyMemberInfo FamilyMember { get; set; }
        }

        public class SingleRecordHandler : IRequestHandler<SingleRecordCommand, Result<FamilyMemberInfo>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public SingleRecordHandler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<FamilyMemberInfo>> Handle(SingleRecordCommand request, CancellationToken cancellationToken)
            {
                var familyMemberObj = await _context.FamilyMembers.FindAsync(request.FamilyMemberId);

                if (familyMemberObj == null) return null;

                // Mapping StudentInfo model
                _mapper.Map(request.FamilyMember, familyMemberObj);

                var res = await _context.SaveChangesAsync() > 0;

                if (!res) return Result<FamilyMemberInfo>.Failure("Failed to update the record");

                return Result<FamilyMemberInfo>.Success(request.FamilyMember);
            }
        }

        /*----------------- Edit Nationality of Specific Family Member ----------------------------------*/

        public class EditNationalityCommand : IRequest<Result<FamilyMemberInfo>>
        {
            public int FamilyMemberId { get; set; }
            public int NationalityId { get; set; }
        }

        public class EditNationalityHandler : IRequestHandler<EditNationalityCommand, Result<FamilyMemberInfo>>
        {
            private readonly DataContext _context;
            public EditNationalityHandler(DataContext context)
            {
                _context = context;
            }
            public async Task<Result<FamilyMemberInfo>> Handle(EditNationalityCommand request, CancellationToken cancellationToken)
            {
                var familyMemberObj = await _context.FamilyMembers.FindAsync(request.FamilyMemberId);

                if (familyMemberObj == null) return null;

                familyMemberObj.NationalityId = request.NationalityId;


                var res = await _context.SaveChangesAsync() > 0;

                if (!res) return Result<FamilyMemberInfo>.Failure("Failed to update the record");

                return Result<FamilyMemberInfo>.Success(familyMemberObj);

            }
        }

    }
}