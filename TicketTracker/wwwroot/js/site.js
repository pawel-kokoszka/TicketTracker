const RenderComments = (comments, container) => {
    container.empty();
    let commentNumber = 1;

    for (const comment of comments) {
        container.append(
            
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

