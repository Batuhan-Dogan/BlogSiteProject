$(document).on('click', '.btn-remove', function (event) {
    event.preventDefault();
    const id = $(this).data('id');
    Swal.fire({
        title: 'Silmek istediğinize emin misiniz?',
        text: 'Seçili kullanıcı silinecektir',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: 'red',
        cancelButtonColor: 'green',
        confirmButtonText: 'Evet, silmek istiyorum',
        cancelButtonText: 'Hayır, silmek istemiyorum'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                data: { userId: id },
                url: '/Admin/User/Delete/',
                success: function (data) {
                    var result = jQuery.parseJSON(data);
                    if (result.ResultStatus === 0) {
                        Swal.fire(
                            'Silindi!',
                            `${result.Message}`,
                            'success'
                        );
                    } else if (result.ResultStatus == 1) {
                        Swal.fire(
                            'hay aksi!',
                            `${result.Message}`,
                            'error'
                        );
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        }
    });
});
