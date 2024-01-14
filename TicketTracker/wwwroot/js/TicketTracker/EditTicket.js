

$(function () {

    $("select#TicketServiceId").change(function () {
        var id = $('#TicketTypeConfigurationId').val();
        var selectedserviceid = $("select#TicketServiceId").val();
        //$("select#TicketServiceId").empty();
        //$("select#TicketSubServiceId").empty();
        $.getJSON(`/TicketTracker/GetTicketServices?ticketTypeConfigurationId=${id}`, function (data) {

            if (data && data.length > 0) {

                $("select#TicketSubServiceId").empty();

                var selectedService;
                selectedService = data.find(function (service) {
                    return service.id == selectedserviceid;
                });

                if (data && data.length > 0) {
                    // Data is not empty, process and append options
                    $("select#TicketSubServiceId").append(`<option disabled selected >Click to select Sub-Service.</option>`);

                    $.each(selectedService.ticketSubServices, function (i, item) {
                        $("select#TicketSubServiceId").append(`<option value="${item.id}">${item.subServiceName}</option>`);
                    });
                } else {
                    $("select#TicketSubServiceId").empty();
                    $("select#TicketSubServiceId").append(`<option disabled selected >There is no Sub-Service set for this ticket type, please contact Admin</option>`);
                }

               
            } else {
                $("select#TicketSubServiceId").empty();
                $("select#TicketSubServiceId").append(`<option disabled selected >There is no SubService set for this Service type, please contact Admin</option>`);
            }
            

        });

    });
});

/*-------------------------------------------------------------------*/

//$("select#TicketServiceId").change(function () {
//    // Get the selected value
//    var selectedValue = $(this).val();

//    // Perform actions based on the selected value
//    if (selectedValue === "1") {
//        // Do something when the value is 1
//        console.log("Selected value is 1");
//    } else if (selectedValue === "2") {
//        // Do something when the value is 2
//        console.log("Selected value is 2");
//    } else {
//        // Handle other cases
//        console.log("Selected value is something else");
//    }
//});


///*-------------------------------------------------------------------*/
//$(function () {
//    $("select#TicketServiceId").change(function () {
//        var serviceId = $(this).val();

//        /*trzeba jakos sprawdziæ element który ju¿ siê wyœwietla w ticketServiceID i na jego podstawie wyœiwtliæ listê subs tutaj*/

//        $("select#TicketSubServiceId").empty();

//        var selectedService;
//            selectedService = jsonData.find(function (service) {
//            return service.id == serviceId;
//        });

//            if (jsonData && jsonData.length > 0) {
//                // Data is not empty, process and append options
//                //$("select#TicketSubServiceId").append(`<option disabled selected >Click to select Sub-Service.</option>`);

//                $.each(selectedService.ticketSubServices, function (i, item) {
//                    $("select#TicketSubServiceId").append(`<option value="${item.id}">${item.subServiceName}</option>`);
//                });
//            } else {
//                $("select#TicketSubServiceId").empty();
//                $("select#TicketSubServiceId").append(`<option disabled selected >There is no Sub-Service set for this ticket type, please contact Admin</option>`);
//            }
            

        
//    })
//});


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