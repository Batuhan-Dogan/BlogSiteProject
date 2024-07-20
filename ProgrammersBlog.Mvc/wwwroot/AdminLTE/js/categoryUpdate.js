$(document).on('click', '.btn-save', function (event) {
    const form = $('#form-category-update');

    const isActiveCheckbox = form.find('input[name="IsActive"]');
    const isActiveValue = isActiveCheckbox.prop('checked');
    form.find('input[name="IsActive"]').val(isActiveValue);

    const isDeletedCheckbox = form.find('input[name="IsDeleted"]');
    const isDeletedValue = isDeletedCheckbox.prop('checked');
    form.find('input[name="IsDeleted"]').val(isDeletedValue);

    $.ajax({
        type: 'POST',
        url: '/Admin/Category/UpdateCategory',
        data: new FormData(form.get(0)),
        contentType: false,
        processData: false,
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
                        timer: 1700
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
        },
        error: function (error) {

        }
    });
    return false;
    event.preventDefault();

});