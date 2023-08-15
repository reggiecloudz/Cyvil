function ajaxFormPost(url, formData, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        beforeSend: function (xhr) {  
            xhr.setRequestHeader("XSRF-TOKEN",  
                $('input:hidden[name="__RequestVerificationToken"]').val());  
        },
        data: formData,
        processData: false,
        contentType: false,
        success: function(response) {
            if (successCallback) {
                successCallback(response);
            }
        },
        error: function(xhr, status, error) {
            if (errorCallback) {
                errorCallback(xhr, status, error);
            }
        }
    });
}

function sendAjaxRequest(url, data, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: successCallback,
        error: errorCallback
    });
}
  
function ajaxGet(url, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        success: successCallback,
        error: errorCallback
    });
}

function insertProjectTableData(data) {
    var tableRow = $("#project-list-table").append(`
        <tr class="position-static">
            <td class="align-middle time white-space-nowrap ps-0 projectName py-4">
                <a class="fw-bold fs-0" href="/Projects/${data.projectId}/Details">${data.name}</a>
            </td>
            <td class="align-middle white-space-nowrap volunteers ps-3 py-4">
                0
            </td>
            <td class="align-middle white-space-nowrap start ps-3 py-4">
                <p class="mb-0 fs--1 text-900">Nov 17, 2020</p>
            </td>
            <td class="align-middle white-space-nowrap deadline ps-3 py-4">
                <p class="mb-0 fs--1 text-900">May 21, 2028</p>
            </td>
            <td class="align-middle white-space-nowrap task ps-3 py-4">
                <p class="fw-bo text-900 fs--1 mb-0">287</p>
            </td>
            <td class="align-middle white-space-nowrap ps-3 projectprogress">
                <p class="text-800 fs--2 mb-0">145 / 145</p>
                <div class="progress" style="height:3px;">
                    <div class="progress-bar bg-success" style="width: 100%" role="progressbar"
                        aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                </div>
            </td>
            <td class="align-middle white-space-nowrap text-end statuses">
                <span class="badge badge-phoenix fs--2 badge-phoenix-success">completed</span>
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

    return tableRow;
}

function loadCities(stateId) {
    $('#CityId').empty();

    $.ajax({
        url: `/States/${stateId}/GetCities`,
        success: (response) => {
            console.log(response);
            if (response != null && response != undefined && response.length > 0) {
                $('#CityId').attr('disabled', false);
                $('#CityId').append('<option value="-1">--Select City--</option>');
                $.each(response, (i, data) => {
                    $('#CityId').append(`<option value="${data.id}">${data.name}</option>`);
                });
            }
            else {
                $('#CityId').attr('disabled', true);
                $('#CityId').append('<option value="-1">--No Cities found--</option>');
            }
        },
        error: (error) => {
            alert(error);
        }
    });
}

function saveProject() {
    const projform = document.getElementById('project-form');

    projform.addEventListener('submit', (e) => {
        e.preventDefault();

        var myModalEl = document.getElementById('saveProjectModal');
        var modal = bootstrap.Modal.getInstance(myModalEl);
        const form = e.target;
        const formData = new FormData(form);
        const url = "/Projects/Create";

        function successCallback(response) {
            modal.hide();
            form.reset();
            insertProjectTableData(response);
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

function getProjectDtails() {
    var anchors = document.querySelectorAll('[data-project]');

    anchors.forEach(a => {
        a.addEventListener('click', (e) => {
            e.preventDefault();

            var myModalEl = document.getElementById('projectsBoardViewModal');
            var modal = new bootstrap.Modal(myModalEl, {});
            const projectId = a.dataset.project;
            const url = `/Projects/${projectId}/GetDetails`;
            var projectName = document.getElementById('projectName');
            var projectPhoto = document.getElementById('projectPhoto');
            var projectGoal = document.getElementById('projectGoal');

            function handleSuccess(response) {
                projectName.innerHTML = response.name;
                projectGoal.innerHTML = response.goal;
                projectPhoto.classList.add('details-img');
                projectPhoto.setAttribute('src', `/media/projects/${response.photo}`);
                modal.show();
                console.log('Request successful:', response);
              }
              
              function handleError(xhr, status, error) {
                console.log('Request failed:', error);
              }
              
            ajaxGet(url, handleSuccess, handleError);
        });
    });
}

function insertPositionTableData(data) {
    var tableRow = $("#position-list-table").append(`
        <tr class="hover-actions-trigger btn-reveal-trigger position-static">
            <td class="fs--1 align-middle py-0">
                <div class="form-check mb-0 fs-0">
                    <input class="form-check-input" type="checkbox" />
                </div>
            </td>
            <td class="position align-middle white-space-nowrap fw-semi-bold text-1100 ps-1 py-0">
                <div class="d-flex align-items-center position-relative">
                    <a class="text-1000 fw-bold stretched-link" href="#!">${data.title}</a>
                </div>
            </td>
            <td class="peopleNeeded align-middle white-space-nowrap fw-semi-bold text-1100 ps-4 py-0">
                <div class="d-flex align-items-center position-relative">
                    ${data.peopleNeeded}
                </div>
            </td>
            <td class="positionsFilled align-middle white-space-nowrap fw-semi-bold text-1100 ps-4 py-0">
                <div class="d-flex align-items-center position-relative">
                    0
                </div>
            </td>
            <td class="status align-middle white-space-nowrap fw-semi-bold text-1100 ps-4 py-0">
                <div class="d-flex align-items-center position-relative">
                    Open
                </div>
            </td>
            <td class="applicants align-middle white-space-nowrap fw-semi-bold text-1100 ps-4 py-0">
                <div class="d-flex align-items-center position-relative">
                    0
                </div>
            </td>
            <td class="align-middle white-space-nowrap text-end pe-0 ps-4">
                <div class="font-sans-serif btn-reveal-trigger position-static">
                    <button class="btn btn-sm dropdown-toggle dropdown-caret-none transition-none btn-reveal fs--2" type="button" data-bs-toggle="dropdown" data-boundary="window" aria-haspopup="true" aria-expanded="false" data-bs-reference="parent">
                        <span class="fas fa-ellipsis-h fs--2"></span>
                    </button>
                    <div class="dropdown-menu dropdown-menu-end py-2">
                        <a class="dropdown-item" href="#!">View</a>
                        <a class="dropdown-item" href="#!">Export</a>
                        <div class="dropdown-divider"></div>
                        <a class="dropdown-item text-danger" href="#!">Remove</a>
                    </div>
                </div>
            </td>
        </tr>
    `);
    return tableRow;
}

function savePosition() {
    var form = document.getElementById("position-form");
    form.addEventListener('submit', (e) => {
        e.preventDefault();

        var myModalEl = document.getElementById('savePositionModal');
        var modal = bootstrap.Modal.getInstance(myModalEl);
        const formData = new FormData(form);
        const url = "/Positions/Create";

        function successCallback(response) {
            modal.hide();
            form.reset();
            insertPositionTableData(response);
        }

        function errorCallback(xhr, status, error) {
            modal.hide();
            form.reset();
            console.log(error);
        }

        ajaxFormPost(url, formData, successCallback, errorCallback);

    });
}

function getPositionDetails() {
    var anchors = document.querySelectorAll('[data-position]');

    anchors.forEach(a => {
        a.addEventListener('click', (e) => {
            e.preventDefault();

            var myModalEl = document.getElementById('saveApplicantModal');
            var modal = new bootstrap.Modal(myModalEl, {});
            const positionId = a.dataset.position;
            const url = `/Positions/${positionId}/GetDetails`;
            var positionTitle = document.getElementById('positionTitle');
            var positionDetails = document.getElementById('positionDetails');

            function handleSuccess(response) {
                positionTitle.innerHTML = response.title;
                positionDetails.innerHTML = response.details;
                document.getElementById("TypeId").value = response.positionId;
                document.getElementById("ManagerId").value = response.managerId;
                modal.show();
                console.log('Request successful:', response);
            }
              
            function handleError(xhr, status, error) {
                console.log('Request failed:', error);
            }
              
            ajaxGet(url, handleSuccess, handleError);
        });
    });
}

function saveApplicant() {
    var form = document.getElementById("applicant-form");

    form.addEventListener('submit', (e) => {
        e.preventDefault();

        var myModalEl = document.getElementById('saveApplicantModal');
        var modal = bootstrap.Modal.getInstance(myModalEl);
        const formData = new FormData(form);
        const url = "/Messages/Create";

        function successCallback(response) {
            modal.hide();
            form.reset();
            console.log(response);
        }

        function errorCallback(xhr, status, error) {
            modal.hide();
            form.reset();
            console.log(error);
        }

        ajaxFormPost(url, formData, successCallback, errorCallback);
    });
}

function insertTeamTableData(data) {
    var tableRow = $("#team-list-table").append(`
        <tr class="hover-actions-trigger btn-reveal-trigger position-static">
            <td class="fs--1 align-middle py-0">
                <div class="form-check mb-0 fs-0">
                    <input class="form-check-input" type="checkbox" />
                </div>
            </td>
            <td class="align-middle applicant white-space-nowrap">
                <a class="d-flex align-items-center text-900" href="#!">
                    ${data.name}
                </a>
            </td>
            <td class="align-middle white-space-nowrap text-end pe-0 ps-4">
                <div class="font-sans-serif btn-reveal-trigger position-static">
                    <button class="btn btn-sm dropdown-toggle dropdown-caret-none transition-none btn-reveal fs--2" type="button" data-bs-toggle="dropdown" data-boundary="window" aria-haspopup="true" aria-expanded="false" data-bs-reference="parent">
                        <span class="fas fa-ellipsis-h fs--2"></span>
                    </button>
                    <div class="dropdown-menu dropdown-menu-end py-2">
                        <a class="dropdown-item" href="#!">View</a>
                        <a class="dropdown-item" href="#!">Export</a>
                        <div class="dropdown-divider"></div>
                        <a class="dropdown-item text-danger" href="#!">Remove</a>
                    </div>
                </div>
            </td>
        </tr> 
    `);

    return tableRow;
}

function saveTeam() {
    var form = document.getElementById("team-form");
    form.addEventListener('submit', (e) => {
        e.preventDefault();

        var myModalEl = document.getElementById('saveTeamModal');
        var modal = bootstrap.Modal.getInstance(myModalEl);
        const formData = new FormData(form);
        const url = "/Teams/Create";

        function successCallback(response) {
            modal.hide();
            form.reset();
            insertTeamTableData(response);
        }

        function errorCallback(xhr, status, error) {
            modal.hide();
            form.reset();
            console.log(error);
        }

        ajaxFormPost(url, formData, successCallback, errorCallback);
    });
}

function insertTeamName(name) {
    var tm = $("#team-assignment").html(`
        <a class="fw-semi-bold d-block lh-sm" href="#!" data-bs-toggle="modal" data-bs-target="#assignTeamModal">
            ${name} <span class="fa-solid fa-repeat ms-2 text-700 fs--1"></span>
        </a>
    `);
    return tm;
}

function assignTeam() {
    var form = document.getElementById("assign-team-form");
    form.addEventListener('submit', (e) => {
        e.preventDefault();

        var myModalEl = document.getElementById('assignTeamModal');
        var modal = bootstrap.Modal.getInstance(myModalEl);
        var objId = document.getElementById("objective-info").dataset.objective;
        const formData = new FormData(form);
        const url = `/Objectives/${objId}/Assign-Team`;

        function successCallback(response) {
            modal.hide();
            form.reset();
            insertTeamName(response);
        }

        function errorCallback(xhr, status, error) {
            modal.hide();
            form.reset();
            console.log(error);
        }

        ajaxFormPost(url, formData, successCallback, errorCallback);
    });
}

function insertTaskTableData(data) {
    var task = $("#task-list-table-body").prepend(`
        <tr class="position-static">
            <td class="align-middle time white-space-nowrap ps-0 name py-4">
                <a class="fw-bold fs-0" herf="#!">
                    ${data.name}
                </a>
            </td>
            <td class="align-middle white-space-nowrap assignees ps-3 py-4">
                0
            </td>
            <td class="align-middle white-space-nowrap start ps-3 py-4">
                <p class="mb-0 fs--1 text-900">Nov 17, 2020</p>
            </td>
            <td class="align-middle white-space-nowrap deadline ps-3 py-4">
                <p class="mb-0 fs--1 text-900">May 21, 2028</p>
            </td>
            <td class="align-middle white-space-nowrap ps-3 projectprogress">
                <p class="text-800 fs--2 mb-0">145 / 145</p>
                <div class="progress" style="height:3px;">
                    <div class="progress-bar bg-success" style="width: 100%" role="progressbar"
                        aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                </div>
            </td>
            <td class="align-middle white-space-nowrap text-end status">
                <span class="badge badge-phoenix fs--2 badge-phoenix-primary">
                    Draft
                </span>
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

    return task;
}

function saveActionItem() {
    var form = document.getElementById("action-item-form");
    form.addEventListener('submit', (e) => {
        e.preventDefault();

        var myModalEl = document.getElementById('saveActionItemModal');
        var modal = bootstrap.Modal.getInstance(myModalEl);
        const formData = new FormData(form);
        const url = "/Tasks/Create";

        function successCallback(response) {
            modal.hide();
            form.reset();
            insertTaskTableData(response);
        }

        function errorCallback(xhr, status, error) {
            modal.hide();
            form.reset();
            console.log(error);
        }

        ajaxFormPost(url, formData, successCallback, errorCallback);
    });
}

function insertAssigneeFormData(data, itemId) {
    var assignees = []
    data.forEach(element => {
        assignees.push(`<div class="form-check">
            <input class="form-check-input" name="Selected" type="checkbox" value="${element.userId}" ${element.assigned ? 'checked' : '' } />
            <label class="form-check-label">${element.name}</label>
        </div>`);
    });
    var fields = $("#add-assignee-form").html(assignees);
    fields.append(`<input name="ItemId" type="hidden" value="${ itemId }" />`);
    return fields;
}

function getAssignees() {

    var anchors = document.querySelectorAll('a[data-task]');
    
    anchors.forEach((a) => {
        a.addEventListener('click', (e) => {
            e.preventDefault();
            var target = e.target;

            var myModalEl = document.getElementById('assigneeModal');
            var modal = new bootstrap.Modal(myModalEl, {});
            const taskId = target.dataset.task;
            const url = `/Tasks/${taskId}/GetAssignees`;

            function handleSuccess(response) {
                console.log(response);
                insertAssigneeFormData(response, taskId);
                modal.show();
              }
              
            function handleError(xhr, status, error) {
                console.log('Request failed:', error);
            }
              
            ajaxGet(url, handleSuccess, handleError);
        });
    });

}

function assign() {
    var form = document.getElementById("add-assignee-form");
    form.addEventListener('submit', (e) => {
        e.preventDefault();

        var myModalEl = document.getElementById('assigneeModal');
        var modal = bootstrap.Modal.getInstance(myModalEl);
        const formData = new FormData(form);
        var data = {
            itemId: formData.get("ItemId"),
            selected: formData.getAll("Selected")
        }
        const url = "/Tasks/Assign";

        function successCallback(response) {
            modal.hide();
            console.log(response);
        }

        function errorCallback(xhr, status, error) {
            modal.hide();
            console.log(error);
        }

        sendAjaxRequest(url, data, successCallback, errorCallback);
    });
}

