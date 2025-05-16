using FluentValidation;

namespace PU_Test.Framework.Models.Validator
{
    public class UserResponseValidator : AbstractValidator<UserResponse>
    {
        public UserResponseValidator()
        {
            RuleFor(x => x.name).NotEmpty();
            RuleFor(x => x.email).NotEmpty().EmailAddress();
            RuleFor(x => x.gender).Must(g => g == "male" || g == "female");
            RuleFor(x => x.status).NotEmpty();
        }
    }
}
