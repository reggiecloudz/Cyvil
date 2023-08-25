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
