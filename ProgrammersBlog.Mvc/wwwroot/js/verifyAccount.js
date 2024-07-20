var isEmailConfirmed = $('#email-Confirmed').data("id");
var isPhoneNumberConfirmed = $('#phoneNumber-Confired').data("id");
if (isEmailConfirmed != false && isPhoneNumberConfirmed != false) {
    Swal.fire({
        title: "MyBlog.com'da sizde yazmak istermisiniz?",
        text: "MyBlog.com'un Ortak Programı, kişilerin kişisel ifade, bilgi paylaşımı ve hikaye anlatımı aracılığıyla dünya kolektif bilgeliğini derinleştirmemize yardımcı olmak isteyenler içindir. Sizde tecrübe ve bilgileriniz paylaşmak isterseniz aramaza katılmak için hesabınızı doğrulayın.",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Hesabınızı doğrulayın",
        cancelButtonText: "Belki daha sonra"
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });
}
