function SaveBranch(isEditMode) {
    if ($(".dz-preview").length > 0) {
        myDropzone.processQueue();
        myDropzone.on("complete", function (file) {
            debugger;
            if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                var data = $("#frmAddBranch").serializeArray();

                //data.push({ name: 'ProfileImage', value: file.name })
                save(isEditMode, data);
            }
        });
    }
    else {
        var data = $("#frmAddBranch").serializeArray();
        save(isEditMode, data);
    }
}

function save(isEditMode, data) {
    var description = CKEDITOR.instances.txtDescription.getData();
    var openingHours = CKEDITOR.instances.txtOpeningHours.getData();
    
    data.find(item => item.name === 'Description').value = description;
    data.find(item => item.name === 'OpeningHours').value = openingHours;

    var isFormvalid = $("#frmAddBranch").valid();

    if (isFormvalid) {
        $.ajax({
            url: '/Admin/Branch/Save',
            type: "POST",
            data: $.param(data),
            success: function (data) {
                debugger;
                if (isEditMode)
                    window.location.href = "/Admin/Branch/index?id=u&buid=" + $("#BusinessID").val();
                else
                    window.location.href = "/Admin/Branch/index?id=s&buid=" + $("#BusinessID").val();
            }
        });
    }
}