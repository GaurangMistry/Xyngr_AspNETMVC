function SaveStaticPages(isEditMode) {
    var pageContent = CKEDITOR.instances.txtPageContent.getData();
    // convert form data to array
    var data = $("#frmAddStaticPage").serializeArray();
    // using ES6
    data.find(item => item.name === 'PageContent').value = pageContent;

    var isFormvalid = $("#frmAddStaticPage").valid();   
    if (isFormvalid) {
        $.ajax({
            url: '/Admin/StaticPage/Save',
            type: "POST",
            data: $.param(data),
            success: function (data) {
                debugger;
                if (isEditMode)
                    window.location.href = "/Admin/StaticPage/index/u";
                else
                    window.location.href = "/Admin/StaticPage/index/s";
            }
        });
    }
}