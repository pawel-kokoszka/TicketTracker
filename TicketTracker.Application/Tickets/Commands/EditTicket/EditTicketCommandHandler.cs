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
                return Unit.Value;//do zastąpienia przy imp. logowania i eh.
            }

            
            var ticketOryginalData = await _ticketRepository.GetTicketById(request.Id);

            var ticketEditedData = new Ticket(); 

            _mapper.Map(request,ticketEditedData);
                      
            var historyEntry = await _ticketRepository.GetTicketLockByTicketId(request.Id);

            
            var ticketEditedPropertiesList = GetEditedHistoryDetails(ticketOryginalData, ticketEditedData, historyEntry.Id);

            //AddPropertiesDisplayNames
            // tutaj trzaba dodać sprawdzanie czy coś się zmieniło w każdej ticket property i jeśli tak jakoś to mergować 
            //i albo updetować istniejący rekord albo dodawać nowy (pewnie też kaswać poprzedni)

            await _ticketRepository.CreateHistoryDetails(ticketEditedPropertiesList);

            return Unit.Value;
        }



        private List<TicketHistoryDetail> GetEditedHistoryDetails(Ticket oryginalTicket, Ticket editedTicket, int historyEntryId)
        {
            if (oryginalTicket is null || editedTicket is null )
            {
                throw new InvalidOperationException("Extracting New Edited Ticket Data - Operation Failed");
            }
            else
            {
                List<PropertyInfo> ticketProperties = new List<PropertyInfo>(oryginalTicket.GetType().GetProperties());
                List<TicketHistoryDetail> editedTicketProperties = new(); 


                foreach (PropertyInfo property in ticketProperties)
                {

                    object valueOld = property.GetValue(oryginalTicket)!;
                    object valueNew = property.GetValue(editedTicket)!;



                    // rules to skip cases :
                    //#1
                    if (property.Name == "DateCreated" || property.Name == "DateEdited" || property.Name == "EditedByUserId")
                    {
                        continue;
                    }

                    //#2
                    if (valueNew is null) { continue; }//because checked value did not changed
                    
                    //#3
                    if (property.Name == "AssignedUserId" && (valueOld is null && valueNew is not null) )
                    {
                        editedTicketProperties.Add(new TicketHistoryDetail
                        {
                            TicketPropertyName = property.Name,
                            PropertyOldValue = null,
                            PropertyNewValue = property.GetValue(editedTicket)!.ToString(),
                            TicketHistoryId = historyEntryId
                        });
                        continue;
                    }


                    //standard cases 
                    if (!valueOld.Equals(valueNew))
                    {
                        editedTicketProperties.Add( new TicketHistoryDetail 
                        { 
                            TicketPropertyName = property.Name,
                            PropertyOldValue = property.GetValue(oryginalTicket)!.ToString(),
                            PropertyNewValue = property.GetValue(editedTicket)!.ToString(),
                            TicketHistoryId = historyEntryId
                        });
                            
                    }
                }

                return editedTicketProperties;
            }
        }
    }
}
