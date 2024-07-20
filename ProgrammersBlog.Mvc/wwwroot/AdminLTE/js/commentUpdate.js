$(document).ready(function () {
    $('#articleList').select2();

});
$(document).ready(function () {
    $('#articleList').select2();
    $(document).on('click', '.btn-update', function (event) {
        event.preventDefault();
        const form = $('#comment-update-form');

        const isActiveCheckbox = form.find('input[name="IsActive"]');
        const isActiveValue = isActiveCheckbox.prop('checked');
        form.find('input[name="IsActive"]').val(isActiveValue);



        var saveAnimationDiv = document.getElementById('btn-save-animation');
        saveAnimationDiv.style.display = 'block';

        var saveSightDiv = document.getElementById('btn-save-sight');
        saveSightDiv.style.display = 'none';


        $.ajax({
            type: 'POST',
            url: '/Admin/Comment/Update',
            data: new FormData(form.get(0)),
            contentType: false,
            processData: false,
            success: function (data) {
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
                    $('.error-message').remove();
                    $.each(result, function (key, value) {
                        var inputElement = $('input[name="' + key + '"]');
                        inputElement.after('<p class="text-danger error-message"> ' + value + '</p>');
                        var textAreaElement = $('textArea[name="' + key + '"]');
                        textAreaElement.after('<p class="text-danger error-message"> ' + value + '</p>');

                    });
                }
                setTimeout(function () {
                    saveAnimationDiv.style.display = 'none';
                    saveSightDiv.style.display = 'block';
                }, 1700);
            },
            error: function (error) {

            }
        });
    });
})