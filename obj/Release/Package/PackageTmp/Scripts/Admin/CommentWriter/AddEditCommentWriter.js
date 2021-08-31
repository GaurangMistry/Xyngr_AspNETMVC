function SaveCommentWriter(isEditMode) {
    if ($(".dz-preview").length > 0) {
        myDropzone.processQueue();
        myDropzone.on("complete", function (file) {
            debugger;
            if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                var data = $("#frmAddCommentWriter").serializeArray();

               // data.push({ name: 'Image', value: file.name })
                save(isEditMode, data);
            }
        });
    }
    else {
        var data = $("#frmAddCommentWriter").serializeArray();
        save(isEditMode, data);
    }
}

function save(isEditMode, data) {
    var isFormvalid = $("#frmAddCommentWriter").valid();

    if (isFormvalid) {
        $.ajax({
            url: '/Admin/CommentWriter/Save',
            type: "POST",
            data: $.param(data),

            success: function (data) {
                if (isEditMode)
                    window.location.href = "/Admin/CommentWriter/index/u";
                else
                    window.location.href = "/Admin/CommentWriter/index/s";
            }
        });
    }
}