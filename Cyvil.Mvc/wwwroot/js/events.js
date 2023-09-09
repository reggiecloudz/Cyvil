function loadEventObjects() {
    $("#MeetingType").on("change", function() {
        $('#MeetingTypeId').empty();
        const type = $(this).find(":selected").val();
        const url = `/Events/${type}/GetEventTypeObjects`;

        function successCallback(response) {
            if (typeof response === "string") {
                $("#MeetingTypeId").append(`<option value="0">${response}</option>`);
            }
            else {
                response.forEach(obj => {
                    const text = obj.name ? obj.name : obj.title;
                    $("#MeetingTypeId").append(`<option value="${obj.id}">${text}</option>`);
                });     
            }
            console.log(response);
        }

        function errorCallback(xhr, status, error) {
            console.log(error);
        }

        ajaxGet(url, successCallback, errorCallback);
    });
}

function insertEventsTableData(data) {
    const tr = $("#event-list-table").append(`
        <tr class="position-static">
            <td class="align-middle time white-space-nowrap ps-0 eventName py-4">
                <a class="fw-bold fs-0" asp-controller="Projects" asp-action="Details" asp-route-id="@item.Id">
                    ${data.name}
                </a>
            </td>
            <td class="align-middle white-space-nowrap type py-4">
                <p class="mb-0 fs--1 text-900 text-center">
                    ${data.type}
                </p>
            </td>
            <td class="align-middle white-space-nowrap attendees py-4">
                <p class="mb-0 fs--1 text-900 text-center">0</p>
            </td>
            <td class="align-middle white-space-nowrap startDate py-4">
                <p class="mb-0 fs--1 text-900 text-center">
                    ${data.startDate}
                </p>
            </td>
            <td class="align-middle white-space-nowrap endDate py-4">
                <p class="mb-0 fs--1 text-900 text-center">
                    ${data.endDate}
                </p>
            </td>
            <td class="align-middle white-space-nowrap py-4 capacity">
                <p class="mb-0 fs--1 text-900 text-center">${data.capacity}</p>
            </td>
            <td class="align-middle white-space-nowrap ps-3 py-4 location">
                <p class="mb-0 fs--1 text-900">${data.location} </p>
            </td>
            <td class="align-middle text-end white-space-nowrap pe-0 action">
                <div class="font-sans-serif btn-reveal-trigger position-static">
                    <button
                        class="btn btn-sm dropdown-toggle dropdown-caret-none transition-none btn-reveal fs--2"
                        type="button" data-bs-toggle="dropdown" data-boundary="window" aria-haspopup="true"
                        aria-expanded="false" data-bs-reference="parent"><span
                            class="fas fa-ellipsis-h fs--2"></span></button>
                    <div class="dropdown-menu dropdown-menu-end py-2"><a class="dropdown-item"
                            href="#!">View</a><a class="dropdown-item" href="#!">Export</a>
                        <div class="dropdown-divider"></div><a class="dropdown-item text-danger"
                            href="#!">Remove</a>
                    </div>
                </div>
            </td>
        </tr>
    `);

    return tr;
}

function saveEvent() {
    var form = document.getElementById("meeting-form");
    form.addEventListener('submit', (e) => {
        e.preventDefault();

        var myModalEl = document.getElementById('saveMeetingModal');
        var modal = bootstrap.Modal.getInstance(myModalEl);
        const formData = new FormData(form);
        const url = "/Events/Create";

        function successCallback(response) {
            modal.hide();
            form.reset();
            insertEventsTableData(response);
        }

        function errorCallback(xhr, status, error) {
            modal.hide();
            form.reset();
            console.log(error);
        }

        ajaxFormPost(url, formData, successCallback, errorCallback);

    });
}

function saveAttendee() {
    var form = document.getElementById("attendee-form");
    form.addEventListener('submit', (e) => {
        e.preventDefault();

        var myModalEl = document.getElementById('saveAttendeeModal');
        var modal = bootstrap.Modal.getInstance(myModalEl);
        const formData = new FormData(form);
        const meetingId = form.dataset.meetingAttendance;
        const url = `/Events/${meetingId}/Invite`;

        function successCallback(response) {
            
            modal.hide();
            $('input[name="selectedUsers"]').filter(':checked').parent().remove();
            form.reset();
            console.log(response.message);
        }

        function errorCallback(xhr, status, error) {
            modal.hide();
            form.reset();
            console.log(error);
        }

        ajaxFormPost(url, formData, successCallback, errorCallback);

    });
}