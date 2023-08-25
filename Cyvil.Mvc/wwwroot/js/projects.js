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