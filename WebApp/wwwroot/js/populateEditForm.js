// tagit hjälp av chatgpt

document.addEventListener("DOMContentLoaded", () => {
    const editButtons = document.querySelectorAll(".edit-project-btn")
    const form = document.getElementById("edit-form")

    editButtons.forEach(button => {
        button.addEventListener("click", async (e) => {
            e.preventDefault()

            const projectId = button.getAttribute("data-project-id")
            const response = await fetch(`/projects/edit/${projectId}`)

            if (!response.ok) {
                alert("No data found")
                return;
            }

            const data = await response.json()
            console.log(data)
            const clientSelect = form.querySelector("select[name='ClientId']");
            const statusSelect = form.querySelector("select[name='StatusId']");

            clientSelect.innerHTML = "<option value='' disabled>-- Select client --</option>";
            statusSelect.innerHTML = "<option value='' disabled>-- Select status --</option>";

            data.clients.forEach(client => {
                const option = document.createElement("option");
                option.value = client.value;
                option.text = client.text;

                if (parseInt(client.value) === parseInt(data.clientId)) {
                    option.selected = true;
                }

                clientSelect.appendChild(option);
            });

            data.statuses.forEach(status => {
                const option = document.createElement("option");
                option.value = status.value;
                option.text = status.text;

                if (parseInt(status.value) === parseInt(data.statusId)) {
                    option.selected = true;
                }

                statusSelect.appendChild(option);
            })


            form.querySelector("input[name='Id']").value = data.id;
            form.querySelector("input[name='ProjectName']").value = data.projectName;
            form.querySelector("textarea[name='Description']").value = data.description;
            form.querySelector("input[name='StartDate']").value = data.startDate.slice(0, 10);
            form.querySelector("input[name='EndDate']").value = data.endDate.slice(0, 10);
            form.querySelector("input[name='Budget']").value = data.budget;

        })
    })
})