$(document).ready(function () {
    var RawCredentials = $.cookie("userKey");
    var credentials = atob(RawCredentials).split(':');
    var UserName = credentials[0];
    var url = '/api/Users/GetUserByUsername?userName=' + UserName;
    // Get the user's data
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        headers: {
            "Content-Type": "application/x-www-form-urlencoded"
        },
        success: function (userData) {
            $('#emailInput').val(userData['email']);
            $('#userName').val(userData['userName']);
            $('#NombreInput').val(userData['name']);
            $('#ApellidoINput').val(userData['lastName']);
        }
    });
    $('#loginform').submit(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        if ($('#passWord').val() == $('#RepeatpassWord').val()) {
            var UserModel = {
                UserName: $('#userName').val(),
                Name: $('#NombreInput').val(),
                LastName: $('#ApellidoINput').val(),
                Password: $('#passWord').val(),
                Email: $('#emailInput').val()
            }
            console.log(UserModel);
            $.ajax({
                url: '/api/Users/UpdateUser',
                type: "POST",
                data: UserModel,
                headers: {
                    'Authorization': 'Basic ' + RawCredentials
                },
                success: function (data, textStatus, jqXHR) {

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    swal('Error');
                }
            }).done(function (data) {
                $("#AjaxLoad").load("/HTML/Login.html", function (responseTxt, statusTxt, xhr) {
                   
                });
            });;
        }
    });
    $(document).on("click", "#ReturnBtn", function () {
        $("#AjaxLoad").load("/HTML/Feed.html", function (responseTxt, statusTxt, xhr) {
            FeedLoad();
        });
        /*
         var Wallap = btoa(UserModel.UserName + ':' + UserModel.Password);
                    $.cookie("userKey", Wallap, { expires: 2 });
                    $("#AjaxLoad").load("/HTML/Feed.html", function (responseTxt, statusTxt, xhr) {
                        //FeedLoad();
                    });
        */
    });
});