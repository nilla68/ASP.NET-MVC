const street = document.getElementById("street");
const postcode = document.getElementById("postcode");
const city = document.getElementById("city");

street.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    const regEx = /^.{2,50}$/;
    validateWithRegEx(element, regEx, "Must be between 2 - 50 characters");
    validateFormToSubmit();
});

postcode.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    const regEx = /^.{5,10}$/;
    validateWithRegEx(element, regEx, "Must be between 5 - 10 characters");
    validateFormToSubmit();
});

city.addEventListener("keyup", (keyboardEvent) => {
    const element = keyboardEvent.target;
    const regEx = /^.{2,50}$/;
    validateWithRegEx(element, regEx, "Must be between 2 - 50 characters");
    validateFormToSubmit();
});

