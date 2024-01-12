var ticketservices;
    $(function () {
        $("select#Projects").change(function () {
            var projectid = $(this).val();

            $("select#Environments").empty();
            $("select#TicketTypes").empty();
            $("select#TicketSlas").empty();
            $("select#TicketServiceId").empty();
            $("select#TicketSubServiceId").empty();

            $.getJSON(`/TicketTracker/GetEnvironments?projectid=${projectid}`, function (data) {
                

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

            $("select#TicketTypes").empty();
            $("select#TicketSlas").empty();
            $("select#TicketServiceId").empty();
            $("select#TicketSubServiceId").empty();

            $.getJSON(`/TicketTracker/GetTicketTypes?projectConfiguratonId=${projectConfiguratonId}`, function (data) {
                

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
                
                
            });
        })
    });

$(function () {
    $("select#TicketSlas").change(function () {

        var id = $("#TicketTypes option:selected").data('ttcid');

        $("select#TicketServiceId").empty();
        $("select#TicketSubServiceId").empty();

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

        //var selectedService;
        //for (var i = 0; i < jsonData.length; i++) {
        //    if (jsonData[i].id == serviceId) {
        //        selectedService = jsonData[i];
        //        break;
        //    }
        //}
        //console.log("selectedService");
        //console.log(selectedService);

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


//<script>
//    $(document).ready(function () {
//        ticketservices = [ /* your JSON data here */ ];

//    // Find the service with "id": 2
//    var selectedService = ticketservices.find(function (service) {
//            return service.id === serviceId;
//        });

 

//    $.each(selectedService.ticketSubServices, function (index, subService) {
//        selectElement.append($('<option>', {
//            value: subService.id,
//            text: subService.subServiceName
//        }));
//            });
//        }
//    });
//</script>









//$(function () {
//    $("select#TicketTypes").change(function () {
//        var id = $(this).val();

//        $("select#TicketSlas").empty();

//        $.getJSON(`/TicketTracker/GetTicketSlas?ticketTypeConfigurationId=${id}`, function (data) {
//            console.log(data);

//            $("select#TicketSlas").append(`<option disabled selected >Click to select SLA.</option>`);

//            $.each(data, function (i, item) {
//                $("select#TicketSlas").append(`<option value="${item.id}">${item.name}</option>`);
//            });
//        });
//    })
//});