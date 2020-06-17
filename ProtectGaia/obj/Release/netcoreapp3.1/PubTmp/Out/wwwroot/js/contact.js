$(function () {
    $("#nav ul li ").parent().find('li').removeClass("current");

    var elem = window.location.pathname.toLocaleLowerCase();
    console.log(elem);

    if (elem.includes('explainers')) {
        $("#explainers").addClass("current");
    }
    if (elem.includes('home')) {
        $("#home").addClass("current");

    }
    if (elem==='/') {
        $("#home").addClass("current");

    }
    if (elem.includes('dashboard')) {
        $('#profile').addClass('current');
    }

});