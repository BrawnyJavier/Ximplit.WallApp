function FeedLoad() {
    $(document).ready(function () {
        // Check if the user is logged in reading the userKey cookie
        var LoggedIn = $.cookie("userKey") ? true : false;
        // If is not, block the textarea used to compose posts
        if (!LoggedIn) $("#PostContent").prop("disabled", true);
        getPosts();
        // Event to handle when user clicks the Post button
        $(document).on("click", "#PostBtn", function () {
            var commentContent = $("#PostContent").val();

            if (commentContent.length > 0) {
                $('#PostBtn').prop('disabled', true);
                $('#PostBtn').html(' <i class="fa fa-spin fa-spinner"></i>');
                var PostModel = {
                    content: commentContent
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
                        console.log('Authorization' + $.cookie("userKey"));
                    }
                });
            }

        });
        $(document).on("click", ".publicCmntBtn", function () {
            console.log(this);
        });



    });
}
function getPosts() {
    var url = '/api/Posts/GetPostAndComments';
    //$.getJSON(url, function (result) { console.log(result); });
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        success: function (Posts) {
            console.log(Posts);

            for (var post in Posts) {
                console.log(post['author']);
            }
            RenderHTML(Posts);
        }
    });
}
function RenderHTML(Posts) {
    var RawTemplate = $('#PostsTemplate').html();
    var CompiledTemplate = Handlebars.compile(RawTemplate);
    var ourRenderedHTML = CompiledTemplate(Posts);
    var PostsLoad = $('#PostsLoad');
    PostsLoad.html(ourRenderedHTML);
}
function HaceCuanto(fecha) {

    var segundos = Math.floor((new Date() - new Date(fecha)) / 1000);

    var interval = Math.floor(segundos / 31536000);

    if (interval > 1) {
        return interval + " años";
    }
    interval = Math.floor(segundos / 2592000);
    if (interval > 1) {
        return interval + " meses";
    }
    interval = Math.floor(segundos / 86400);
    if (interval > 1) {
        return interval + " días";
    }
    interval = Math.floor(segundos / 3600);
    if (interval > 1) {
        return interval + " horas";
    }
    interval = Math.floor(segundos / 60);
    if (interval > 1) {
        return interval + " minutos";
    }
    return Math.floor(segundos) + " segundos";
}