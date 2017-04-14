function DateChangeEvent() {
    var date = new Date(document.getElementById("date").value);
    if (date.getDay() == 0 || date.getDay() == 6)
        alert("Holiday");
    else
        alert("Working Day");
}