function FeedLoad() {
    $(document).ready(function () {
        var asGuest = $.cookie("userKey") ? true : false;
        console.log(asGuest);
        if (asGuest) swal('usuario');

        $(document).on("click", "#PublishEntryBtn", function () {
            alert("inside");
        });
    });
}