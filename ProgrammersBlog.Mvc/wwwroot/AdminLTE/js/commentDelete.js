$(document).on('click', '.btn-delete', function (event) {
    event.preventDefault();
    const id = $(this).data('id'); 
    $.ajax({
        type: 'POST',
        url: '/Admin/Comment/Delete/' + id,
        success: function (data) {
            result = jQuery.parseJSON(data);
            if (result.ResultStatus === 0) {
                Swal.fire(
                    'Silindi!',
                    `${result.Message}`,
                    'success'
                );
            }
            else if (result.ResultStatus === 1) {
                Swal.fire(
                    'hay aksi!',
                    `${result.Message}`,
                    'error'
                );
            }
            else if (result.ResultStatus === 2) {
                Swal.fire(
                    'hata!',
                    `${result.Message}`,
                    'warning'
                );
            }
        }
    });
})