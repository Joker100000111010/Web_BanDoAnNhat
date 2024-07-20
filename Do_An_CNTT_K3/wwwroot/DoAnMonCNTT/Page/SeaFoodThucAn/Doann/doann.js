function toggleMenu(event) {
    var dropdownContent = document.querySelector('.dropdown-content');
    dropdownContent.classList.toggle('active');
    event.stopPropagation();
}
function toggleMenu(event) {
    var dropdownContent = document.querySelector('.dropdown-content');
    setTimeout(function () {
        dropdownContent.classList.toggle('active');
    }, 100); // Thời gian độ trễ, ví dụ: 200 miligiây
}
function toggleMenu1(event) {
    var dropdownContent = document.querySelector('.dropdown-content1');
    dropdownContent.classList.toggle('active');
    event.stopPropagation();
}
function toggleMenu1(event) {
    var dropdownContent = document.querySelector('.dropdown-content1');
    setTimeout(function () {
        dropdownContent.classList.toggle('active');
    }, 100); // Thời gian độ trễ, ví dụ: 200 miligiây
}
function toggleMenu2(event) {
    var dropdownContent = document.querySelector('.dropdown-content2');
    dropdownContent.classList.toggle('active');
    event.stopPropagation();
}
function toggleMenu2(event) {
    var dropdownContent = document.querySelector('.dropdown-content2');
    setTimeout(function () {
        dropdownContent.classList.toggle('active');
    }, 100); // Thời gian độ trễ, ví dụ: 200 miligiây
}

function toggleDropdown(id) {
    var element = document.getElementById(id);
    if (element.style.display === "none" || element.style.display === "") {
        element.style.display = "block";
    } else {
        element.style.display = "none";
    }
}
function toggleDropdown(id) {
    var element = document.getElementById(id);
    if (element.style.display === "none" || element.style.display === "") {
        element.style.display = "block";
    }
}





