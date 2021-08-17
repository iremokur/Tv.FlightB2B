/// <reference path="getarrivalautocomplete.js" />
/// <reference path="getdepartureautocomplete.js" />
/// <reference path="flight.js" />


function getCheckInDate(selectedBodydep, selectedBodyarr) {

    $('#checkin').click(function () {
        var DepartureDate = $('#checkin').val();
        if (selectedBodyarr.airport != null && selectedBodydep.airport != null) {

            var request = { "currency": "EUR", "culture": "tr-TR", "departureLocations": [{ "id": selectedBodydep.airport.id }], "arrivalLocations": [{ "id": selectedBodyarr.airport.id }], "serviceType": "2", "productType": 3 };
        }
        if ( selectedBodyarr.city != null && selectedBodydep.city != null ) {

            var request = { "currency": "EUR", "culture": "tr-TR", "departureLocations": [{ "id": selectedBodydep.city.id }], "arrivalLocations": [{ "id": selectedBodyarr.city.id }], "serviceType": "2", "productType": 3 };

        }
        if (selectedBodyarr.city != null && selectedBodydep.airport != null) {

            var request = { "currency": "EUR", "culture": "tr-TR", "departureLocations": [{ "id": selectedBodydep.airport.id }], "arrivalLocations": [{ "id": selectedBodyarr.city.id }], "serviceType": "2", "productType": 3 };
        }
        if (selectedBodyarr.city != null && selectedBodydep.airport != null) {

            var request = { "currency": "EUR", "culture": "tr-TR", "departureLocations": [{ "id": selectedBodydep.city.id }], "arrivalLocations": [{ "id": selectedBodyarr.airport.id }], "serviceType": "2", "productType": 3 };
        }

      
        $.ajax({
            type: "POST",
            url: "https://t3-services.tourvisio.com/v2/api/productservice/getcheckindates",
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
                    return response.body.dates;
                }
            }
        });

    });
}