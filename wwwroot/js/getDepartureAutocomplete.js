/// <reference path="getarrivalautocomplete.js" />
/// <reference path="flight.js" />

function getDeparture() {

    done = false;
    isSelected = false;
    var searchField;

    var experission;
    $('#Departure').keyup(function () {
        $('#depresultcity').html('');
        $('#depresultflight').html('');
        searchField = $('#Departure').val();
        experission =new RegExp(searchField, "i");
       
      
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
                                    $('#depresultcity').append("<datalist><option>"+ name  + "</option></datalist>");
                                }
                            }
                            if (loc[i].airport != null) {
                                var name = loc[i].airport.name;
                                if (name.search(experission.source) != -1) {
                                    $('#depresultflight').append("<datalist><option> "+name  + "</option></datalist>");

                                }
                            }
                            done = true;
                        });
                    }
                }
            });
        }
    });
    $('#Departure').on('input', function () {
        var searchdep = $(this).val();
        $("#dep").find("option").each(function () {
            var search = $(this).val();

            if (searchdep == search && done!=false) {

                isSelected = true;

                getSelected(searchdep);
            }
        });
    });
}
function getSelected(searchdep) {

    var selectedBodydep;
    var request = { "currency": "EUR", "culture": "tr-TR", "query": searchdep, "productType": 3, "serviceType": "2" };
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
                            if (loc[i].city.name == searchdep) {
                                selectedBodydep = loc[i];
                                getArrivalAutoComplete(selectedBodydep);

                            }
                        }
                        if (loc[i].airport != null) {
                                if (loc[i].airport.name == searchdep) {
                                    selectedBodydep = loc[i];
                                    getArrivalAutoComplete(selectedBodydep);
                                }
                        }
                });
            }
        }
    });

   

}
