var url='http://localhost:62411/api/Users/Login?credentials=';
$(document).ready(function () {
    console.log(url);
    $(document).on("click", "#LoginBtn", function () {
        var username = $('#userName').val();
        var password = $('#passWord').val();
        var credentials = username + ':' + password;
        console.log(url);
        $.getJSON(url+credentials, function (data) {

            console.log(data.data);



        });
    }); 
});