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

            //extracted edited ticket properties entered by user on edit ticket form - not yet saved in DB 
            var newEditedProperties = ExtractEditedHistoryDetails(ticketOryginalData, ticketEditedData, historyEntry.Id);


            //already edited ticket properties, that are saved in DB and are related to current ticket lockId
            var alreadyEditedPropertiesInSession = await _ticketRepository.GetUnsavedTicketProperties(historyEntry.Id);

            if (alreadyEditedPropertiesInSession.Count == 0)
            {
                await _ticketRepository.CreateHistoryDetails(newEditedProperties);
            }
            else
            {
                RemoveDuplicates(alreadyEditedPropertiesInSession, newEditedProperties);
                //porównać czy w ticketEditedPropertiesList znajduje się jakieś alreadyEditedPropertiesInSession
                //jeśli tak to usunąć je z ticketEditedPropertiesList
                await _ticketRepository.CreateHistoryDetails(newEditedProperties);

                //spr. czt w alreadyEditedPropertiesInSession któryś element został przesawiony na IsDiscarded == true

                var isChanged = alreadyEditedPropertiesInSession.Any(thd => thd.IsDiscarded == true);

                if (isChanged == true)
                {
                    //wykonaj update na ticekt history detail użwyając alreadyEditedPropertiesInSession
                    await _ticketRepository.SaveChangesToAlredyEditedHistoryDetails(alreadyEditedPropertiesInSession);
                }

                //if (alreadyEditedPropertiesInSession.Count != 0) 
                //{
                //    await _ticketRepository.DiscardHistoryDetails(alreadyEditedPropertiesInSession);
                //}

            }

            //AddPropertiesDisplayNames
            // tutaj trzaba dodać sprawdzanie czy coś się zmieniło w każdej ticket property i jeśli tak jakoś to mergować 
            //i albo updetować istniejący rekord albo dodawać nowy (pewnie też kaswać poprzedni)


            return Unit.Value;
        }

        private void RemoveDuplicates(List<TicketHistoryDetail> alreadyEditedPropertiesInSession, List<TicketHistoryDetail> newEditedPropertiesList)
        {
            foreach (var alreadyEdited in alreadyEditedPropertiesInSession)
            {
                foreach (var newlyEdited in newEditedPropertiesList)
                {
                    if (alreadyEdited.TicketPropertyName == newlyEdited.TicketPropertyName)
                    {
                        //możliwe przypadki
                        //1. nowe i stare property  jest takie samo - skasuj nowe
                        //2. nowe i stare różnią się - skasuj stare 

                        if (newlyEdited.PropertyOldValue == alreadyEdited.PropertyOldValue && newlyEdited.PropertyNewValue != alreadyEdited.PropertyNewValue)
                        {                            
                            //discard or delete alreadyEdited from DB
                            alreadyEdited.IsDiscarded = true;

                            continue;
                        }
                        if (newlyEdited.PropertyOldValue == alreadyEdited.PropertyOldValue && newlyEdited.PropertyNewValue == alreadyEdited.PropertyNewValue)
                        {                        
                            //discard in newlyEdited or remove it from the list 
                            newEditedPropertiesList.Remove(newlyEdited);

                            continue;
                        }
                        if (newlyEdited.PropertyOldValue != alreadyEdited.PropertyOldValue)
                        {
                            throw new InvalidOperationException("Checking Ticket Edited Proprties (PropertyOldValue Inconsistency) Invalid Operation");
                        }
                    }
                }
            }
        }

        private List<TicketHistoryDetail> ExtractEditedHistoryDetails(Ticket oryginalTicket, Ticket editedTicket, int historyEntryId)
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
