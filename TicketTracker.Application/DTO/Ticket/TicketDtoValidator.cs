using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.DTO.Ticket
{
    public class TicketDtoValidator : AbstractValidator<TicketDto>
    {
        public TicketDtoValidator() 
        {
            RuleFor(t => t.Description)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Description should have at least 3 characters.")
                .MaximumLength(100).WithMessage("Description should have maximum of 100 characters.");
        }
    }
}
