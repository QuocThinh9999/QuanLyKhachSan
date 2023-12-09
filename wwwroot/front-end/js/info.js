var inforBtn = document.querySelectorAll('.fa-info');
var inforPart = document.querySelectorAll('.information');
var closeBtn = document.querySelectorAll('.close-btn');
console.log(inforBtn, inforPart, closeBtn);
inforBtn.forEach((item, index) => {
    item.addEventListener('click', () => {
        inforPart[index].classList.add('show');
        console.log(index);
    });
});
closeBtn.forEach((item, index) => {
    item.addEventListener('click', () => {
        inforPart[index].classList.remove('show');
        console.log(index);
    });
});