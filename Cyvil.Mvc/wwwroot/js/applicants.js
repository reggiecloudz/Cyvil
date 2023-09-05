function saveApplicant() {
    var form = document.getElementById("applicant-form");

    form.addEventListener('submit', (e) => {
        e.preventDefault();

        var myModalEl = document.getElementById('saveApplicantModal');
        var modal = bootstrap.Modal.getInstance(myModalEl);
        const formData = new FormData(form);
        const url = "/Applicants/Create";

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

function interviewApplicant() {

    var anchors = document.querySelectorAll('a[data-interview-applicant]');
    
    anchors.forEach((a) => {
        a.addEventListener('click', (e) => {
            e.preventDefault();
            var target = e.target;

            const applicantId = target.dataset.interviewApplicant;
            const url = `/Applicants/${applicantId}/Interview`;

            function handleSuccess(response) {
                document.getElementById("applicant-status").innerHTML = response.status;
                target.classList.add("disabled");
                console.log(response.message);
              }
              
            function handleError(xhr, status, error) {
                console.log('Request failed:', error);
            }
              
            ajaxGet(url, handleSuccess, handleError);
        });
    });

}

function selectApplicant() {

    var anchors = document.querySelectorAll('a[data-select-applicant]');
    
    anchors.forEach((a) => {
        a.addEventListener('click', (e) => {
            e.preventDefault();
            var target = e.target;

            const applicantId = target.dataset.selectApplicant;
            const url = `/Applicants/${applicantId}/Select`;

            function handleSuccess(response) {
                document.getElementById("applicant-status").innerHTML = response.status;
                document.getElementById("people-needed").innerHTML = response.peopleNeeded;
                document.getElementById("positions-filled").innerHTML = response.positionsFilled;
                document.getElementById("applicant-count").innerHTML = response.applicantCount;
                target.classList.add("disabled");
                console.log(response.message);
              }
              
            function handleError(xhr, status, error) {
                console.log('Request failed:', error);
            }
              
            ajaxGet(url, handleSuccess, handleError);
        });
    });

}