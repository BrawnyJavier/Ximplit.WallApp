var url = '/api/Users/Login?credentials=';
$(document).ready(function () {
    $('.modal').modal();
    $("#loginform").submit(function () {
        return false;
    });
    $(document).on("click", "#LoginBtn", function () {
        var username = $('#userName').val();
        var password = $('#passWord').val();
        var credentials = username + '|' + password;
        $.getJSON(url + credentials, function (result) {
            if (result == true) {
                /* If the user exist in the database, we store his credentials 
                in a cookie to persist the data 
                */
                var Wallap = btoa(username + ':' + password);
                $.cookie("userKey", Wallap, { expires: 2 });
                $("#AjaxLoad").load("/HTML/Feed.html", function (responseTxt, statusTxt, xhr) {
                    FeedLoad();
                });
            } else {
                swal(
                      'Credenciales incorrectas.',
                      '¿Estás seguro que esa es tu contraseña?',
                      'warning'
                    );
            }
        });
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
                url: '/api/Users/CreateUser',
                type: "POST",
                data: UserModel,
                success: function (data, textStatus, jqXHR) {
                    var Wallap = btoa(UserModel.UserName + ':' + UserModel.Password);
                    $.cookie("userKey", Wallap, { expires: 2 });
                    $("#AjaxLoad").load("/HTML/Feed.html", function (responseTxt, statusTxt, xhr) {
                        FeedLoad();
                        $("#SignOutBtn").show();
                    });
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    swal('error');
                }
            });
        }

    });
    $(document).on("click", "#ReturnBtn", function () {
        $("#AjaxLoad").load("/HTML/Login.html", function (responseTxt, statusTxt, xhr) {

        });
    });
});