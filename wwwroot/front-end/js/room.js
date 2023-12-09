const rangInput = document.querySelectorAll(".range-input input"),
    priceInput = document.querySelectorAll(".price__input input"),
    progress = document.querySelector(".slider .progress");
var notesearch = document.getElementById('notes-search');
var searchbtn = document.getElementById('searchbtn');
let priceGap = 10000;
rangInput.forEach(input => {
    input.addEventListener("input", e => {
        let minVal = parseInt(rangInput[0].value),
            maxVal = parseInt(rangInput[1].value);
        if ((maxVal - minVal < priceGap)) {
            if (e.target.className === "range-min") {
                rangInput[0].value = maxVal - priceGap;
            } else {
                rangInput[1].value = minVal + priceGap;
            }
        } else {
            priceInput[0].value = minVal;
            priceInput[1].value = maxVal;
            progress.style.left = (minVal / rangInput[0].max) * 100 + "%";
            progress.style.right = (100 - (maxVal / rangInput[1].max) * 100) + "%";
        }

    });
});
function compareDate1() {
    var dateInput1 = new Date(document.getElementById('checkin-date').value);
    var dateInput2 = new Date(document.getElementById('checkout-date').value);
    if (dateInput1 > dateInput2) {
        notesearch.style.display = 'block';
    } else {
        notesearch.style.display = 'none';
    }
}
searchbtn.addEventListener('click', function (event) {
    compareDate1();
})