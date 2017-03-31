var url;
$(document).ready(function () {
    //$("#AjaxLoad").load("/HTML/Feed.html", function (responseTxt, statusTxt, xhr) {
    //    //if (statusTxt == "success")
    //    //    alert("External content loaded successfully!");
    //    //if (statusTxt == "error")
    //    //    alert("Error: " + xhr.status + ": " + xhr.statusText);
    //});
    $("#AjaxLoad").load("/HTML/Login.html", function (responseTxt, statusTxt, xhr) {
        //if (statusTxt == "success")
        //    alert("External content loaded successfully!");
        //if (statusTxt == "error")
        //    alert("Error: " + xhr.status + ": " + xhr.statusText);
    });
});