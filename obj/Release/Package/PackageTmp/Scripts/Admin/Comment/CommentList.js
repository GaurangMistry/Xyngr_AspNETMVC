$(document).ready(function () {

    var imagegalleryDataTable = $('#comment-contacts').DataTable();
    // Set the search textbox functionality in sidebar
    $('#search-contacts').on('keyup', function () {
        imagegalleryDataTable.search(this.value).draw();
    });

    // Checkbox & Radio 1
    $('.input-chk').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
    });

    imagegalleryDataTable.on('draw.dt', function () {
        // Checkbox & Radio 1
        $('.input-chk').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
        });
    });

});