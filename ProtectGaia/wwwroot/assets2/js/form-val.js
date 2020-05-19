﻿$('#markcomplete').prop("disabled", true);
$('input:checkbox').click(function () {
    if ($(this).is(':checked')) {
        $('#markcomplete').prop("disabled", false);
    } else {
        if ($('.checks').filter(':checked').length < 1) {
            $('#markcomplete').attr('disabled', true);
        }
    }
});




$("#level_1").click(function () {
   getchallenge(1);
});

$("#level_2").click(function () {
   getchallenge(2);
});

$("#level_3").click(function () {
    getchallenge(3);
});
$("#level_4").click(function () {
    getchallenge(4);
});
$("#level_5").click(function () {
    getchallenge(5);
})

function getchallenge(data) {
    $.ajax({
        url: "Dashboard/GetChallenges",
        type: "POST",
        data: { LevelId: data },
        cache: false,
        async: false,
        success: function (response) {
            console.log(response);
            $('.dynamic').html("");
            $('.dynamic').html(response);
            //$('#level' + data).html("<ul class=\"leaf\"><li>" + response.challenges[0] + "</li><li>" + response.challenges[1] + "</li><li>" + response.challenges[2] + "</li><li>" + response.challenges[3] +"</li>");
        }
    })
}


$(document).ready(function () {
    $(".set > a").on("click", function () {
        if ($(this).hasClass("active")) {
            $(this).removeClass("active");
            $(this)
                .siblings(".content")
                .slideUp(200);
            $(".set > a i")
                .removeClass("fa-minus")
                .addClass("fa-plus");
        } else {
            $(".set > a i")
                .removeClass("fa-minus")
                .addClass("fa-plus");
            $(this)
                .find("i")
                .removeClass("fa-plus")
                .addClass("fa-minus");
            $(".set > a").removeClass("active");
            $(this).addClass("active");
            $(".content").slideUp(200);
            $(this)
                .siblings(".content")
                .slideDown(200);
        }
    });
});




