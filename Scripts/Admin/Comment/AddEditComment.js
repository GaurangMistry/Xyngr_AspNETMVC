
$(document).ready(function () {
    BindBranchDropdownByBusinessID($("#BusinessID").val())
    $("#BusinessID").change(function () {
        BindBranchDropdownByBusinessID($("#BusinessID").val())
    });
});

function SaveComment(isEditMode) {
    var Comments = CKEDITOR.instances.txtComments.getData();
    debugger;
    // convert form data to array
    var data = $("#frmAddComment").serializeArray();
    // using ES6
    data.find(item => item.name === 'Comments').value = Comments;
    if (Comments == "") {
        $('span[data-valmsg-for="Comments"]').html("Comments is required."); 
        return false;
    }

    var isFormvalid = $("#frmAddComment").valid();   

    if (isFormvalid) {
        $.ajax({
            url: '/Admin/Comment/Save',
            type: "POST",
            data: $.param(data),
            success: function (data) {
                debugger;
                if (isEditMode)
                    window.location.href = "/Admin/Comment/index?id=u&buid=" + $("#BusinessID").val() + "&bid=" + $("#ddlBranch").val();
                else
                    window.location.href = "/Admin/Comment/index?id=s&buid=" + $("#BusinessID").val() + "&bid=" + $("#ddlBranch").val();
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
                debugger;
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