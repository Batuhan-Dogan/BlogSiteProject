$(document).ready(function () {
    var pageNumber = 0;
    $(document).on('click', '.get-more-users-comments', function (event) {
        event.preventDefault();
        pageNumber += 1;
        const actionUrl = '/Comment/GetUsersComments?PageNumber=' + pageNumber+'&PageSize=12&';
        $.ajax({
            type: 'GET',
            url: actionUrl,
            success: function (data) {
                const result = jQuery.parseJSON(data);
                if ('ResultStatus' in result) {
                    if (result.ResultStatus == 0) {
                        result.Comments.forEach(function (comment) {
                            $('.comments-table').css({
                                "display": "block"
                            });
                            var commentTr = $('<tr></tr>');
                            var commentCreatedTime = $('<td></td>');
                            var commentText = $('<td></td>');
                            var deleteButton = $('<div class="btn btn-sm btn-danger" data-id' + comment.Id + '>Sil</div>');
                            var updateButton = $('<div class="btn btn-sm btn-warning" data-id' + comment.Id + '>Düzenle</div>');
                            var readMeButton = $('<div class="btn btn-sm btn-info" data-id' + comment.Id + '>Oku</div>');
                            var operations = $('<div class="btn-group"></div>');
                            operations.append(deleteButton);
                            operations.append(updateButton);
                            operations.append(readMeButton);

                            commentCreatedTime.text(comment.CreatedTime);
                            commentText.text(comment.Text);
                            commentTr.append(commentCreatedTime);
                            commentTr.append(commentText);
                            commentTr.append(operations);
                            $('.new-comment-area').prepend(commentTr);
                        });
                    }
                    else if (result.ResultStatus == 1) {
                        $(".get-more-users-comments").off("click");
                        $('.get-more-users-comments').css({
                            "color": "#red",
                            "cursor": "not-allowed",
                            "background-color":"darkgray"
                        });
                        $('.get-more-users-comments').removeClass('get-more-users-comments');
                    }
                    else {
                    }
                }
                else {
                    alert('resultStatus yok');
                }
                
            }
        });
    });
});
