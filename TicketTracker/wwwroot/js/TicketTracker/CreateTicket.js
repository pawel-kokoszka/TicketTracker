var ticketservices;

let jsonSlaData;

function calculateFullHoursAndExtraMinutes(minutes) {
    // Calculate the number of full hours
    let hours = Math.floor(minutes / 60);

    // Calculate the number of extra minutes
    let extraMinutes = minutes % 60;

    return { hours, extraMinutes };
}

$(function () {
        $("select#Projects").change(function () {
            var projectid = $(this).val();
            var userid = $("#CreatedByUserId").val();

            $("select#Environments").empty();
            $("select#TicketTypes").empty();
            $("select#TicketSlas").empty();
            $("select#TicketServiceId").empty();
            $("select#TicketSubServiceId").empty();

            $.getJSON(`/TicketTracker/GetEnvironments?projectid=${projectid}&userId=${userid}`, function (data) {
                

                $("select#Environments").append(`<option disabled selected >Click to select Env.</option>`);

                $.each(data, function (i, item) {
                    $("select#Environments").append(`<option value="${item.projectConfigurationId}">${item.name}</option>`);
                });
            });
        })
    });

    $(function () {
        $("select#Environments").change(function () {
            var projectConfiguratonId = $(this).val();
            var userid = $("#CreatedByUserId").val();

            $("#ProjectConfigurationId").val(projectConfiguratonId);

            $("select#TicketTypes").empty();
            $("select#TicketSlas").empty();
            $("select#TicketServiceId").empty();
            $("select#TicketSubServiceId").empty();
            $("#TicketSlaParams").val("");

            $.getJSON(`/TicketTracker/GetTicketTypes?projectConfiguratonId=${projectConfiguratonId}&userId=${userid}`, function (data) {
                

                $("select#TicketTypes").append(`<option disabled selected >Click to select Type.</option>`);

                $.each(data, function (i, item) {
                    $("select#TicketTypes").append(`<option value="${item.id}" data-ttcid="${item.ticketTypeConfigurationId}">${item.typeName}</option>`);
                });
            });
        })
    });

    $(function () {
        $("select#TicketTypes").change(function () {

            var id  = $("#TicketTypes option:selected").data('ttcid');

            $('#TicketTypeConfigurationId').val(id);
            $("select#TicketSlas").empty();
            $("select#TicketServiceId").empty();
            $("select#TicketSubServiceId").empty();
            $("select#AssignedUserId").empty();
            $("#TicketSlaParams").empty();

            $.getJSON(`/TicketTracker/GetTicketSlas?ticketTypeConfigurationId=${id}`, function (data) {

                if (data && data.length > 0) {
                    // Data is not empty, process and append options
                    $("select#TicketSlas").append(`<option disabled selected >Click to select SLA.</option>`);

                    $.each(data, function (i, item) {
                        $("select#TicketSlas").append(`<option value="${item.id}">${item.name}</option>`);
                        
                    });
                } else {
                    $("select#TicketSlas").empty();
                    $("select#TicketSlas").append(`<option disabled selected >There is no SLA set for this ticket type, please contact Admin</option>`);
                }

                jsonSlaData = data;
            });
        })
    });

