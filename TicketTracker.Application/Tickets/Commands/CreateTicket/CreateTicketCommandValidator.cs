﻿using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Tickets.Commands.CreateTicket;

namespace TicketTracker.Application.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketCommandValidator()
        {
            RuleFor(t => t.Description)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Description should have at least 3 characters.")
                .MaximumLength(1000).WithMessage("Description should have maximum of 1000 characters.");

            RuleFor(t => t.ShortDescription)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Short Description should have at least 3 characters.")
                .MaximumLength(100).WithMessage("Short Description should have maximum of 100 characters.");
        }
    }
}
