$(document).ready(function () {
    BindCategoryDropdownByParentID($("#ParentCategoryID").val())
    $("#ParentCategoryID").change(function () {
        BindCategoryDropdownByParentID($("#ParentCategoryID").val())
    });
});

function SaveBusiness(isEditMode) {
    if ($(".dz-preview").length > 0) {
        myDropzone.processQueue();
        myDropzone.on("complete", function (file) {
            debugger;
            if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                var data = $("#frmAddBusiness").serializeArray();

                //data.push({ name: 'Image', value: file.name })
                save(isEditMode, data);
            }
        });
    }
    else {
        var data = $("#frmAddBusiness").serializeArray();
        save(isEditMode, data);
    }
}

function save(isEditMode, data) {
    var isFormvalid = $("#frmAddBusiness").valid();
    if (isFormvalid) {
        $.ajax({
            url: '/Admin/Business/Save',
            type: "POST",
            data: $.param(data),
            success: function (data) {
                if (!data.success) {
                    $("#lblMessage").html(data.message);
                    return false;
                }
                debugger;
                if (isEditMode)
                    window.location.href = "/Admin/Business/index/?id=u&pcid=" + $("#ParentCategoryID").val();
                else
                    window.location.href = "/Admin/Business/index/?id=s&pcid=" + $("#ParentCategoryID").val();
            }
        });
    }
}


function BindCategoryDropdownByParentID(ParentCategoryID) {
    $("#ddlCategory").empty();
    $.ajax({
        type: "POST",
        url: "/Admin/Category/GetCategoriesByParentID",
        data: { "parentCategoryID": ParentCategoryID},
        success: function (res) {
            $.each(res, function (data, value) {
                var selected = "";
                if ($("#hdnCategoryID").val() != null) {
                    if ($("#hdnCategoryID").val() == value.CategoryID) {
                        selected = "selected='selected'";
                    }
                }
                $("#ddlCategory").append($("<option " + selected + "></option>").val(value.CategoryID).html(value.CategoryName));
            })

            $('#ddlCategory option:first-child').attr("selected", "selected");
            $("#hdnCategoryID").val($("#ddlCategory").val());
        }

    });
}

$(document).on('change', '#ddlCategory', function () {
    $("#hdnCategoryID").val($("#ddlCategory").val());
});