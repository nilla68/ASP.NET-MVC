const button = document.getElementById("submitform");

// Show and hide error messages
const markAsValid = (element) => {
    const inputBox = element.parentElement;
    inputBox.className = "input-box success";
    element.style.borderColor = "green";
};

const markAsInvalid = (element, text) => {
    const inputBox = element.parentElement;
    inputBox.className = "input-box error";
    const small = inputBox.querySelector("small");
    small.innerText = text;
    element.style.borderColor = "red";
};

// RegEx Validation
const validateWithRegEx = (element, regEx, errorMessage) => {
    if (regEx.test(element.value)) {
        markAsValid(element);
    } else {
        markAsInvalid(element, errorMessage);
    }
};

const validateFormToSubmit = () => {
    let disable = false;
    const errors = document.querySelectorAll(".input-box.error");

    if (errors.length > 0) {
        disable = true;
    }

    button.disabled = disable;
};