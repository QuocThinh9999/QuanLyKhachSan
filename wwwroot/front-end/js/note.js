var deleteBtn=document.querySelectorAll('.fa-trash-alt');
var notePart=document.getElementById('notes');
var cancelBtn=document.getElementById('cancel-btn');
deleteBtn.forEach(function (row) {
    row.addEventListener('click', function (event) {
      event.preventDefault();
      notePart.classList.add('show');
    });
  });
  cancelBtn.addEventListener('click', function (event) {
    event.preventDefault();
    notePart.classList.remove('show');
  });