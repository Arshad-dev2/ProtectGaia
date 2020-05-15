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


            if (response.isSucess == true) {
                var aqi_category = CategoryAqi(response.location.aqi);
                var ozone_category = CategoryOzone(response.location.ozone);
                $(".deviceLocation").html("<p><i class=\"fa fa-map-marker\" style=\"color: #ff3333\"></i>&nbsp;" + response.location.address + "</p>");
                $(".currentTemp").html("<h3>Current Temperature</h3><p style=\"font-size:1.5em;\">" + response.location.temp + "&#8451" + "<b>/</b>" + response.location.feelsLikeTemp + "&#8451;</p>");
                $(".AqiValue").html("<h3>Air Quality Index</h3><p style=\"font-size:1.5em;\"> " + response.location.aqi + "&nbsp;</h1 >μg/m<superscript>3</superscript><p style=\"color:black\"><b>" + aqi_category + "</b></p>");
                $(".OzoneValue").html("<h3>Ozone Index</h3><p style=\"font-size:1.5em;\"> " + response.location.ozone + "&nbsp;</h1 >ppb<p style=\"color:black;\"> <b>" + ozone_category + "</b></p>");
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

function CategoryAqi(value) {
    if (value >= 0 && value <= 50) {
        return "Good";
    }
    else if (value > 50 && value <= 100) {
        return "Moderate";
    }
    else if (value > 100 && value <= 150) {
        return "Unhealthy for Sensitivite groups";
    }
    else {
        return "Unhealthy";
    }
}
function CategoryOzone(value) {
    if (value >= 0 && value <= 59) {
        return "Good";
    }
    else if (value > 60 && value <= 75) {
        return "Moderate";
    }
    else if (value > 76 && value <= 95) {
        return "Unhealthy for Sensitivite groups";
    }
    else {
        return "Unhealthy";
    }
}