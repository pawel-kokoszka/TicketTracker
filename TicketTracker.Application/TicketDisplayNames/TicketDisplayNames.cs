using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.Tickets;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.TicketDisplayNames
{
    public class TicketDisplayNames : ITicketDisplayNames
    {
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;

        public TicketDisplayNames(IProjectConfigurationRepository projectConfigurationRepository)
        {
            _projectConfigurationRepository = projectConfigurationRepository;
        }

        public async void AddDisplayNamesToHistoryDetails(List<TicketHistoryDetailDto> historyPropertyDetails)
        {
            foreach (var property in historyPropertyDetails!)
            {
                switch (property.TicketPropertyName)
                {
                    case "TicketSlaConfigurationId":
                        property.TicketPropertyDisplayName = "Priority Level:";

                        var slaPair = await _projectConfigurationRepository.GetTicketSlaForSlaList(new List<int>() { int.Parse(property.PropertyOldValue!), int.Parse(property.PropertyNewValue!) });

                        property.PropertyOldDisplayValue = slaPair.Find(sla => sla.Id == int.Parse(property.PropertyOldValue!))!.Name;
                        property.PropertyNewDisplayValue = slaPair.Find(sla => sla.Id == int.Parse(property.PropertyNewValue!))!.Name;
                        break;

                    case "Description":
                        property.TicketPropertyDisplayName = "Description";

                        property.PropertyOldDisplayValue = property.PropertyOldValue!.ToString();
                        property.PropertyNewDisplayValue = property.PropertyNewValue!.ToString();
                        break;

                    case "ShortDescription":
                        property.TicketPropertyDisplayName = "Short Description";

                        property.PropertyOldDisplayValue = property.PropertyOldValue!.ToString();
                        property.PropertyNewDisplayValue = property.PropertyNewValue!.ToString();
                        break;

                    case "TicketStatusId":
                        property.TicketPropertyDisplayName = "Ticket Status:";

                        var statusPair = await _projectConfigurationRepository.GetTicketStatusesForIdList(new List<int>() { int.Parse(property.PropertyOldValue!), int.Parse(property.PropertyNewValue!) });

                        property.PropertyOldDisplayValue = statusPair.Find(status => status.Id == int.Parse(property.PropertyOldValue!))!.Name;
                        property.PropertyNewDisplayValue = statusPair.Find(status => status.Id == int.Parse(property.PropertyNewValue!))!.Name;
                        break;

                    case "TicketServiceId":
                        property.TicketPropertyDisplayName = "Service:";

                        var servicePair = await _projectConfigurationRepository.GetTicketServicesForIdList(new List<int>() { int.Parse(property.PropertyOldValue!), int.Parse(property.PropertyNewValue!) });

                        property.PropertyOldDisplayValue = servicePair.Find(service => service.Id == int.Parse(property.PropertyOldValue!))!.ServiceName;
                        property.PropertyNewDisplayValue = servicePair.Find(service => service.Id == int.Parse(property.PropertyNewValue!))!.ServiceName;
                        break;

                    case "TicketSubServiceId":
                        property.TicketPropertyDisplayName = "Sub Service:";

                        var subServicePair = await _projectConfigurationRepository.GetTicketSubServicesForIdList(new List<int>() { int.Parse(property.PropertyOldValue!), int.Parse(property.PropertyNewValue!) });

                        property.PropertyOldDisplayValue = subServicePair.Find(subService => subService.Id == int.Parse(property.PropertyOldValue!))!.SubServiceName;
                        property.PropertyNewDisplayValue = subServicePair.Find(subService => subService.Id == int.Parse(property.PropertyNewValue!))!.SubServiceName;
                        break;

                    case "AssignedTeamId":
                        property.TicketPropertyDisplayName = "Team:";

                        var teamPair = await _projectConfigurationRepository.GetTeamsForIdList(new List<int>() { int.Parse(property.PropertyOldValue!), int.Parse(property.PropertyNewValue!) });

                        property.PropertyOldDisplayValue = teamPair.Find(team => team.Id == int.Parse(property.PropertyOldValue!))!.Name;
                        property.PropertyNewDisplayValue = teamPair.Find(team => team.Id == int.Parse(property.PropertyNewValue!))!.Name;
                        break;

                    case "AssignedUserId":
                        property.TicketPropertyDisplayName = "Assigned User:";

                        var userPair = await _projectConfigurationRepository.GetUsersForIdList(new List<string>() { property.PropertyOldValue!, property.PropertyNewValue! });

                        if (property.PropertyOldValue == null)
                        {
                            property.PropertyOldDisplayValue = "Ticket Manager";
                        }
                        else
                        {
                            property.PropertyOldDisplayValue = userPair.Find(user => user.Id == (property.PropertyOldValue))!.Email;

                        }

                        property.PropertyNewDisplayValue = userPair.Find(user => user.Id == (property.PropertyNewValue))!.Email;
                        break;
                }
            }
        }

        public void AddDisplayNamesToHistoryDetail(TicketHistoryDetailDto historyPropertyDetail)
        {
            
                switch (historyPropertyDetail.TicketPropertyName)
                {
                    case "TicketSlaConfigurationId":
                        historyPropertyDetail.TicketPropertyDisplayName = "Priority Level:";

                        var slaPair = _projectConfigurationRepository.GetTicketSlaForSlaList(new List<int>() { int.Parse(historyPropertyDetail.PropertyOldValue!), int.Parse(historyPropertyDetail.PropertyNewValue!) }).Result;

                        historyPropertyDetail.PropertyOldDisplayValue = slaPair.Find(sla => sla.Id == int.Parse(historyPropertyDetail.PropertyOldValue!))!.Name;
                        historyPropertyDetail.PropertyNewDisplayValue = slaPair.Find(sla => sla.Id == int.Parse(historyPropertyDetail.PropertyNewValue!))!.Name;
                        break;

                    case "Description":
                        historyPropertyDetail.TicketPropertyDisplayName = "Description";

                        historyPropertyDetail.PropertyOldDisplayValue = historyPropertyDetail.PropertyOldValue!.ToString();
                        historyPropertyDetail.PropertyNewDisplayValue = historyPropertyDetail.PropertyNewValue!.ToString();
                        break;

                    case "ShortDescription":
                        historyPropertyDetail.TicketPropertyDisplayName = "Short Description";

                        historyPropertyDetail.PropertyOldDisplayValue = historyPropertyDetail.PropertyOldValue!.ToString();
                        historyPropertyDetail.PropertyNewDisplayValue = historyPropertyDetail.PropertyNewValue!.ToString();
                        break;

                    case "TicketStatusId":
                        historyPropertyDetail.TicketPropertyDisplayName = "Ticket Status:";

                        var statusPair =  _projectConfigurationRepository.GetTicketStatusesForIdList(new List<int>() { int.Parse(historyPropertyDetail.PropertyOldValue!), int.Parse(historyPropertyDetail.PropertyNewValue!) }).Result;

                        historyPropertyDetail.PropertyOldDisplayValue = statusPair.Find(status => status.Id == int.Parse(historyPropertyDetail.PropertyOldValue!))!.Name;
                        historyPropertyDetail.PropertyNewDisplayValue = statusPair.Find(status => status.Id == int.Parse(historyPropertyDetail.PropertyNewValue!))!.Name;
                        break;

                    case "TicketServiceId":
                        historyPropertyDetail.TicketPropertyDisplayName = "Service:";

                        var servicePair =  _projectConfigurationRepository.GetTicketServicesForIdList(new List<int>() { int.Parse(historyPropertyDetail.PropertyOldValue!), int.Parse(historyPropertyDetail.PropertyNewValue!) }).Result;

                        historyPropertyDetail.PropertyOldDisplayValue = servicePair.Find(service => service.Id == int.Parse(historyPropertyDetail.PropertyOldValue!))!.ServiceName;
                        historyPropertyDetail.PropertyNewDisplayValue = servicePair.Find(service => service.Id == int.Parse(historyPropertyDetail.PropertyNewValue!))!.ServiceName;
                        break;

                    case "TicketSubServiceId":
                        historyPropertyDetail.TicketPropertyDisplayName = "Sub Service:";

                        var subServicePair =  _projectConfigurationRepository.GetTicketSubServicesForIdList(new List<int>() { int.Parse(historyPropertyDetail.PropertyOldValue!), int.Parse(historyPropertyDetail.PropertyNewValue!) }).Result;

                        historyPropertyDetail.PropertyOldDisplayValue = subServicePair.Find(subService => subService.Id == int.Parse(historyPropertyDetail.PropertyOldValue!))!.SubServiceName;
                        historyPropertyDetail.PropertyNewDisplayValue = subServicePair.Find(subService => subService.Id == int.Parse(historyPropertyDetail.PropertyNewValue!))!.SubServiceName;
                        break;

                    case "AssignedTeamId":
                        historyPropertyDetail.TicketPropertyDisplayName = "Team:";

                        var teamPair = _projectConfigurationRepository.GetTeamsForIdList(new List<int>() { int.Parse(historyPropertyDetail.PropertyOldValue!), int.Parse(historyPropertyDetail.PropertyNewValue!) }).Result;

                        historyPropertyDetail.PropertyOldDisplayValue = teamPair.Find(team => team.Id == int.Parse(historyPropertyDetail.PropertyOldValue!))!.Name;
                        historyPropertyDetail.PropertyNewDisplayValue = teamPair.Find(team => team.Id == int.Parse(historyPropertyDetail.PropertyNewValue!))!.Name;
                        break;

                    case "AssignedUserId":
                        historyPropertyDetail.TicketPropertyDisplayName = "Assigned User:";

                        var userPair = _projectConfigurationRepository.GetUsersForIdList(new List<string>() { historyPropertyDetail.PropertyOldValue!, historyPropertyDetail.PropertyNewValue! }).Result; 

                        if (historyPropertyDetail.PropertyOldValue == null)
                        {
                            historyPropertyDetail.PropertyOldDisplayValue = "Ticket Manager";
                        }
                        else
                        {
                            historyPropertyDetail.PropertyOldDisplayValue = userPair.Find(user => user.Id == (historyPropertyDetail.PropertyOldValue))!.Email;

                        }

                        historyPropertyDetail.PropertyNewDisplayValue = userPair.Find(user => user.Id == (historyPropertyDetail.PropertyNewValue))!.Email;
                        break;
                }
            
        }
    }
}
