$(document).ready(function () {
    BindBranchDropdownByBusinessID($("#BusinessID").val())
    $("#BusinessID").change(function () {
        BindBranchDropdownByBusinessID($("#BusinessID").val())
    });
});

function SaveMediaGallery(isEditMode) {
    var isFormvalid = $("#frmAddMediaGallery").valid();   
    if (isFormvalid) {
        $.ajax({
            url: '/Admin/MediaGallery/Save',
            type: "POST",
            data: $("#frmAddMediaGallery").serialize(),
            success: function (data) {
                debugger;
                if (isEditMode)
                    window.location.href = "/Admin/MediaGallery/index?id=u&buid=" + $("#BusinessID").val() + "&bid=" + $("#ddlBranch").val();
                else
                    window.location.href = "/Admin/MediaGallery/index?id=s&buid=" + $("#BusinessID").val() + "&bid=" + $("#ddlBranch").val();
            }
        });
    }
}

function BindBranchDropdownByBusinessID(BusinessID) {
    $("#ddlBranch").empty();
    $.ajax({
        type: "POST",
        url: "/Admin/Branch/GetBranchByBusinessID",
        data: { "BusinessID": BusinessID},
        success: function (res) {
            $.each(res, function (data, value) {
                var selected = "";
                if ($("#hdnBranchID").val() != null) {
                    if ($("#hdnBranchID").val() == value.BranchID) {
                        selected = "selected='selected'";
                    }
                    else
                        selected = "";
                }
                $("#ddlBranch").append($("<option " + selected + "></option>").val(value.BranchID).html(value.Address2));
            })

            //$('#ddlBranch option:first-child').attr("selected", "selected");
            $("#hdnBranchID").val($("#ddlBranch").val());
        }

    });
}

$(document).on('change', '#ddlBranch', function () {
    $("#hdnBranchID").val($("#ddlBranch").val());
});