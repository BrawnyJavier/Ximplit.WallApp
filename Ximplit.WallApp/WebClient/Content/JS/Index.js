function searchfn() {
    //Disable textbox to prevent multiple submit
    var dir = 'http://aylmaoo.com/Posts/?s=';
    $(this).attr("disabled", "disabled");
    var keyWords = $('#userinput').val();
    console.log(keyWords[0]);
    var SearchTerms = keyWords.split(" ");
    for (var i = 0; i < SearchTerms.length ; i++) {
        dir = dir + SearchTerms[i];
        if (!(i == SearchTerms.length - 1)) {
            dir = dir + '+';
        }
    }
    dir = dir + '&submit=Search';
    window.location = dir;
}
$(document).ready(function () {
    $('.modal').modal();
    $(document).delegate('.open', 'click', function (event) {
        $(this).addClass('oppenned');
        event.stopPropagation();
    })
    $(document).delegate('body', 'click', function (event) {
        $('.open').removeClass('oppenned');
    })
    $(document).delegate('.cls', 'click', function (event) {
        $('.open').removeClass('oppenned');
        event.stopPropagation();
    });
    $(".button-collapse").sideNav();
    $("#menuIcon").click(function () {
        $('.button-collapse').sideNav('show');
    });

    // For image upload
    $('.materialboxed').materialbox();
    $('select').material_select();
    $(document).on('change', '.btn-file :file', function () {
        var input = $(this),
			label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [label]);
    });

    $('.btn-file :file').on('fileselect', function (event, label) {

        var input = $(this).parents('.input-group').find(':text'), log = label;

        if (input.length) {
            input.val(log);
        } else {
            if (log) alert(log);
        }
    });
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#img-upload').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
    $("#imgInp").change(function () {
        readURL(this);
    });
    // Image upload end






});
