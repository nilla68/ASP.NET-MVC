const firstname = document.getElementById("firstname");
const lastname = document.getElementById("lastname");
const email = document.getElementById("email");
const password_1 = document.getElementById("password_1");
const password_2 = document.getElementById("password_2");
const street = document.getElementById("street");
const postcode = document.getElementById("postcode");
const city = document.getElementById("city");

const regExName = /^.{2,25}$/;
const regExEmail = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
const regExPassword = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})/;
const regExStreetAndCity = /^.{2,50}$/;
const regExPostCode = /^.{5,10}$/;


firstname.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    validateWithRegEx(element, regExName, "Must be between 2 - 25 characters");
    validateFormToSubmit();
});

lastname.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    validateWithRegEx(element, regExName, "Must be between 2 - 25 characters");
    validateFormToSubmit();
});

email.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    validateWithRegEx(element, regExEmail, "Please enter a valid email address");
    validateFormToSubmit();
});

password_1.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    validateWithRegEx(element, regExPassword, "Minimum 8 characters, must contain: 0-9, a-ö, A-Ö, !@#$%^&*");
    validatePassword2(element, password_2);
    validateFormToSubmit();
});

password_2.addEventListener("keyup", (keyboardEvent) => {
    validatePassword2(password_1, keyboardEvent.target);
    validateFormToSubmit();
});

const validatePassword2 = (password1Element, password2Element) => {
    if (password1Element.value === password2Element.value) {
        markAsValid(password2Element);
    } else {
        markAsInvalid(password2Element, "Passwords do not match");
    }
};

street.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    validateWithRegEx(element, regExStreetAndCity, "Must be between 2 - 50 characters");
    validateFormToSubmit();
});

postcode.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    validateWithRegEx(element, regExPostCode, "Must be between 5 - 10 characters");
    validateFormToSubmit();
});

city.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    validateWithRegEx(element, regExStreetAndCity, "Must be between 2 - 50 characters");
    validateFormToSubmit();
});

// Validate Form on Submit
const validateFormOnSubmit = () => {
    validateWithRegEx(firstname, regExName, "Must be between 2 - 25 characters");
    validateWithRegEx(lastname, regExName, "Must be between 2 - 25 characters");
    validateWithRegEx(email, regExEmail, "Please enter a valid email address");
    validateWithRegEx(password_1, regExPassword, "Minimum 8 characters, must contain: 0-9, a-ö, A-Ö, !@#$%^&*");
    validatePassword2(password_1, password_2);
    validateWithRegEx(street, regExStreetAndCity, "Must be between 2 - 50 characters");
    validateWithRegEx(postcode, regExPostCode, "Must be between 5 - 10 characters");
    validateWithRegEx(city, regExStreetAndCity, "Must be between 2 - 50 characters");

    const errors = document.querySelectorAll(".input-box.error");

    if (errors.length > 0) {
        return false;
    }
    else {
        return true;
    }
};