using Domain;
using FluentValidation;

namespace Application.FamilyMember
{
    public class FamilyMemberValidator : AbstractValidator<FamilyMemberInfo>
    {
        public FamilyMemberValidator()
        {
            // RuleFor(x => x.Name).NotEmpty().WithMessage("Family member name is required.");

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.RelationId).NotEmpty();
            RuleFor(x => x.NationalityId).NotEmpty();
        }
    }
}