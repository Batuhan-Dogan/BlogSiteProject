$(document).on('click', '.btn-add', function (event) {
    var newComment = $(`<div class="p-4 rounded-4 mt-2 new-Comment">
    <div class="row">Siz</div>
    <div class="comment-text mt-2 row lead"></div>
    <div class="row">Şimdi</div>
    </div>`);
    event.preventDefault();
    const form = $('#form-comment-add');
    $.ajax({
        url: '/Comment/Add/',
        type: 'POST',
        data: new FormData(form.get(0)),
        contentType: false,
        processData: false,
        success: function (data) {
            const result = jQuery.parseJSON(data);
            if ('ResultStatus' in result) {
                if (result.ResultStatus == 0) {
                    var message = form.find('input[name="Text"]').val();
                    $('.comments').prepend(newComment);
                    $('.comment-text').append(message);
                    $('.btn-add').css({
                        "pointer-events": "none",
                        "background-color": "orangered",
                    });

                    setTimeout(function () {
                        $(".new-Comment").css({
                            "opacity": "1",
                            "transition": "0.2s",
                        });
                        $('.btn-add').css({
                            "pointer-events": "",
                            "background-color": "",
                        });
                    }, 3000);
                } else if (result.ResultStatus == 1) {
                    alert('alarm hatalı');
                } else if (result.ResultStatus == 2) {
                    Swal.fire(
                        'hata!',
                        `${result.Message}`,
                        'warning'
                    );
                }
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Hatalı veri girişi yaptınız!',
                });
                $('.error-message').remove();
                $.each(result, function (key, value) {
                    var inputElement = $('.comment-Error');
                    inputElement.after('<p class="text-danger error-message"> ' + value + '</p>');
                });
            }
        }
    });
})