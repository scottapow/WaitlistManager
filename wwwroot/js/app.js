(function() {

    'use strict';

    var upEles = document.getElementsByClassName("cu");
    var downEles = document.getElementsByClassName("cd");
    var splitUp = [], splitDown = [];


    var i, j;
    // multidimensional -- splitUp/Down[ [hh,mm], [hh,mm], [hh,mm], etc...]
    for (i = 0; i < upEles.length; i++) {
        splitUp[i] =
            upEles[i].innerHTML.split(":");
        upEles[i].innerHTML =
            timeDisplay(splitUp[i][0], splitUp[i][1]);
    }
    for (j = 0; j < downEles.length; j++) {
        splitDown[j] =
            downEles[j].innerHTML.split(":");
        downEles[j].innerHTML =
            timeDisplay(splitDown[j][0], splitDown[j][1]);
    }

    // Every minute, adjusts upEles & downEles
    var k, l;
    setInterval(function () {
        // Add's 1 minute every 60000
        for (k = 0; k < upEles.length; k++) {
            splitUp[k] =
                timeUp(parseInt(splitUp[k][0]),
                       parseInt(splitUp[k][1]));
            upEles[k].innerHTML =
                timeDisplay(splitUp[k][0],
                            splitUp[k][1]);
        };
        // Subtracts 1 minute every 60000
        for (l = 0; l < downEles.length; l++) {
            splitDown[l] =
                timeDown(parseInt(splitDown[l][0]),
                         parseInt(splitDown[l][1]));
            downEles[l].innerHTML =
                timeDisplay(splitDown[l][0],
                            splitDown[l][1]);
        };
    }, 1000);

// Adjusts hours and minutes, up or down for CountUp elements
    function timeUp(h, m) {
        if (m >= 59) {
            h += 1;
            m -= 59;
        } else {
            m += 1;
        }
        return [h,m];
    };
// Adjusts hours and minutes, up or down for CountDown elements
    function timeDown(h, m) {
        if (m < 1 && h > 0) {
            h -= 1;
            m += 59;
        } else {
            m -= 1;
        }
        return [h, m];
    };
// Changes the text display from hh:mm to "{hh} hrs, {mm} mins"
    function timeDisplay(hours, mins) {
        var hourString = "",
            minString = "";
        if (hours > 0) {
            hourString = "" + hours + " hr ";
        }
        if (mins !== 0) {
            minString = "" + mins + " min";
        }
        if (mins < 1 && hours < 1) {
            minString = "Soon!";
        }
        return (hourString + minString);
    };


    
    //$('#SelectBarber').on("click", function () {
    //    if ($('form').valid()) {
    //        $.ajax({
    //            url: '@Url.RouteUrl(new{ action="GetAnswer", controller="Home"})',
    //            data: { Answer: '', Question: $('#Question').val() },
    //            type: 'POST',
    //            dataType: 'json',
    //            contentType: "application/json; charset=utf-8",
    //            success: function (resp) {
    //                openAlert(resp);
    //            }
    //        });
    //    }
    //    else {
    //        closeAlert();
    //    }
    //});

}());