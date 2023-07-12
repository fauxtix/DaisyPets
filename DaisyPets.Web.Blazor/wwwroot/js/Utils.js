window.PreventEnterKey = (id) => {
    var formObj = document.getElementById(id);
    formObj.addEventListener('keydown', function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    })
}; 