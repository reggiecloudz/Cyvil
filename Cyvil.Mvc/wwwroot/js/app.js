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
    console.log(JSON.stringify(data))
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
            <td class="position align-middle white-space-nowrap fw-semi-bold text-1100 ps-4 py-0">
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
                    Closed
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
                document.getElementById("PositionId").value = response.positionId;
                document.getElementById("ProjectId").value = response.projectId;
                console.log(response.projectId);
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
        const url = "/Positions/Apply";

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

function showApplicant() {
    var anchors = document.querySelectorAll('a[data-applicant]');
    console.log(anchors);
    anchors.forEach((a) => {
        a.addEventListener('click', (e) => {
            e.preventDefault();
            var app = e.target;

            var myModalEl = document.getElementById('showApplicantModal');
            var modal = new bootstrap.Modal(myModalEl, {});
            const applicantId = app.dataset.applicant;
            const url = `/Applicants/${applicantId}/Show`;

            function handleSuccess(response) {
                var projectPhoto = document.getElementById('projectPhoto');
                var userPhoto = document.getElementById('userPhoto');
                var userFullName = document.getElementById('userFullName');
                var appStatus = document.getElementById('appStatus');
                // projectPhoto.classList.add('details-img');
                projectPhoto.setAttribute('style', `background-image:url(/media/projects/${response.projectPhoto});background-position:top center;object-fit: cover;`);
                userPhoto.setAttribute('src', `/media/members/${response.userPhoto}`);
                userFullName.innerHTML = response.userFullName;
                appStatus.innerHTML = response.applicantStatus;
                document.getElementById('select-applicant').setAttribute('data-appmodal', applicantId);

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

function selectApplicant() {
    const element = document.getElementById('select-applicant');

    element.addEventListener('click', (e) => {

        e.preventDefault()
        var link = e.target;
        var myModalEl = document.getElementById('showApplicantModal');
        var modal = bootstrap.Modal.getInstance(myModalEl);
        const url = `/Applicants/${link.dataset.appmodal}/Select`

        function handleSuccess(response) {
            document.getElementById(`status-${link.dataset.appmodal}`).innerHTML = response;
            modal.hide();
            console.log('Request successful:', response);
        }
        
        function handleError(xhr, status, error) {
            console.log('Request failed:', error);
        }
        
        ajaxGet(url, handleSuccess, handleError);

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

function insertObjectiveTableData(data) {
    $("#objective-list").append(`<div class="d-flex hover-actions-trigger py-3 border-top">
        <input class="form-check-input form-check-input-todolist flex-shrink-0 my-1 me-2 form-check-input-undefined"
            type="checkbox" id="checkbox-todo-${data.id}" data-event-propagation-prevent="data-event-propagation-prevent" />
        <div class="row justify-content-between align-items-md-center border-200 gx-0 flex-1 cursor-pointer">
            <div class="col-12 col-md-auto col-xl-12 col-xxl-auto">
                <div class="mb-1 mb-md-0 d-flex align-items-center lh-1">
                    <label class="form-check-label mb-1 mb-md-0 mb-xl-1 mb-xxl-0 fs-0 me-2 line-clamp-1 text-900 cursor-pointer">
                        <a href="/Objectives/${data.id}/Details">${data.name}</a>
                    </label>
                    <span class="badge badge-phoenix ms-auto fs--2 badge-phoenix-primary">DRAFT</span>
                </div>
            </div>
            <div class="col-12 col-md-auto col-xl-12 col-xxl-auto mt-2">
                <div class="d-flex lh-1 align-items-center">
                    <a class="text-700 fw-bold fs--2 me-2" href="#!">
                        <span class="fas fa-people-group me-1"></span>2
                    </a>
                    <a class="text-primary fw-bold fs--2 me-2" href="#!">
                        <span class="fas fa-tasks me-1"></span>3
                    </a>
                    <p class="text-700 fs--2 mb-md-0 me-2 me-md-3 me-xl-2 me-xxl-3 mb-0">12 Nov, 2021</p>
                    <div class="hover-md-hide hover-xl-show hover-xxl-hide">
                        <p class="text-700 fs--2 fw-bold mb-md-0 mb-0 ps-md-3 ps-xl-0 ps-xxl-3 border-start-md border-xl-0 border-start-xxl border-300">
                            12:00 PM
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <div class="d-none d-md-block d-xl-none d-xxl-block end-0 position-absolute" style="top: 23%;"
            data-event-propagation-prevent="data-event-propagation-prevent">
            <div class="hover-actions end-0" data-event-propagation-prevent="data-event-propagation-prevent">
                <button class="btn btn-phoenix-secondary btn-icon me-1 fs--2 text-900 px-0 me-1"
                    data-event-propagation-prevent="data-event-propagation-prevent">
                    <span class="fas fa-edit"></span>
                </button>
                <button class="btn btn-phoenix-secondary btn-icon fs--2 text-danger px-0"
                    data-event-propagation-prevent="data-event-propagation-prevent">
                    <span class="fas fa-trash"></span>
                </button>
            </div>
        </div>
    </div>`)
}

function saveObjective() {
    var form = document.getElementById("objective-form");
    form.addEventListener('submit', (e) => {
        e.preventDefault();

        var myModalEl = document.getElementById('saveObjectiveModal');
        var modal = bootstrap.Modal.getInstance(myModalEl);
        const formData = new FormData(form);
        const url = "/Objectives/Create";

        function successCallback(response) {
            modal.hide();
            form.reset();
            insertObjectiveTableData(response);
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