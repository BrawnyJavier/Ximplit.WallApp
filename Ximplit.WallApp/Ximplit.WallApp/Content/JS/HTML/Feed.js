function FeedLoad() {
    $(document).ready(function () {
        getPosts();
        var credentials;
        // Check if the user is logged in reading the userKey cookie
        var LoggedIn = $.cookie("userKey") ? true : false;
        // If is not, block the textarea used to compose posts
        if (!LoggedIn) {
            $("#PostContent").prop("disabled", true);
            $(".publicCmntBtn").prop("disabled", true);
            $(".CommentsTxtArea").attr('disabled', true);
        }
        else credentials = $.cookie("userKey");
        // Event to handle when user clicks the Post button
        $(document).on("click", "#PostBtn", function () {
            var PostContent = $("#PostContent").val();
            if (PostContent.length > 0) {
                $('#PostBtn').prop('disabled', true);
                $('#PostBtn').html(' <i class="fa fa-spin fa-spinner"></i>');
                var PostModel = {
                    content: PostContent
                }
                $.ajax({
                    url: '/api/Posts/CreatePost',
                    type: "POST",
                    data: PostModel,
                    dataType: 'json',
                    headers: {
                        'Authorization': 'Basic ' + credentials
                    },
                    success: function (data, textStatus, jqXHR) {
                        getPosts();
                        $('#PostBtn').prop('disabled', false);
                        $('#PostBtn').html('POST');
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        swal(
                        'Ooops!!!',
                        'Ha ocurrido un error.',
                        'warning'
                      );
                    }
                });
            }
        });
        // Handle the event thrown when user 'Likes' a post
        $(document).on("click", ".PostLikeLink", function () {
            // Gets the id of the post
            var PostId = $(this).parents().eq(3).prop("id");
            $.ajax({
                url: '/api/Posts/LikePost?postID=' + PostId,
                type: "POST",
                headers: {
                    'Authorization': 'Basic ' + credentials
                },
                success: function (data, textStatus, jqXHR) {

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    swal(
                    'Ooops!!!',
                    'Ha ocurrido un error.',
                    'warning'
                  );
                }
            });
            getPosts();
        });
        // Handle the event thrown when user 'unlikes' a post
        $(document).on("click", ".PostUnlikeLink", function () {
            // Gets the id of the post
            var PostId = $(this).parents().eq(3).prop("id");
            $.ajax({
                url: '/api/Posts/UnLikePost?postID=' + PostId,
                type: "POST",
                headers: {
                    'Authorization': 'Basic ' + credentials
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    swal(
                    'Ooops!!!',
                    'Ha ocurrido un error.',
                    'warning'
                  );
                }
            }).done(function (results) {
                getPosts();
            });
        });
        $(document).on("click", ".CommentLink", function () {
            // Gets the id of the post
            var PostId = $(this).parents().eq(4).prop("id");
            // Gets the comment content
            var CommentContent = $(this).parent().find('.CommentsTxtArea');
            CommentContent.hide();
            console.log(CommentContent);
        });

        // Event fired when an user clicks the button to post a comment
        $(document).on("click", ".publicCmntBtn", function () {

            // Gets the id of the post
            var PostId = $(this).parents().eq(4).prop("id");

            // Gets the comment content
            var CommentContent = $(this).parent().find('.CommentsTxtArea').val();

            var CommentModel = {
                Content: CommentContent,
                PostId: PostId
            };

            $.ajax({
                url: '/api/Comments/CreateComment',
                type: "POST",
                data: CommentModel,
                dataType: 'json',
                headers: {
                    'Authorization': 'Basic ' + credentials
                },
                success: function (data, textStatus, jqXHR) {
                    getPosts();
                    this.prop('disabled', false);
                    this.html('POST');
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
    });
}
function getPosts() {
    var Credentials = $.cookie("userKey");
    if (Credentials) {
        $.ajax({
            url: '/api/Posts/GetPostAndCommentsForLoggedIn',
            type: "GET",
            headers: {
                'Authorization': 'Basic ' + Credentials
            },
            success: function (data, textStatus, jqXHR) {
                RenderHTML(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                swal(
                'Ooops!!!',
                'Ha ocurrido un error.',
                'warning'
              );
            }
        });
    } else {
        var url = '/api/Posts/GetPostAndComments';
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            },
            success: function (Posts) {
                RenderHTML(Posts);
            }
        });
    }
}
function RenderHTML(Posts) {
    // Gets the Handlebars template
    var RawTemplate = $('#PostsTemplate').html();
    var CompiledTemplate = Handlebars.compile(RawTemplate);
    var ourRenderedHTML = CompiledTemplate(Posts);
    var PostsLoad = $('#PostsLoad');
    PostsLoad.html(ourRenderedHTML);
}
