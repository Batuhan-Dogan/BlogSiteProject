$(document).on('click', '.btn-save', function (event) {
    event.preventDefault();
    const form = $('#form-user-update');
    const dataToSend = new FormData(form.get(0));
    $.ajax({
        type: 'POST',
        url: '/Admin/User/UpdateUser',
        data: dataToSend,
        processData: false,
        contentType: false,
        success: function (data) {
            const result = jQuery.parseJSON(data);
            if ('ResultStatus' in result)
            {
                if (result.ResultStatus == 0) {
                    Swal.fire({
                        position: 'top',
                        icon: 'success',
                        title: `${result.Message}`,
                        showConfirmButton: false,
                        timer: 3500
                    });
                }
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hatalı veri girişi yaptınız !',
                });
                $('.error-message').remove();
                $.each(result, function (key, value) {
                    var inputElement = $('input[name="' + key + '"]');
                    inputElement.after('<p class="text-danger error-message"> ' + value + '</p>');
                });
            }
        }
    });
});
