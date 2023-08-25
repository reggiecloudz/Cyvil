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