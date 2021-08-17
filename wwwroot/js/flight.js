/// <reference path="getdepartureautocomplete.js" />


$(document).ready(function () {
    if (localStorage.getItem("Token") == null || localStorage.getItem("Token") == '' || localStorage.getItem("Token") == 'undefined')
        localStorage.setItem("Token", getCookie("Token"));

   getDepAutoComplete();
  
});


function getCookie(cookiename) {
    
    var cookies = document.cookie;
    return cookies.split("=")[1];
}


function statusCheck(elmID)
{
    var value = document.getElementById(elmID).value;
    flag = false;
    if (value.length >= 3) {
        flag = true;
    }
    return flag;

}
function logOut() {
    $('#logoutBtn').click(function () {

        window.localStorage.removeItem('Token');
        window.location.href = '@Url.Action("Index", "Home")';
    });
}




