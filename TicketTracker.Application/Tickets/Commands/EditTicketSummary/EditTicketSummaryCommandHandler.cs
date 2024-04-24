using AutoMapper;
using MediatR;
using System.Reflection;
using TicketTracker.Application.ApplicationUser;
using TicketTracker.Domain.Entities;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Commands.EditTicketSummary
{
    internal class EditTicketSummaryCommandHandler : IRequestHandler<EditTicketSummaryCommand>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public EditTicketSummaryCommandHandler(ITicketRepository ticketRepository, IUserContext userContext, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditTicketSummaryCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("App User")))
            {
                return Unit.Value;//do zastąpienia przy imp. logowania i eh.
            }

            var ticketOryginalData = await _ticketRepository.GetTicketById(request.Id);

            var ticketEditedData = request.TicketHistory;


            var numberOfChangedProperties = WriteChangedProperiesToTicket(ticketOryginalData, ticketEditedData.HistoryDetails!);
            if (numberOfChangedProperties == 0)
            {
                return Unit.Value;
            }


            var currentHistoryEntry = await _ticketRepository.GetTicketHistoryEntryByLockIdAndTicketId(request.Id);
            currentHistoryEntry.SummaryComment = request.TicketHistory.SummaryComment;
            currentHistoryEntry.IsApproved = true;

            await _ticketRepository.UpdateHistoryEntry(currentHistoryEntry); 

            await _ticketRepository.SaveToDb();

            return Unit.Value;
        }

        private int WriteChangedProperiesToTicket(Ticket ticketOryginalData, List<TicketHistoryDetailDto> changedProperties)
        {
            int numberOfChanges = 0;

            if (ticketOryginalData is null)
            {
                throw new InvalidOperationException("Writing Edited Data to Ticket - Failed");
            }
            else
            {
                if (changedProperties is null || changedProperties.Count == 0)
                {
                    return 0;
                }
                else
                {
                    List<PropertyInfo> ticketProperties = new List<PropertyInfo>(ticketOryginalData.GetType().GetProperties());

                    foreach (TicketHistoryDetailDto changedProperty in changedProperties) 
                    {
                        numberOfChanges++;

                        var propertyToChange = ticketProperties.Find(prop => prop.Name == changedProperty.TicketPropertyName);

                        var propertyToChangeType = propertyToChange!.PropertyType;

                        if (propertyToChangeType !=  changedProperty.PropertyNewValue!.GetType()) //czyli nie równa się string bo PropertyNewValue to string
                        {                        

                            int liczba;
 
                            if (int.TryParse(changedProperty.PropertyNewValue, out liczba))
                            {
                                propertyToChange.SetValue(ticketOryginalData, liczba, null);
                            }
                            else
                            {
                                throw new InvalidOperationException("TryParse from string to int failure");
                            }

                            continue;
                        }

                        propertyToChange.SetValue(ticketOryginalData, changedProperty.PropertyNewValue, null);
                    }
                }


            }

            return numberOfChanges;
        }
    }
}
