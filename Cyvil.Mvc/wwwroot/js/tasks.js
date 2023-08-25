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