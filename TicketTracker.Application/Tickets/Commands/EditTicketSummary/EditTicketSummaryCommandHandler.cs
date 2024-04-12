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


            //1 dorób mapowanie z TicketEditSummaryDto na ticket z historią 
            //_mapper.Map(request, ticketEditedData);
            // albo jakąś inno metodą 
            //_ticketRepository.MapTicketProperties(ticketEditedData, ticketOryginalData);

            //2 dodaj metodę do zapisywania/mapowania props z historydetails na ticket ----------------------------------------------------
            WriteChangedProperiesToTicket(ticketOryginalData, ticketEditedData.HistoryDetails!);

            await _ticketRepository.SaveToDb();

            return Unit.Value;
        }

        private void WriteChangedProperiesToTicket(Ticket ticketOryginalData, List<TicketHistoryDetail> changedProperties)
        {
            if (ticketOryginalData is null || (changedProperties is null || changedProperties.Count == 0))
            {
                throw new InvalidOperationException("Writing Edited Data to Ticket - Failed");
            }
            else
            {
                List<PropertyInfo> ticketProperties = new List<PropertyInfo>(ticketOryginalData.GetType().GetProperties());

                foreach (TicketHistoryDetail changedProperty in changedProperties) 
                {
                    var propertyToChange = ticketProperties.Find(prop => prop.Name == changedProperty.TicketPropertyName);


                    var propertyToChangeType = propertyToChange!.PropertyType;


                    //do przetestowania 

                    if (propertyToChangeType !=  changedProperty.PropertyNewValue!.GetType()) //czyli nie równa się string bo PropertyNewValue to string
                    {                        

                        int liczba;
 
                        if (int.TryParse(changedProperty.PropertyNewValue, out liczba))
                        {
                            propertyToChange.SetValue(ticketOryginalData, liczba, null);
                        }
                        else
                        {
                            throw new InvalidOperationException("TryParse nie dało rady");
                        }

                        continue;
                    }

                    propertyToChange.SetValue(ticketOryginalData, changedProperty.PropertyNewValue, null);

                }


            }
        }
    }
}
