
function expandcollapse(obj, row) {
    var div = document.getElementById(obj);
    var img = document.getElementById('img' + obj);
    if (div.style.display == "none") {
        div.style.display = "block";
        if (row == 'alt') {
            img.src = "minus.png";
        }
        else {
            img.src = "minus.png";
        }
        img.alt = "Close to view other Items";
    }
    else {
        div.style.display = "none";
        if (row == 'alt') {
            img.src = "plus.png";
        }
        else {
            img.src = "plus.png";
        }
        img.alt = "Expand to show Item";
    }
} 