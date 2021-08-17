



function priceSearch() {
    $(document).ready(function () {
        $.ajax({
            type: "POST",
            url: "https://t3-services.tourvisio.com/v2/api/productservice/pricesearch",
            error: (result) => $.Notification.error(result),
            dataType: 'json',
            data: JSON.stringify(request),
            success: function (result) {
                var myJSON = JSON.stringify(result);
                var response = JSON.parse(myJSON);
                if (response["header"].success) {

                }
            }
        });
    });
}





//var request = {
//    "productType": 3, "serviceTypes": ["1"], "checkIn": "2021-08-21T00:00:00",
//    "includeSubLocations": false, "departureLocations": [{
//        "name": "Antalya, Türkiye (Tüm Havaalanları)", "type": 2, "provider": 3, "isTopRegion": false, "id": "AYT",
//        "uiName": "Antalya, Türkiye (Tüm Havaalanları)"
//    }], "arrivalLocations": [{
//        "name": "İstanbul, Türkiye (Tüm Havaalanları)",
//        "type": 2, "provider": 0, "isTopRegion": false, "id": "IST", "uiName":
//            "İstanbul, Türkiye (Tüm Havaalanları)"
//    }], "childAges": [], "checkAllotment": true, "checkStopSale": true,
//    "showAllotment": false, "showStopSale": false, "getOnlyDiscountedPrice":
//        false, "getOnlyBestOffers": false, "getTransportations": false,
//    "pagingOption": {
//        "productType": 0, "pageRowCount": 0, "currentPage": 1,
//        "sort": 0, "getFilters": false, "isNewPagingRequest": false,
//        "getOnlyDefaultProducts": false, "getPagingCount": false
//    }, "showOnlyNonStopFlight": false, "forceFlightBundlePackage": false,
//    "searchBrandedFares": false, "calculateFlightFees": true,
//    "disablePackageOfferTotalPrice": true,
//    "supportedFlightReponseListTypes": [2, 3], "compulsory": false,
//    "additionalParameters": { "getCountry": false, "getTransferLocation": true },
//    "passengers": [{ "type": 1, "ages": [], "count": 1 }],
//    "priceSearchResponseDetails": { "flightResponseType": 4, "flightResponseListType": 1 },
//    "customer": {}, "targetProvider": 0, "id": "ce68b5fc-a19e-49dc-9fab-1dbfab7fd62b", "dataSource": 0, "webRequestDetail":
//    {
//        "clientAddress": "176.232.62.129", "domain": "t3-services.tourvisio.com",
//        "userAgent":
//            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.131 Safari/537.36",
//        "referrer": "https://t3-b2b.tourvisio.com/"
//    }, "currency": "EUR", "culture": "tr-TR", "provider": 7
//};