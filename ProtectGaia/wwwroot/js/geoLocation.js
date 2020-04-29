$(document).ready(function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    }
});
function showPosition(position) {
        makeCall(position.coords.latitude +
            "," + position.coords.longitude);
}
function showError(error) {
    switch (error.code) {
        case error.PERMISSION_DENIED:
            makeCall("UnAvailable");
            break;
        case error.POSITION_UNAVAILABLE:
            makeCall("UnAvailable");
            break;
        case error.TIMEOUT:
            makeCall("UnAvailable");
            break;
        case error.UNKNOWN_ERROR:
            makeCall("UnAvailable");
            break;
    }
}

function makeCall(location) {
    $.ajax({
        url: "/Home/GetLocation",
        type: "POST",
        data: { "location": location },
        cache: false,
        async: false,
        success: function (response) {
            if (response.isSucess==true) {
                $(".deviceLocation").html("<p><i class=\"fa fa-map-marker\" style=\"color: #ff3333\"></i>&nbsp;" + response.location.address + "</p>");
                $(".currentTemp").html("<img src=\"" + response.location.icon + "\" style=\"widhth:30%;float:right;position:relative;bottom:2rem;right:1rem\" alt=\"Weather\"/><h3><b>Current Temp</b></h3><p style=\"font-size:40px;\"><b>" + response.location.temp + "&#8451" + "<b>/</b>"  + response.location.feelsLikeTemp + "&#8451;</b></p>");
                $(".AqiValue").html("<h3><b>Air Quality Index</b></h3> <b><h1 style=\"font-size:54px;\"><b> " + response.location.aqi + "&nbsp;</b ></h1 >μg/m<superscript>3</superscript>");
                $(".OzoneValue").html("<h3><b>Ozone Index</b></h3><b><h1 style=\"font-size:54px;\"><b> " + response.location.ozone + "&nbsp;</b ></h1 >μg/m<superscript>3</superscript>");
            }
            else {
                $(".deviceLocation").html("<br />Your device location<br/> <b>" + "Location Unavailable" + "</b>");
                $(".currentTemp").html("<br /><b>Current Weather</b><br/>Unavailable");
                $(".AqiValue").html("<br />Air Quality Index<br />Unavailable");
                $(".OzoneValue").html("<br />Ozone Index<br />Unavailable");
            }
        }
        
    })

}