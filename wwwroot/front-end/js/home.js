var adultCountSpan = document.getElementById('adult-count');
var adultMnButton = document.getElementById('adult-mn');
var adultPlButton = document.getElementById('adult-pl');
var kidCountSpan = document.getElementById('kid-count');
var kidMnButton = document.getElementById('kid-mn');
var kidPlButton = document.getElementById('kid-pl');
var checkDatebtn = document.getElementById('checkdate-btn');
var notiValidate = document.getElementById('validation-notes');
var openBtn = document.getElementById('open-btn');
var sidebarMenu = document.getElementById('sidebar-menu');
var closeBtn = document.getElementById('close-btn');
var oversLay = document.getElementById('overs-lay');
openBtn.addEventListener('click', function () {
    sidebarMenu.style.left = '0';
    sidebarMenu.style.opacity = '1';
    sidebarMenu.style.visibility = 'visible';
    oversLay.style.visibility = 'visible';
});
closeBtn.addEventListener('click', function () {

    sidebarMenu.style.left = '-300px';
    sidebarMenu.style.opacity = '0';
    sidebarMenu.style.visibility = 'hidden';
    oversLay.style.visibility = 'hidden';
});
checkDatebtn.addEventListener('click', function () {
    compareDate();
});
function compareDate() {
    var dateInput1 = new Date(document.getElementById('hasDatepicker1').value);
    var dateInput2 = new Date(document.getElementById('hasDatepicker2').value);
    
    if (dateInput1 > dateInput2) {
        notiValidate.style.display = 'block';
    } else {
        notiValidate.style.display = 'none';
    }
}
kidMnButton.addEventListener('click', function () {
    decrement('kid-count');
});

kidPlButton.addEventListener('click', function () {
    increment('kid-count');
});
adultMnButton.addEventListener('click', function () {
    decrement('adult-count');
});
adultPlButton.addEventListener('click', function () {
    increment('adult-count');
});

function decrement(text) {
    var inputElement = document.getElementById(text);
    var currentValue = parseInt(inputElement.value, 10);

    if (currentValue > 0) {
        inputElement.value = currentValue - 1;
    }
}

function increment(text) {
    var inputElement = document.getElementById(text);
    var currentValue = parseInt(inputElement.value, 10);

    inputElement.value = currentValue + 1;
}
// Lấy tham chiếu đến các phần tử HTML
const adultCountInput = document.getElementById('adult-count');
const kidCountInput = document.getElementById('kid-count');
const adultMinusBtn = document.getElementById('adult-mn');
const adultPlusBtn = document.getElementById('adult-pl');
const kidMinusBtn = document.getElementById('kid-mn');
const kidPlusBtn = document.getElementById('kid-pl');

// Ngăn chặn sự kiện tải lại trang khi nhấn nút số lượng
function preventReload(event) {
    event.preventDefault();
}

// Gán sự kiện click cho nút số lượng người lớn
adultMinusBtn.addEventListener('click', preventReload);
adultPlusBtn.addEventListener('click', preventReload);

// Gán sự kiện click cho nút số lượng trẻ em
kidMinusBtn.addEventListener('click', preventReload);
kidPlusBtn.addEventListener('click', preventReload);