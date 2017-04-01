$(document).ready(function () {
    // Check if the user is logged in reading the userKey cookie
    var LoggedIn = $.cookie("userKey") ? true : false;
    // If is not, block the textarea used to compose posts
    if (LoggedIn) {
        $("#SignOutBtn").show();
        $("#AjaxLoad").load("/HTML/Feed.html", function () { FeedLoad() });
    } 
    else {
        $("#SignOutBtn").hide();
        $("#AjaxLoad").load("/HTML/Login.html");        
    }
    $(document).on("click", "#SignOutBtn", function () {
        $.removeCookie("userKey");
        $("#SignOutBtn").hide();
        $("#AjaxLoad").load("/HTML/Login.html");
    });
});