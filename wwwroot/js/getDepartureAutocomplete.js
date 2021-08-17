/// <reference path="flight.js" />
/// <reference path="autocomplete.js" />

function getDepAutoComplete() {
 
    isSelected = false;
    $('#Departure').keyup(function () {
        $('#depresultcity').html('');
        $('#depresultflight').html('');
        var searchField = $('#Departure').val();
        var experission = new RegExp(searchField, "i");
        var request = { "currency": "EUR", "culture": "tr-TR", "query": experission.source, "productType": 3, "serviceType": "2" };
     
        if (statusCheck("Departure")) {
            $.ajax({
                type: "POST",
                url: "https://t3-services.tourvisio.com/v2/api/productservice/getdepartureautocomplete",
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
                                    $('#depresultcity').append("<datalist><option>" + name + "</option></datalist>");
                                }
                            }
                            if (loc[i].airport != null) {
                                var name = loc[i].airport.name;
                                if (name.search(experission.source) != -1) {
                                    $('#depresultflight').append("<datalist><option>" + name + "</option></datalist>");

                                }
                            }
                        });
                    }
                }
            });
        }
    });
    $(document).ready(function () {
        $('#Departure').on('input', function () {
            var searchdep = $(this).val();
            var selectedBodydep;
            $("#dep").find("option").each(function () {
                if ($(this).val() == searchdep) {
                    var request = { "currency": "EUR", "culture": "tr-TR", "query": searchdep, "productType": 3, "serviceType": "2" };
                    isSelected = true;

                    $.ajax({
                        type: "POST",
                        url: "https://t3-services.tourvisio.com/v2/api/productservice/getdepartureautocomplete",
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
                                        selectedBodydep = loc[i];
                                        getArrivalAutoComplete(selectedBodydep);
                                    }
                                });
                            }
                        }
                    });
                }
            })
        })
    });
}
