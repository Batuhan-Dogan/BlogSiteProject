$(document).ready(function () {
    var table = $('#categoriesTable').DataTable({
        dom: "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        language:{
            "sDecimal":        ",",
            "sEmptyTable":     "Tabloda herhangi bir veri mevcut değil",
            "sInfo":           "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "sInfoEmpty":      "Kayıt yok",
            "sInfoFiltered":   "(_MAX_ kayıt içerisinden bulunan)",
            "sInfoPostFix":    "",
            "sInfoThousands":  ".",
            "sLengthMenu":     "Sayfada _MENU_ kayıt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sProcessing":     "İşleniyor...",
            "sSearch":         "Ara:",
            "sZeroRecords":    "Eşleşen kayıt bulunamadı",
            "oPaginate": {
                "sFirst":    "İlk",
                "sLast":     "Son",
                "sNext":     "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending":  ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        }
    });
    $(document).ready(function () {
        $("#reload").click(function () {
            $("#btnLoading").css("display", "block");
            setTimeout(function () {
                location.reload();
            }, 1500);
        });
    });
    $(document).on('click', '.btn-delete', function (event) {
        event.preventDefault();
        const id = $(this).data('id'); 
        Swal.fire({
            title: 'Silmek istediğinize emin misiniz?',
            text: 'Seçili kategori silinecektir',
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
                    data: { categoryId: id },
                    url: '/Admin/Category/Delete/',
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
                        else if (result.ResultStatus===1) {
                            Swal.fire({
                                icon: 'error',
                                title: 'Hay aksi',
                                text: `${ result.Message }`,
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
    });
});
