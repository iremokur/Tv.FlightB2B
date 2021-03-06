/// <reference path="getcheckindate.js" />
/// <reference path="getDepartureAutoComplete.js" />
/// <reference path="flight.js" />

function getArrivalAutoComplete(depbody) {

    done = false;
    isSelected = false;
    $('#Arrival').keyup(function () {
        $('#arrresultcity').html('');
        $('#arrresultflight').html('');
        var searchField = $('#Arrival').val();
        var experission = new RegExp(searchField, "i");

        var request = {
            "currency": "eur", "culture": "tr-tr", "query": experission.source, "producttype": 3, "servicetype": "2",
            "departurelocations": [{ depbody }]
        }
      

        if (statusCheck("Arrival")) {
            $.ajax({
                type: "POST",
                url: "https://t3-services.tourvisio.com/v2/api/productservice/getarrivalautocomplete",
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
                            if (loc[i].city != null) {
                                var name = loc[i].city.name;
                                if (name.search(experission.source) != -1) {
                                    $('#arrresultcity').append("<datalist><option>" + name + "</option></datalist>");
                                }
                            }
                            if (loc[i].airport != null) {
                                var name = loc[i].airport.name;
                                if (name.search(experission.source) != -1) {
                                    $('#arrresultflight').append("<datalist><option>" + name + "</option></datalist>");
                                }
                            }
                            done = true;
                        });
                    }
                }
            });
        }
    });

    $('#Arrival').on('input', function () {
        var searchArr = $(this).val();
        $("#arr").find("option").each(function () {
            var search = $(this).val();

            if (search == searchArr &&done!=false) {

                isSelected = true;
                getSelectedArrival(searchArr, depbody);
            }
        });

    });
}
function getSelectedArrival(searchArr,depbody) {


        var selectedBodyar;
        var request = {
                "currency": "EUR", "culture": "tr-TR", "query": searchArr, "productType": 3, "serviceType": "2",
                "departureLocations": [{ depbody }]
        }
        $.ajax({
            type: "POST",
             url: "https://t3-services.tourvisio.com/v2/api/productservice/getarrivalautocomplete",
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
                                if (loc[i].city != null) {
                                    if (loc[i].city.name == searchArr) {
                                        selectedBodyar = loc[i];
                                        getCheckInDate(depbody, selectedBodyar);
                                    }
                                }
                                if (loc[i].airport != null) {
                                    if (loc[i].airport.name == searchArr) {
                                        selectedBodyar = loc[i];
                                        getCheckInDate(depbody, selectedBodyar);
                                    }
                                }
                            });
                        }
                    }      
        });

}