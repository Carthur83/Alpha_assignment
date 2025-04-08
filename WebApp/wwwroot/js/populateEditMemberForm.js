// tagit hjälp av chatgpt

document.addEventListener("DOMContentLoaded", () => {
    const editButtons = document.querySelectorAll(".edit-member-btn")
    const form = document.getElementById("editMemberForm")

    editButtons.forEach(button => {
        button.addEventListener("click", async (e) => {
            e.preventDefault()

            const memberId = button.getAttribute("data-project-id")
            const response = await fetch(`/admin/edit/${memberId}`)

            if (!response.ok) {
                alert("No data found")
                return;
            }

            const data = await response.json()

            form.querySelector("input[name='Id']").value = data.id;
            form.querySelector("input[name='ImageFileName']").value = data.imageFileName;
            form.querySelector("input[name='FirstName']").value = data.firstName;
            form.querySelector("input[name='LastName']").value = data.lastName;
            form.querySelector("input[name='Email']").value = data.email;
            form.querySelector("input[name='JobTitle']").value = data.jobTitle;
            form.querySelector("input[name='Phone']").value = data.phone;
            form.querySelector("input[name='Street']").value = data.street;
            form.querySelector("input[name='City']").value = data.city;
            form.querySelector("input[name='PostalCode']").value = data.postalCode;
            form.querySelector("select[name='Day']").value = data.day;
            form.querySelector("select[name='Month']").value = data.month;
            form.querySelector("select[name='Year']").value = data.year;

        })
    })

})