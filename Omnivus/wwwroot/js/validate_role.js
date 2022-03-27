const rolename = document.getElementById("rolename");
    const regExName = /^.{2,}$/;

rolename.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    validateWithRegEx(element, regExName, "Must be at least 2 characters");
    validateFormToSubmit();
});

// Validate Form on Submit
const validateFormOnSubmit = () => {
    validateWithRegEx(rolename, regExName, "Must be at least 2 characters");

    const errors = document.querySelectorAll(".input-box.error");

    if (errors.length > 0) {
        return false;
    }
    else {
        return true;
    }
};