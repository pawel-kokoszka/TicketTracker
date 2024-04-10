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

            ticketEditedData.DateEdited = DateTime.UtcNow;            
            ticketEditedData.EditedByUserId = currentUser.Id;

            //1 pobieramy history entry po lockID i ttID 
            //2 sprawdzamy które prop w tt zostały zmienione jeśli nic nie zostało zmienione nie wprowadzamy żadnych zman
            //  i trzeba userowi jakoś przekazać że nic nie zmienił 
            //3 zapisujemy zmienione prop jako odzielne rekordy w history detalis 
            //4 w akcji edit summary tworzymy widok z EditsummaryCommand lub redirect to EditSummary(właściwą/oddzielną scieżką) action 
            //5 na widoku są widoczne nie edytowalne informacje i lista zmian oraz pole z komentarzem na podsumowanie

            //ad1
            var historyEntry = await _ticketRepository.GetTicketLockByTicketId(request.Id);



            //ad2
            var ticketEditedPropertiesList = GetEditedHistoryDetails(ticketOryginalData, ticketEditedData, historyEntry.Id);


            //ad3            
           await _ticketRepository.CreateHistoryDetails(ticketEditedPropertiesList);


            //zmień mapticket.. żeby zmiany zapisywała do tabeli tymaczowej z edtytowanymi zmianami 
            _ticketRepository.MapTicketProperties(ticketEditedData, ticketOryginalData);

            //zamiast zapisywać ticketOryginalData to trzeba zapisać do tymaczsowej tabeli 
            await _ticketRepository.SaveToDb();

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

                    if (valueNew is null) { continue; }

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

    //0327 kopia z momentu kiedy handler zapisywał zmiany odrazu do tabeli tickets 
    //public class EditTicketCommandHandler : IRequestHandler<EditTicketCommand>
    //{
    //    private readonly ITicketRepository _ticketRepository;
    //    private readonly IUserContext _userContext;
    //    private readonly IMapper _mapper;

    //    public EditTicketCommandHandler(ITicketRepository ticketRepository, IUserContext userContext, IMapper mapper)
    //    {
    //        _ticketRepository = ticketRepository;
    //        _userContext = userContext;
    //        _mapper = mapper;
    //    }
    //    public async Task<Unit> Handle(EditTicketCommand request, CancellationToken cancellationToken)
    //    {
    //        var currentUser = _userContext.GetCurrentUser();
    //        if (currentUser == null || (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("App User")))
    //        {
    //            return Unit.Value;//do zastąpienia przy imp. logowania i eh.
    //        }


    //        var ticketOryginalData = await _ticketRepository.GetTicketById(request.Id);

    //        var ticketEditedData = new Ticket();

    //        _mapper.Map(request, ticketEditedData);

    //        ticketEditedData.DateEdited = DateTime.UtcNow;
    //        ticketEditedData.EditedByUserId = currentUser.Id;

    //        //1 pobieramy history entry po lockID i ttID 
    //        //2 sprawdzamy które prop w tt zostały zmienione jeśli nic nie zostało zmienione nie wprowadzamy żadnych zman
    //        //  i trzeba userowi jakoś przekazać że nic nie zmienił 
    //        //3 zapisujemy zmienione prop jako odzielne rekordy w history detalis 
    //        //4 w akcji edit summary tworzymy widok z EditsummaryCommand lub redirect to EditSummary(właściwą/oddzielną scieżką) action 
    //        //5 na widoku są widoczne nie edytowalne informacje i lista zmian oraz pole z komentarzem na podsumowanie

    //        //ad2 var ticketEditedPropertiesList = UpdateEditedTicketProperties(ticketOryginalData, ticketEditedData);

    //        _ticketRepository.MapTicketProperties(ticketEditedData, ticketOryginalData);


    //        await _ticketRepository.SaveToDb();

    //        return Unit.Value;
    //    }

    //    private List<string> UpdateEditedTicketProperties(Ticket oryginalTicket, Ticket editedTicket)
    //    {
    //        if (oryginalTicket is null || editedTicket is null)
    //        {
    //            throw new InvalidOperationException("Extracting New Edited Ticket Data - Operation Failed");
    //        }
    //        else
    //        {
    //            List<PropertyInfo> ticketProperties = new List<PropertyInfo>(oryginalTicket.GetType().GetProperties());
    //            List<string> editedTicketProperties = new List<string>();


    //            foreach (PropertyInfo property in ticketProperties)
    //            {

    //                object valueOld = property.GetValue(oryginalTicket)!;
    //                object valueNew = property.GetValue(editedTicket)!;

    //                if (valueNew is null) { continue; }

    //                if (!valueOld.Equals(valueNew))
    //                {
    //                    editedTicketProperties.Add($"NAZWA: {property.Name} | Wartość: {property.GetValue(editedTicket)}");
    //                }
    //            }

    //            return editedTicketProperties;
    //        }
    //    }


    //}
}
