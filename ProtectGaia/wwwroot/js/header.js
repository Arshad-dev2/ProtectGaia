$(window).on("scroll", function () {
    if ($(window).scrollTop() > 80) {
        $(".mu-navbar").css( "background-color","black");
    }
    else {
    $(".mu-navbar").css("background-color", "rgba(51, 51, 51, 0.5)");
    }
  /*  if($(window).scrollTop() > 50) {
        $(".header").addClass("active");
    }
    
    else {
        //remove the background property so it comes transparent again (defined in your css)
       $(".header").removeClass("active");
    }*/

});

function scrollToDiv(id) {
    $("#navbarSupportedContent li").parent().find('li').removeClass("active");
    $('html,body').animate({
        scrollTop: $("#" + id).offset().top - 55
    }, 'slow');
    $("#"+id).addClass("active");
}
