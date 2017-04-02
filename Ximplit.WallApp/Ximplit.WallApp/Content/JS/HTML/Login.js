var url = '/api/Users/Login?credentials=';
$(document).ready(function () {
    $('.modal').modal();
    $("#loginform").submit(function () {
        return false;
    });
    console.log(url);
    $(document).on("click", "#LoginBtn", function () {
        $('#LoginBtn').prop('disabled', true);
        $('#LoginBtn').html(' <i class="fa fa-spin fa-spinner"></i>');
        var username = $('#userName').val();
        var password = $('#passWord').val();
        var credentials = username + '|' + password;
        console.log(url + credentials);
        //alert('d');
        $.getJSON(url + credentials, function (result) {
            if (result == true) {
                /* If the user exist in the database, we store his credentials 
                in a cookie to persist the data */
                var Wallap = btoa(username + ':' + password);
                $.cookie("userKey", Wallap, { expires: 2 });
                $("#SignOutBtn").show();
                $("#AjaxLoad").load("/HTML/Feed.html", function (responseTxt, statusTxt, xhr) {
                    FeedLoad();
                    $("#SignOutBtn").show();
                });
            } else {
                // swal('¡Credenciales incorrectas!', '¿Estás seguro que ese es tu usuario y esa es tu contraseña?', 'warning');
                swal({
                    title: '¡Credenciales incorrectas!',
                    text: "¿Estás seguro que ese es tu usuario y esa es tu contraseña?",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Enviar contraseña por email'
                }).then(function () {
                    var url = '/api/Users/sendPassword?username=' + username;
                    $.ajax({
                        url: url,
                        type: 'GET',
                        dataType: 'json',
                        success: function (data, textStatus, xhr) {
                            if (data == true) {
                                swal(
                                  'Credenciales enviadas.',
                                  'Revisa tu email, te hemos enviado tus datos para que puedas iniciar sesión.',
                                  'success'
                                );
                            } else {
                                swal(
                                    'Hmm, al parecer este usuario no existe',
                                  'Por favor, intentalo una vez más.',
                                  'warning'
                                  );
                            }

                        }
                    });



                })
                $('#LoginBtn').prop('disabled', false);
                $('#LoginBtn').html('INICIAR SESIÓN');
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