$(document).ready(function () {

    var mediagalleryDataTable = $('#mediagallery-contacts').DataTable();
    // Set the search textbox functionality in sidebar
    $('#search-contacts').on('keyup', function () {
        mediagalleryDataTable.search(this.value).draw();
    });

    // Checkbox & Radio 1
    $('.input-chk').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
    });

    mediagalleryDataTable.on('draw.dt', function () {
        // Checkbox & Radio 1
        $('.input-chk').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
        });
    });

});