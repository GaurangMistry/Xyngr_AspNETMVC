function SaveCategory(isEditMode) {
    if ($(".dz-preview").length > 0) {
        myDropzone.processQueue();
        myDropzone.on("complete", function (file) {
            debugger;
            if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                var data = {
                    "categoryID": $("#CategoryID").val(),
                    "parentCatID": $("#ParentCategoryID").val(),
                    "categoryName": $("#txtCategory").val(),
                    "status": $("#Status").val(),
                    "MetaAuthorContent": $("#txtMetaAuthorContent").val(),
                    "MetaDescContent": $("#txtMetaDescContent").val(),
                    "MetaKeyWordContent": $("#txtMetaKeyWordContent").val(),
                    "Image": $("#hdnfileName").val() 
                }
                save(isEditMode, data);
            }
        });
    }
    else {
        var data = {
            "categoryID": $("#CategoryID").val(),
            "parentCatID": $("#ParentCategoryID").val(),
            "categoryName": $("#txtCategory").val(),
            "status": $("#Status").val(),
            "MetaAuthorContent": $("#txtMetaAuthorContent").val(),
            "MetaDescContent": $("#txtMetaDescContent").val(),
            "MetaKeyWordContent": $("#txtMetaKeyWordContent").val()
        }
        save(isEditMode, data);
    }
}

function save(isEditMode, frmdata) {
    var isFormvalid = $("#frmAddCategory").valid();
    if (isFormvalid) {
        $.ajax({
            url: '/Admin/Category/Save',
            type: "POST",
            data: frmdata,
            cache: false,
            success: function (data) {
                if (!data.success) {
                    $("#lblMessage").html(data.message);
                    return false;
                }
                debugger;
                if (isEditMode)
                    window.location.href = "/Admin/Category/index/u";
                else
                    window.location.href = "/Admin/Category/index/s";
            }
        });
    }
}