function FeedLoad() {
    $(document).ready(function () {
        // Check if the user is logged in reading the userKey cookie
        var LoggedIn = $.cookie("userKey") ? true : false;
        // If is not, block the textarea used to compose posts
        if (!LoggedIn) $("#PostContent").prop("disabled", true);
        // Event to handle when user clicks the Post button
        $(document).on("click", "#PostBtn", function () {
            var PostModel = {
                content: $("#PostContent").val()
            }
            $.ajax({
                url: '/api/Posts/CreatePost',
                type: "POST",
                data: PostModel,
                dataType: 'json',
                headers: {
                    'Authorization': $.cookie("userKey")
                },
                success: function (data, textStatus, jqXHR) {
                    swal('success');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    swal(
                    'Ooops!!!',
                    'Ha ocurrido un error.',
                    'warning'
                  );
                }
            });
        });
        $(document).on("click", "#PublishEntryBtn", function () {
            alert("inside");
        });



    });
}