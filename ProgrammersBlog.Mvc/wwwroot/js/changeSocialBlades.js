$(document).on('click', '.btn-change-social-blades', function (event) {
    event.preventDefault();
    var formData = $('#form-social-blades').serialize();
    $.ajax({
        type: 'POST', // veya GET
        url: '/User/EditSocialBlades', // Formun gönderileceği controller ve action
        data: formData,
        success: function (response) {
            const result = jQuery.parseJSON(response);
            if ('ResultStatus' in result) {

                if (result.ResultStatus == 0) {
                    $('.error-message').text('');
                    Swal.fire({
                        position: 'top',
                        icon: 'success',
                        title: `${result.Message}`,
                        showConfirmButton: false,
                        timer: 1700
                    }); 
                }
                else{
                    Swal.fire({
                        position: 'top',
                        icon: 'error',
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
                inputElement.after('<p class="text-danger error-message p-2"> ' + value + '</p>');
                            });
                        }
                    },
        error: function (xhr, status, error) {
            // Hata durumunda yapılacak işlemler
            console.error(xhr.responseText);
                }
            });
});
