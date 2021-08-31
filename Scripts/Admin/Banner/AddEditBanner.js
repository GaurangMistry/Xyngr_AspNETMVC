function SaveBanner(isEditMode) {
    if ($(".dz-preview").length > 0) {
        myDropzone.processQueue();
        myDropzone.on("complete", function (file) {
            debugger;
            if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                var data = $("#frmAddBanner").serializeArray();

               // data.push({ name: 'Image', value: file.name })
                save(isEditMode, data);
            }
        });
    }
    else {
        var data = $("#frmAddBanner").serializeArray();
        save(isEditMode, data);
    }
}

function save(isEditMode, data) {
    var isFormvalid = $("#frmAddBanner").valid();

    if (isFormvalid) {
        $.ajax({
            url: '/Admin/Banner/Save',
            type: "POST",
            data: $.param(data),

            success: function (data) {
                if (isEditMode)
                    window.location.href = "/Admin/Banner/index/u";
                else
                    window.location.href = "/Admin/Banner/index/s";
            }
        });
    }
}


//Dropzone.options.myAwesomeDropzone = { // The camelized version of the ID of the form element

//    // The configuration we've talked about above
//    url: '/Admin/Banner/Save',
//    autoProcessQueue: false,
//    uploadMultiple: true,
//    parallelUploads: 100,
//    maxFiles: 100,

//    // The setting up of the dropzone
//    init: function () {
//        var myDropzone = this;
//        var el = document.getElementById('btnSaveBanner');
//        if (el) {
//            el.addEventListener("click", function (e) {
//                // Make sure that the form isn't actually being sent.
//                e.preventDefault();
//                e.stopPropagation();
//                myDropzone.processQueue();
//            });
//        }


//        // Listen to the sendingmultiple event. In this case, it's the sendingmultiple event instead
//        // of the sending event because uploadMultiple is set to true.
//        this.on("sendingmultiple", function (data, xhr, formData) {
//            // Gets triggered when the form is actually being sent.
//            // Hide the success button or the complete form.
//            //formData = $("#frmAddBanner").serialize();
//            //var  formValues=$("#frmAddBanner").serializeArray();
//            $("#frmAddBanner").serializeArray().forEach(function (field) {
//                formData.append(field.name, field.value)
//            })

//        });
//        this.on("successmultiple", function (files, response) {
//            // Gets triggered when the files have successfully been sent.
//            // Redirect user or notify of success.
//        });
//        this.on("errormultiple", function (files, response) {
//            // Gets triggered when there was an error sending the files.
//            // Maybe show form again, and notify user of error
//        });
//    }

//}