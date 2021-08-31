$(document).ready(function () {
    BindCategoryDropdownByParentID($("#ParentCategoryID").val())
    $("#ParentCategoryID").change(function () {
        BindCategoryDropdownByParentID($("#ParentCategoryID").val())
    });
});

function SaveFacilities(isEditMode) {

    var isFormvalid = $("#frmAddFacilities").valid();   

    if (isFormvalid) {
        $.ajax({
            url: '/Admin/Facilities/Save',
            type: "POST",
            data: $("#frmAddFacilities").serialize(),
            success: function (data) {
                debugger;
                if (isEditMode)
                    window.location.href = "/Admin/Facilities/index/u";
                else
                    window.location.href = "/Admin/Facilities/index/s";
            }
        });
    }
}

function BindCategoryDropdownByParentID(ParentCategoryID) {
    $("#ddlCategory").empty();
    $.ajax({
        type: "POST",
        url: "/Admin/Category/GetCategoriesByParentID",
        data: { "parentCategoryID": ParentCategoryID },
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