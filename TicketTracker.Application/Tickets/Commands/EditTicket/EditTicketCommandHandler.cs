using AutoMapper;
using MediatR;
using System.Reflection;
using TicketTracker.Application.ApplicationUser;
using TicketTracker.Application.TicketRoles;
using TicketTracker.Domain.Entities;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Commands.EditTicket
{
    public class EditTicketCommandHandler : IRequestHandler<EditTicketCommand>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public EditTicketCommandHandler(ITicketRepository ticketRepository, IUserContext userContext, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(EditTicketCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("App User")))
            {
                return Unit.Value;
            }

            
            var ticketOryginalData = await _ticketRepository.GetTicketById(request.Id);

            var ticketEditedData = new Ticket(); 

            _mapper.Map(request,ticketEditedData);

            ticketEditedData.DateEdited = DateTime.UtcNow;            
            ticketEditedData.EditedByUserId = currentUser.Id;

            var ticketEditedPropertiesList = UpdateEditedTicketProperties(ticketOryginalData, ticketEditedData);

            _ticketRepository.MapTicketProperties(ticketEditedData, ticketOryginalData);


            await _ticketRepository.SaveToDb();

            return Unit.Value;
        }

        private List<string> UpdateEditedTicketProperties(Ticket oryginalTicket, Ticket editedTicket)
        {
            if (oryginalTicket is null || editedTicket is null )
            {
                throw new InvalidOperationException("Extracting New Edited Ticket Data - Operation Failed");
            }
            else
            {
                List<PropertyInfo> ticketProperties = new List<PropertyInfo>(oryginalTicket.GetType().GetProperties());
                List<string> editedTicketProperties = new List<string>();


                foreach (PropertyInfo property in ticketProperties)
                {

                    object valueOld = property.GetValue(oryginalTicket)!;
                    object valueNew = property.GetValue(editedTicket)!;

                    if (valueNew is null) { continue; }

                    if (!valueOld.Equals(valueNew))
                    {
                        editedTicketProperties.Add($"NAZWA: {property.Name} | Wartość: {property.GetValue(editedTicket)}");
                    }
                }

                return editedTicketProperties;
            }
        }


        //public async Task<Unit> Handle(EditTicketCommand request, CancellationToken cancellationToken)
        //{
        //    var currentUser = _userContext.GetCurrentUser();
        //    if (currentUser == null || (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("App User")))
        //    {
        //        return Unit.Value;
        //    }


        //    //  1 pobieram org tt z db 
        //    var ticket = await _ticketRepository.GetTicketById(request.Id);
        //    //  2 spr. jakie zmiany zostały zrobione między orgTT a editedTT i zapamiętuje jakoś te zmiany 

        //    //  3 zapisuje editedTT w miejsce orgTT, a znalezione zmiany do tabeli z ttHistory


        //    _mapper.Map(request, ticket);

        //    //_mapper.Map<Domain.Entities.Ticket>(request);            
        //    //var ticketNew = _mapper.Map<Domain.Entities.Ticket>(request);

        //    ticket.EditedByUserId = currentUser.Id;
        //    ticket.DateEdited = DateTime.UtcNow;

        //    List<PropertyInfo> ticketProperties = new List<PropertyInfo>(ticket.GetType().GetProperties());

        //    foreach (PropertyInfo property in ticketProperties)
        //    {

        //    }


        //    await _ticketRepository.SaveToDb();

        //    return Unit.Value;
        //}
    }
}
