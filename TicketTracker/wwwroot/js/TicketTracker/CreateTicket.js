
    $(function () {
        $("select#Projects").change(function () {
            var projectid = $(this).val();

            $("select#Environments").empty();
            $("select#TicketTypes").empty();
            $("select#TicketSlas").empty();

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