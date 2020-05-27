$(window).on("scroll", function () {
    if ($(window).scrollTop() > 80) {
        $(".mu-navbar").css("background-color", "black");
    }
    else {
        $(".mu-navbar").css("background-color", "rgba(51, 51, 51, 0.5)");
    }
});
  
function scrollToDiv(id) {
    $("#navbarSupportedContent li").parent().find('li').removeClass("active");
    $('html,body').animate({
        scrollTop: $("#" + id).offset().top - 55
    }, 'slow');
    $("#" + id+"one").addClass("active");
    //$(this).addClass('active');

}

function MakeActive(id) {
    var new_id = "#" + id;
    $("#navbarSupportedContent li ").parent().find('li').removeClass("active");
    //$("#navbarSupportedContent li ").parent().find('li').removeClass("active");

    $(new_id).addClass("active");
}