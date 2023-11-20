const RenderComments = (comments, container) => {
    container.empty();

    for (const comment of comments) {
        container.append(
            `<div class="card text-bg-primary mb-2" style="">
                <div class="row card-header pt-2 pb-1 ">
                    <label class="col text-sm">#${comment.id} 2023-11-23 11:23</label>
      
                    <label class="col text-sm-end">user: pawel.kokoszka@test.pl</label>
        
                </div>
             
                <div class="card-body">
                    <p class="card-text">${comment.message}</p>
                </div>
             </div>`)
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
