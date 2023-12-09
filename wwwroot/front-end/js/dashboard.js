var sideBar = document.getElementById('sidebar');
var overLay = document.getElementById('overlay');
var sideBarBtn = document.getElementById('sidebar-active');
console.log(sideBar);
sideBarBtn.addEventListener('click', function () {
    sideBar.style.left = 0;
    overLay.style.visibility = 'visible';
})
overLay.addEventListener('click', function () {
    sideBar.style.left = '-270px';
    overLay.style.visibility = 'hidden';
})
function handleResize() {
    let width = window.innerWidth;
    if (width > 992) {
        sideBar.style.left = '0';
        overLay.style.visibility = 'hidden';
    } else if (width < 992) {
        sideBar.style.left = '-270px';
    }
}
window.addEventListener('resize', handleResize);