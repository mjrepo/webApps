$(function () {
    $('#fileupload').fileupload({
        //add: function (e, data) {
        //    data.context = $('<p/>').text('Uploading...').appendTo(document.body);
        //    data.submit();
        //},
        done: function (e, data) {
            $.each(data.files, function (index, file) {
                $('<p/>').text(file.name).appendTo(document.body).append('<img src="' + URL.createObjectURL(file) + '"/>');
            });
        },
        progressall: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            $('#progress .progress-bar').css(
                'width',
                progress + '%'
            );
        }
    });
});