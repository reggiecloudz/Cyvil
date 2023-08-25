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