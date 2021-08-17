/// <reference path="getArrivalAutoComplete.js" />
/// <reference path="getDepartureAutoComplete.js" />
/// <reference path="flight.js" />
/// <reference path="getCheckInDate.js" />

function getAutoComplete(elemID, cityoption, flightop, request, serviceurl, methodurl, experission, isSelected, type) {
    var selectedBodydep;
    var selectedBodyarr;
    if (statusCheck(elemID)) {
        $.ajax({
            type: "POST",
            url: "https://t3-services.tourvisio.com/v2" + serviceurl + methodurl,
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("Token")//using bearer token! that's why!
            },
            error: (result) => $.Notification.error(result),
            dataType: 'json',
            data: JSON.stringify(request),
            success: function (result) {
                var myJSON = JSON.stringify(result);
                var response = JSON.parse(myJSON);
                if (response["header"].success) {
                    var loc = response['body'].items;
                    $.each(loc, function (i) {
                  
                        if (isSelected == true) {
                            if (type == "2") {
                                selectedBodydep = loc[i];
                                //getArrivalAutoComplete(selectedBodydep);
                            }
                            if (type == "1") {
                                selectedBodyarr = loc[i];
                                //getCheckInDate(selectedBodydep, selectedBodyarr);
                            }
                        }
                    });
                }
            }
        });
    }
}