function LoginValidate() {
    var isFormvalid = $("#frmLogin").valid();

    if (isFormvalid) {
        $.ajax({
            url: '/Admin/Account/Login',
            type: "POST",
            data: $("#frmLogin").serializeArray(),
            success: function (data) {
                window.location.href = "/Admin/User/Index";
            },
            error: function (data) {
                $("#lblerror").html("Invalid UserName/Password or Inactivate account.")
                return false;
            }
        });
    }
}
