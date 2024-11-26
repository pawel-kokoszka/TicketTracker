$(document).ready(function () {


    //LoadCommentsForTicketId();

    $("#AddCommentCollapse form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
                HideCollapse();
                LoadHistoryForTicketId();
                //LoadCommentsForTicketId()
                //toastr["success"]("Comment - added")

                
            },
            error: function () {
                toastr["error"]("Something went wrong")
            }

        })
    });

    

});


////wydziel do oddzielnej funkcji 
//$(document).ready(function () {
//    $("#reloadButton").click(function () {

//        LoadHistoryForTicketId();
        
//    });
//});