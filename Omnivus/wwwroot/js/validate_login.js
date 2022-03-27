const email = document.getElementById("email");
const password = document.getElementById("password");
const regExEmail = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
const regExPassword = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})/;

email.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    validateWithRegEx(element, regExEmail, "Please enter a valid email address");
    validateFormToSubmit();
});

password.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    validateWithRegEx(element, regExPassword, "Minimum 8 characters, must contain: 0-9, a-ö, A-Ö, !@#$%^&*");
    validateFormToSubmit();
});

// Validate Form on Submit
const validateFormOnSubmit = () => {
    validateWithRegEx(email, regExEmail, "Please enter a valid email address");
    validateWithRegEx(password, regExPassword, "Minimum 8 characters, must contain: 0-9, a-ö, A-Ö, !@#$%^&*");

    const errors = document.querySelectorAll(".input-box.error");

    if (errors.length > 0) {
        return false;
    }
    else {
        return true;
    }
};