$(document).ready(function () {


    LoadCommentsForTicketId();

    $("#AddCommentCollapse form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {

                LoadCommentsForTicketId()
                toastr["success"]("Comment - added")
                HideCollapse()

                
            },
            error: function () {
                toastr["error"]("Something went wrong")
            }

        })
    });


});