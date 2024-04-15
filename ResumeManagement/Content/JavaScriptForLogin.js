function password_show_hide() {
    var x = document.getElementById("password");
    var show_eye = document.getElementById("show_eye");
    var hide_eye = document.getElementById("hide_eye");
    hide_eye.classList.remove("d-none");
    if (x.type === "password") {
        x.type = "text";
        show_eye.style.display = "none";
        hide_eye.style.display = "block";
    } else {
        x.type = "password";
        show_eye.style.display = "block";
        hide_eye.style.display = "none";
    }
}


// JavaScript để hiển thị popup khi đưa chuột vào tên người dùng
document.addEventListener("DOMContentLoaded", function () {
    var userNameElement = document.querySelector(".nav-link-user");
    var userPopup = document.getElementById("userPopup");

    userNameElement.addEventListener("mouseover", function () {
        userPopup.style.display = "block";
    });

    userNameElement.addEventListener("mouseout", function () {
        userPopup.style.display = "none";
    });
});
