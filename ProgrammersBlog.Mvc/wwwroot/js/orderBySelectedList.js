$(document).ready(function (event) {
    var orderSelectList = document.getElementById("orderSelectList");
    var selectedOption = orderSelectList.getAttribute('data-selected-value');
    for (var i = 0; i < orderSelectList.options.length; i++) {
        if (orderSelectList.options[i].value === selectedOption) {
            orderSelectList.selectedIndex = i;
            break;
        }
    }
})