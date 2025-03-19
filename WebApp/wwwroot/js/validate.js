const validateField = (field) => {
    let spanError = document.querySelector(`span[data-valmsg-for='${field.name}']`)
    if (!spanError) return;

    let errorMessage = ""
    let value = field.value.trim()

    if (field.hasAttribute("data-val-required") && value === "")
        errorMessage = field.getAttribute("data-val-required")

    if (field.hasAttribute("data-val-regex") && value !== "") {
        let pattern = new RegExp(field.getAttribute("data-val-regex-pattern"))
        if (!pattern.test(value))
            errorMessage = field.getAttribute("data-val-regex")
    }

    if (errorMessage) {
        spanError.classList.remove("field-validation-valid")
        spanError.classList.add("field-validation-error")
        spanError.textContent = errorMessage
    } else {
        spanError.classList.add("field-validation-valid")
        spanError.classList.remove("field-validation-error")
        spanError.textContent = ""
    }
}

document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form")

    if (!form) return;

    const fields = form.querySelectorAll("input[data-val='true']")

    fields.forEach(field => {
        field.addEventListener("input", function () {
            validateField(field)
        })
    })
})