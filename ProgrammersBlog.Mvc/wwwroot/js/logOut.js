$(document).on('click', '.btn-logOut', function (event) {
    event.preventDefault();
    Swal.fire({
        title: "Çıkış yapmak istediğine emin misin?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "red",
        cancelButtonColor: "green",
        cancelButtonText: "iptal et",
        confirmButtonText: "evet, çıkış yap"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/Account/LogOut",
                success: function (result) {
                    let timerInterval;
                    Swal.fire({
                        title: "Başarıyla çıkış yaptınız",
                        html: "Bu sayfadan ayrılıyorsunuz ... <b></b> ",
                        timer: 2000,
                        timerProgressBar: true,
                        didOpen: () => {
                            Swal.showLoading();
                            const timer = Swal.getPopup().querySelector("b");
                            timerInterval = setInterval(() => {
                                timer.textContent = `${Swal.getTimerLeft()}`;
                            }, 100);
                        },
                        willClose: () => {
                            clearInterval(timerInterval);
                        }

                    }).then((result) => {
                        /* Read more about handling dismissals below */
                        if (result.dismiss === Swal.DismissReason.timer) {
                            console.log("I was closed by the timer");
                        }
                        location.reload();
                    });
                },
                error: function (error) {
                    // Hata durumunda yapılacak işlemler
                    alert("Metod çağrılırken bir hata oluştu.");
                }
            });
        }
    });
});