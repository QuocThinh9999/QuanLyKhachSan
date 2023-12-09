'use strict';

document.addEventListener('DOMContentLoaded', function () {

    /*------------------
        Background Set
    --------------------*/
    const setBgElements = document.querySelectorAll('.set-bg');
    setBgElements.forEach(function (element) {
        const bg = element.getAttribute('data-setbg');
        element.style.backgroundImage = 'url(' + bg + ')';
    });
});

document.addEventListener('click', function (e) {
    if (e.target.classList.contains("ap-service-item")) {
        const src = e.target.getAttribute("data-setbg");
        document.querySelector(".modal-img").src = src;
        const myModal = new bootstrap.Modal(document.getElementById('gallery-modal'));
        myModal.show();
    }

})

//document.addEventListener("DOMContentLoaded", function () {
//    const form = document.querySelector("form");

//    form.addEventListener("submit", function (event) {
//        event.preventDefault(); // Ngăn chặn việc submit form mặc định

//        const ten = document.getElementById("ten").value.trim();
//        const email = document.getElementById("email").value.trim();
//        const sdt = document.getElementById("sdt").value.trim();

//        // Kiểm tra nếu Họ tên, email và số điện thoại không được nhập
//        if (ten === "" || email === "" || sdt === "") {
//            alert("Vui lòng nhập đầy đủ thông tin");
//            return;
//        }

//        // Kiểm tra số điện thoại chỉ chứa ký tự số và không chứa các ký tự khác
//        if (!/^\d+$/.test(sdt)) {
//            alert("Số điện thoại không hợp lệ");
//            return;
//        }

//        // Nếu thông tin hợp lệ, tiến hành submit form
//        this.submit();
//    });
//});
