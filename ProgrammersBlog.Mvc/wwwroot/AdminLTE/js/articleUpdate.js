$(document).on('click', '.btn-save', function (event) {
    const form = $('#form-article-update');

    const isActiveCheckbox = form.find('input[name="IsActive"]');
    const isActiveValue = isActiveCheckbox.prop('checked');
    form.find('input[name="IsActive"]').val(isActiveValue);

    const isDeletedCheckbox = form.find('input[name="IsDeleted"]');
    const isDeletedValue = isDeletedCheckbox.prop('checked');
    form.find('input[name="IsDeleted"]').val(isDeletedValue);

    const dataToSend = new FormData(form.get(0));

    var saveAnimationDiv = document.getElementById('btn-save-animation');
    saveAnimationDiv.style.display = 'block';

    var saveSightDiv = document.getElementById('btn-save-sight');
    saveSightDiv.style.display = 'none';

    $.ajax({
        type: 'POST',
        url: '/Admin/Article/Update',
        data: dataToSend,
        processData: false,
        contentType: false,
        success: function (data)
        {
            const result = jQuery.parseJSON(data);
            if ('ResultStatus' in result) {
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
                $.each(result, function (key, value) {
                    var inputElement = $('input[name="' + key + '"]');
                    inputElement.after('<p class="text-danger error-message"> ' + value + '</p>');
                });
            }
            setTimeout(function () {
                saveAnimationDiv.style.display = 'none';
                saveSightDiv.style.display = 'block';
            }, 1700);
        }
    });
    event.preventDefault();
    return false;
})