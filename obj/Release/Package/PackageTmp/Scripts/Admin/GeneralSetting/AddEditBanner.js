function SaveGeneralSetting() {

    $.ajax({
        url: '/Admin/GeneralSetting/Save',
        type: "POST",
        data: $("#frmAddGeneralSetting").serialize(),
        success: function (data) {
            console.log(data);
        }
    });
}