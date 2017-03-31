var url = '/api/Users/Login?credentials=';
$(document).ready(function () {
    $('.modal').modal();
    $("#loginform").submit(function () {
        return false;
    });
    console.log(url);
    $(document).on("click", "#LoginBtn", function () {
        var username = $('#userName').val();
        var password = $('#passWord').val();
        var credentials = username + '|' + password;
        console.log(url + credentials);
        //alert('d');
        $.getJSON(url + credentials, function (result) {
            if (result == true) {
                /* If the user exist in the database, we store his credentials 
                in a cookie to persist the data 
                */
                var Wallap = btoa(username+':'+password);
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
    $(document).on("click", "#GuestLoginBtn", function () {
        $("#AjaxLoad").load("/HTML/Feed.html", function (responseTxt, statusTxt, xhr) {
            FeedLoad();
        });
    });
    $(document).on("click", "#createAccount", function () {
        $("#AjaxLoad").load("/HTML/SignUp.html", function (responseTxt, statusTxt, xhr) {
        
        });
    });
});