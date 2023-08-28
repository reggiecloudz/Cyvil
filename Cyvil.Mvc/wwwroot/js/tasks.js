function insertTaskTableData(data) {
    var task = $("#task-list-table-body").prepend(`
        <tr class="position-static">
            <td class="align-middle time white-space-nowrap ps-0 name py-4">
                <a class="fw-bold fs-0" href="/Tasks/${data.id}/Details">
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

function insertSubtaskListData(data) {
    const tr = $("#").append(`
        <div class="d-flex hover-actions-trigger py-2 border-bottom">
            <input class="form-check-input form-check-input-todolist flex-shrink-0 my-1 me-2 form-check-input-undefined" type="checkbox" id="checkbox-todo-0" data-event-propagation-prevent="data-event-propagation-prevent" />
            <div class="row justify-content-between align-items-md-center btn-reveal-trigger border-200 gx-0 flex-1 cursor-pointer" data-bs-toggle="modal" data-bs-target="#exampleModal">
                <div class="col-12 col-md-auto col-xl-12 col-xxl-auto">
                    <div class="mb-1 mb-md-0 d-flex align-items-center lh-1">
                        <label class="form-check-label mb-1 mb-md-0 mb-xl-1 mb-xxl-0 fs-0 me-2 line-clamp-1 text-900 cursor-pointer">
                            ${data.name}
                        </label>
                        <span class="badge badge-phoenix ms-auto fs--2 badge-phoenix-primary">${data.status}</span>
                    </div>
                </div>
                <div class="col-12 col-md-auto col-xl-12 col-xxl-auto">
                    <div class="d-flex lh-1 align-items-center">
                        <a class="text-700 fw-bold fs--2 me-2" href="#!">
                            <span class="fas fa-paperclip me-1"></span>2
                        </a>
                        <p class="text-700 fs--2 mb-md-0 me-2 me-md-3 me-xl-2 me-xxl-3 mb-0">${data.deadlineDate}</p>
                        <div class="hover-md-hide hover-xl-show hover-xxl-hide">
                            <p class="text-700 fs--2 fw-bold mb-md-0 mb-0 ps-md-3 ps-xl-0 ps-xxl-3 border-start-md border-xl-0 border-start-xxl border-300">
                            ${data.deadlineTime}
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="d-none d-md-block d-xl-none d-xxl-block end-0 position-absolute" style="top: 23%;" data-event-propagation-prevent="data-event-propagation-prevent">
                <div class="hover-actions end-0" data-event-propagation-prevent="data-event-propagation-prevent">
                    <button class="btn btn-phoenix-secondary btn-icon me-1 fs--2 text-900 px-0 me-1" data-event-propagation-prevent="data-event-propagation-prevent">
                        <span class="fas fa-edit"></span>
                    </button>
                    <button class="btn btn-phoenix-secondary btn-icon fs--2 text-danger px-0" data-event-propagation-prevent="data-event-propagation-prevent">
                        <span class="fas fa-trash"></span>
                    </button>
                </div>
            </div>
        </div>
    `);

    return tr;
}

function saveSubtask() {
    var form = document.getElementById("subtask-form");
    form.addEventListener('submit', (e) => {
        e.preventDefault();

        var myModalEl = document.getElementById('saveSubtaskModal');
        var modal = bootstrap.Modal.getInstance(myModalEl);
        const formData = new FormData(form);
        const url = "/Tasks/New-Subtask";

        function successCallback(response) {
            modal.hide();
            form.reset();
            insertSubtaskListData(response);
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