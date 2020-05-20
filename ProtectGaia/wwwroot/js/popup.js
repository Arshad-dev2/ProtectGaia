
var elem = $('#isFirstTimeLogin').val();
$(function () {
    if (elem == "True") {
        $('#firstTimeModal').show();
    }
    else {
        $('#firstTimeModal').hide();
    }

    if ($("#taskPopupModal").length) {

        $("#taskPopupModal").show();

    }
    else if ($('#levelPopupModal').length) {
        $("#levelPopupModal").show();
    }
})

$('.close').click(function () {
    if ($("#firstTimeModal").length) {
        $('#firstTimeModal').hide();
    }
   if ($("#taskPopupModal").length) {
        $("#taskPopupModal").hide();
    }
   if ($("#levelPopupModal").length) {
        $("#levelPopupModal").hide();
    }  
})



