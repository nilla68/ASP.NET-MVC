const name = document.getElementById("name");
const message = document.getElementById("message");
const email = document.getElementById("email");
const regExEmail = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
const regExName = /^.{2,}$/;
const regExMessage = /^.{5,300}$/;

name.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    validateWithRegEx(element, regExName, "Must be at least 2 characters");
    validateFormToSubmit();
});

message.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    validateWithRegEx(element, regExMessage, "Must be between 5 - 300 characters");
    validateFormToSubmit();
});

email.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    validateWithRegEx(element, regExEmail, "Please enter a valid email address");
    validateFormToSubmit();
});

// Validate Form on Submit
const validateFormOnSubmit = () => {
    validateWithRegEx(name, regExName, "Must be at least 2 characters");
    validateWithRegEx(message, regExMessage, "Must be between 5 - 300 characters");
    validateWithRegEx(email, regExEmail, "Please enter a valid email address");

    const errors = document.querySelectorAll(".input-box.error");

    if (errors.length > 0) {
        return false;
    }
    else {
        return true;
    }
};