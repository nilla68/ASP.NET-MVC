const firstname = document.getElementById("firstname");
const lastname = document.getElementById("lastname");
const street = document.getElementById("street");
const postcode = document.getElementById("postcode");
const city = document.getElementById("city");

const regExName = /^.{2,25}$/;
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