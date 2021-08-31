$(document).ready(function () {
    BindBranchDropdownByBusinessID($("#BusinessID").val())
    $("#BusinessID").change(function () {
        BindBranchDropdownByBusinessID($("#BusinessID").val())
    });
});

function SaveImageGallery(isEditMode) {
    if ($(".dz-preview").length > 0) {
        myDropzone.processQueue();

        myDropzone.on("complete", function (file) {
            debugger;
            if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                var data = $("#frmAddImageGallery").serializeArray();

                //data.push({ name: 'Image', value: file.name })
                save(isEditMode, data);
            }
        });
    }
    else {
        var data = $("#frmAddImageGallery").serializeArray();
        save(isEditMode, data);
    }
}

function save(isEditMode, data) {
    var isFormvalid = $("#frmAddImageGallery").valid();   
    if (isFormvalid) {
        $.ajax({
            url: '/Admin/ImageGallery/Save',
            type: "POST",
            data: $.param(data),
            success: function (data) {
                debugger;
                if (isEditMode)
                    window.location.href = "/Admin/ImageGallery/index?id=u&buid=" + $("#BusinessID").val() + "&bid=" + $("#ddlBranch").val();
                else
                    window.location.href = "/Admin/ImageGallery/index?id=s&buid=" + $("#BusinessID").val() + "&bid=" + $("#ddlBranch").val();
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