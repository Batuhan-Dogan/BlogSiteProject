$(document).ready(function () {
    var pageNumber = 1;
    $(document).on('click', '.get-more-comment', function (event) {
        event.preventDefault();
        pageNumber += 1;
        const articleId = $('#ArticleId').val();
        const actionUrl = '/Comment/GetCommentsByResquestParameters?PageNumber=' + pageNumber+'&PageSize=12&ArticleId='+articleId+'';
        $.ajax({
            type: 'GET',
            url: actionUrl,
            success: function (data) {
                const result = jQuery.parseJSON(data);
                if ('ResultStatus' in result) {
                    if (result.ResultStatus == 0) {
                        result.Comments.forEach(function (comment) {
                            var commentDiv = $('<div class="p-4 rounded-4 text-center mt-2" style="background-color:#0000002b"></div>');
                            var row = $('<div class="row"></div>');
                            var row_2 = $('<div class="row"></div>');
                            row.text(comment.CreatedByName);
                            row_2.text(comment.Text);
                            commentDiv.append(row);
                            commentDiv.append(row_2);
                            $('.new-comments-container').prepend(commentDiv);
                        });
                    }
                    else if (result.ResultStatus == 1) {
                        $(".get-more-comment").off("click");
                        $('.get-more-comment').text('Daha Fazla Yorum Bulunamadı');
                        $('.get-more-comment').css({
                            "color": "#ac2b2b", 
                            "border": "3px solid #ac2b2b",
                            "pointer-events": "none"
                        });
                        $('.get-more-comment').removeClass('get-more-comment');
                    }
                    else {
                    }
                }
                
            }
        });
    });
});
