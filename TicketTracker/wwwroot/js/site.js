const RenderComments = (comments, container) => {
    container.empty();

    for (const comment of comments) {
        container.append(
            `<div class="card border-primary mb-3" style="max-width: 18rem;">
                    <div class="card-header">${comment.id}</div>
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
