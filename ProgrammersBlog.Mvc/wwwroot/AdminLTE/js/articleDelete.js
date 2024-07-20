$(document).on('click', '.btn-delete', function (event) {
    event.preventDefault();
    const id = $(this).data('id');
    alert(id + 6);
    var x = '/Admin/Article/Delete/' + id;
    alert(x);
    Swal.fire({
        title: 'Silmek istediğinize emin misiniz?',
        text: 'Seçili makale silinmiş duruma getirilecektir',
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
                url: '/Admin/Article/Delete/' + id,
                success: function (data) {
                    const result = jQuery.parseJSON(data);
                    if (result.ResultStatus === 0) {
                        Swal.fire(
                            'Silindi!',
                            `${result.Message}`,
                            'success'
                        );
                        const tableRow = $(`[name="${id}"]`);
                        tableRow.fadeOut(1700);
                    }
                    else if (result.ResultStatus === 1) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Hay aksi',
                            text: `${result.Message}`,
                        })
                    }
                    else if (result.ResultStatus === 2) {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Hata',
                            text: `${result.Message}`,
                        })
                    }
                },
                error: function (err) { console.log(err); }
            });
        }
    });
})