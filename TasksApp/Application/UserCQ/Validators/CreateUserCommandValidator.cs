using Application.UserCQ.Commands;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.UserCQ.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() 
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("O campo 'email' não pode ser vazio")
                .EmailAddress().WithMessage("O campo 'email' não é válido.");
            RuleFor(x => x.Username).MinimumLength(1).WithMessage("O campo 'username' não pode estar vazio.");
        }
    }
}
