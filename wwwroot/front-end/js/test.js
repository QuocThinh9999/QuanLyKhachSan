var deleteB = document.querySelectorAll('.fa-trash-alt');
var note = document.querySelectorAll('.notification');
var cancel = document.querySelectorAll('.cancel-btn');
deleteB.forEach((item, index) => {
    item.addEventListener('click', () => {
        note[index].classList.add('show');
    });
});
cancel.forEach((item, index) => {
    item.addEventListener('click', () => {
        note[index].classList.remove('show');
    });
});