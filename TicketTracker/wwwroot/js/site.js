const RenderComments = (comments, container) => {
    container.empty();
    let commentNumber = 1;

    for (const comment of comments) {
        container.prepend(
            
            `<div class="card text-bg-primary mb-2" style="">
                <div class="row card-header pt-2 pb-1 ">
                    <label class="col text-sm">#${commentNumber}    ${comment.createdDate}</label>
      
                    <label class="col text-sm-end">user: ${comment.userName}</label>
        
                </div>
             
                <div class="card-body text-wrap text-break">
                    <p class="card-text">${comment.message.replace(/[\r]/g, '<br>') }</p>
                </div>
             </div>`)
        commentNumber++; 
    }
}


const LoadCommentsForTicketId = () => {
    const container = $("#comments")
    const ticketid = container.data("id");

    $.ajax({
        url: `/TicketTracker/Comments/${ticketid}`,
        type: 'get',
        success: function (data) {
            if (!data.length) {
                container.html("There are no comments for this ticket")
            } else {
                RenderComments(data, container)
                
            }
        },
        error: function () {
            toastr["error"]("Something went wrong during Loading of Comments")
        }

    })

}

const HideCollapse = () => {
    const collapse = $("#newcommentcollapse");

    collapse.collapse('hide');

    const messagetext = $("#messagetext");

    messagetext.val('');

}

const LoadServices = () => {

    const id = $('#TicketTypeConfigurationId').val();
    const selectedserviceid = $("select#TicketServiceId").val();
    //$("select#TicketServiceId").empty();
    //$("select#TicketSubServiceId").empty();
    $.getJSON(`/TicketTracker/GetTicketServices?ticketTypeConfigurationId=${id}`, function (data) {

        if (data && data.length > 0) {
            // Data is not empty, process and append options
            //$("select#TicketServiceId").append(`<option disabled selected >Click to select Service.</option>`);

            $("select#TicketServiceId").empty();

            $.each(data, function (i, service) {

                if (service.id == selectedserviceid) {
                    $("select#TicketServiceId").append(`<option selected value="${service.id}">${service.serviceName}</option>`);

                }
                else {
                    $("select#TicketServiceId").append(`<option value="${service.id}">${service.serviceName}</option>`);
                }
            });



        } else {
            $("select#TicketServiceId").empty();
            $("select#TicketServiceId").append(`<option disabled selected >There is no Service set for this ticket type, please contact Admin</option>`);
        }

        var serviceId = $("select#TicketServiceId").val();

        /*trzeba jakos sprawdzić element który już się wyświetla w ticketServiceID i na jego podstawie wyśiwtlić listę subs tutaj*/


        var selectedService;
        selectedService = data.find(function (service) {
            return service.id == serviceId;
        });


        var selectedSubServiceId = $("select#TicketSubServiceId").val();

        if (data && data.length > 0) {

            $("select#TicketSubServiceId").empty();

            $.each(selectedService.ticketSubServices, function (i, subService) {


                if (subService.id == selectedSubServiceId) {
                    
                    $("select#TicketSubServiceId").append(`<option selected value="${subService.id}">${subService.subServiceName}</option>`);

                }
                else {
                    $("select#TicketSubServiceId").append(`<option value="${subService.id}">${subService.subServiceName}</option>`);
                    
                }

            });
        } else {
            $("select#TicketSubServiceId").empty();
            $("select#TicketSubServiceId").append(`<option disabled selected >There is no Sub-Service set for this ticket type, please contact Admin</option>`);
        }

    });


}