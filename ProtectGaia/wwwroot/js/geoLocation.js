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
                $(".deviceLocation").html("<br />Your device location<br/> <h3>" + response.location.address + "</h3>");
                $(".currentTemp").html("</br><img src=\"" + response.location.icon + "\" style=\"widhth:30%;float:right\" alt=\"Weather\"/><b>Current Weather</b><br/>Current Temp:" + response.location.temp + "&#8451;<br/>Feels Like: " + response.location.feelsLikeTemp + "&#8451;");
                $(".AqiValue").html("<br />Air Quality Index<br /> <h1 style=\"display:inline\">" + response.location.aqi + "</h1>&nbsp;μg/m<superscript>3</superscript>");
                $(".OzoneValue").html("<br />Ozone Index<br /><h1 style=\"display:inline\">" + response.location.ozone + "</h1>&nbsp;μg/m<superscript>3</superscript>");
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