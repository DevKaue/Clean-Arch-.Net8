﻿using Application.UserCQ.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserCQ.Validators
{
    public  class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("O campo 'Email' não pode estar vazio.")
                .EmailAddress().WithMessage("Email inválido");

            RuleFor(x => x.Password).NotEmpty().WithMessage("O campo 'Password' não pode estar vazio.");
        }
    }
}