$(function () {
    $("select#TicketSlas").change(function () {

        var id = $("#TicketTypes option:selected").data('ttcid');

        $("select#TicketServiceId").empty();
        $("select#TicketSubServiceId").empty();

        let priorityId = $(this).val();

        let selectedSlaProp = jsonSlaData.find(function (sla) {
            return sla.id == priorityId;
        });

        if (jsonSlaData && jsonSlaData.length > 0) {
                        
            
            let result = calculateFullHoursAndExtraMinutes(selectedSlaProp.numberOfMinutes);

            if (selectedSlaProp.numberOfDays > 0) {

                if (result.hours > 0) {

                    $("#TicketSlaParams").val(`${selectedSlaProp.numberOfDays} Days, ${result.hours} Hrs and ${result.extraMinutes} Min.`);
                } else {
                    $("#TicketSlaParams").val(`${selectedSlaProp.numberOfDays} Days, ${result.hours} Hrs and ${result.extraMinutes} Min.`);
                }                

            } else {

                if (result.hours > 0) {

                    $("#TicketSlaParams").val(`${result.hours} Hrs and ${result.extraMinutes} min.`);
                } else {
                    $("#TicketSlaParams").val(`${result.extraMinutes} min.`);                    
                }


                
            }

        } 

        //-------------------------------------------------------------------------------------------------------------------
        $.getJSON(`/TicketTracker/GetTicketServices?ticketTypeConfigurationId=${id}`, function (data) {

            if (data && data.length > 0) {
                // Data is not empty, process and append options
                $("select#TicketServiceId").append(`<option disabled selected >Click to select Service.</option>`);

                    

                $.each(data, function (i, item) {
                    $("select#TicketServiceId").append(`<option value="${item.id}">${item.serviceName}</option>`);
                });
            } else {
                $("select#TicketServiceId").empty();
                $("select#TicketServiceId").append(`<option disabled selected >There is no Service set for this ticket type, please contact Admin</option>`);
            }
            jsonData = data;

        });
    })
});

$(function () {
    $("select#TicketServiceId").change(function () {
        const serviceId = $(this).val();

        $("select#TicketSubServiceId").empty();

        var selectedService;
            selectedService = jsonData.find(function (service) {
            return service.id == serviceId;
             });

            if (jsonData && jsonData.length > 0) {
                // Data is not empty, process and append options
                $("select#TicketSubServiceId").append(`<option disabled selected >Click to select Sub-Service.</option>`);

                $.each(selectedService.ticketSubServices, function (i, item) {
                    $("select#TicketSubServiceId").append(`<option value="${item.id}">${item.subServiceName}</option>`);
                });
            } else {
                $("select#TicketSubServiceId").empty();
                $("select#TicketSubServiceId").append(`<option disabled selected >There is no Sub-Service set for this ticket type, please contact Admin</option>`);
            }



    })
});

$(function () {
    $("select#TicketTypes").change(function () {

        var id = $("#TicketTypes option:selected").data('ttcid');
        var userid = $("#CreatedByUserId").val();

        //$("select#AssignedUserId").empty();

        $.get(`/TicketTracker/GetAssigningTeam?ticketTypeConfigurationId=${id}&userId=${userid}`, function (iddata) {


            $("#AssigningTeamId").val(iddata);


        });

        $.getJSON(`/TicketTracker/GetTeamsToAssign?ticketTypeConfigurationId=${id}&userId=${userid}`, function (tdata) {

            if (tdata && tdata.length > 0) {
                // Data is not empty, process and append options
                $("select#AssignedUserId").append(`<option disabled selected >Click to assign Team Member.</option>`);

                let teamId = 0;

                const noUser = 0;

                const teamOnlyMessage = "TICKET MANAGER";

                $.each(tdata, function (i, item) {
                    if (teamId == 0) {
                        teamId = item.teamId;
                        $("select#AssignedUserId").append(`<option value="${noUser}" data-assignedteamid=${item.teamId} >${item.teamName} | ${teamOnlyMessage}</option>`);
                    }

                    if (teamId == item.teamId) {
                        $("select#AssignedUserId").append(`<option value="${item.userId}" data-assignedteamid=${item.teamId} >${item.teamName} | ${item.userEmail}</option>`);
                    } else {
                        teamId = item.teamId;
                        $("select#AssignedUserId").append(`<option value="${noUser}" data-assignedteamid=${item.teamId} >${item.teamName} | ${teamOnlyMessage}</option>`);
                        $("select#AssignedUserId").append(`<option value="${item.userId}" data-assignedteamid=${item.teamId} >${item.teamName} | ${item.userEmail}</option>`);                        
                    }

                });
            } else {
                $("select#AssignedUserId").empty();
                $("select#AssignedUserId").append(`<option disabled selected >There is no Team set for this ticket type, please contact Admin</option>`);
            }


        });
    })
});
$(function () {
    $("select#AssignedUserId").change(function () {

        var teamid = $("#AssignedUserId option:selected").data('assignedteamid');



        $("#AssignedTeamId").val(teamid);


    });
});
