function SaveGeneralSetting(isEditMode) {
    var isFormvalid = $("#frmAddGeneralSetting").valid(); 

    if (isFormvalid) {
        $.ajax({
            url: '/Admin/GeneralSetting/Save',
            type: "POST",
            data: $("#frmAddGeneralSetting").serialize(),
            success: function (data) {
                debugger;
                window.location.href = "/Admin/GeneralSetting/Edit?id=1&status=u";
            }
        });
    }
    
}