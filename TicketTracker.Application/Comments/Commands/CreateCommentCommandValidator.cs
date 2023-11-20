using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Comments.Commands
{
    public class CreateCommentCommandValidator :AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(c => c.Message)
                .MaximumLength(1000)
                .NotEmpty()
                .NotNull();
        }
    }
}
