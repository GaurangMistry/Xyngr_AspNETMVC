function SaveUser(isEditMode) {
    var isFormvalid = $("#frmAddUser").valid();
    debugger;
    if (isFormvalid) {
        $.ajax({
            url: '/Admin/User/Save',
            type: "POST",
            data: $("#frmAddUser").serialize(),
            success: function (data) {
                if (!data.success) {
                    $("#lblMessage").html(data.message);
                    return false;
                }
                debugger;
                if (isEditMode)
                    window.location.href = "/Admin/User/index/u";
                else
                    window.location.href = "/Admin/User/index/s";
            }
        });
    }
}