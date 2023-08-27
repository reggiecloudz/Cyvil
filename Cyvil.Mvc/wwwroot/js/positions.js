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
                document.getElementById("PositionId").value = response.positionId;
                document.getElementById("ProjectId").value = response.projectId;
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
