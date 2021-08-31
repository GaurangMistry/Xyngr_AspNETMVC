$(document).ready(function () {

    var staticpageDataTable = $('#staticpage-contacts').DataTable();
    // Set the search textbox functionality in sidebar
    $('#search-contacts').on('keyup', function () {
        staticpageDataTable.search(this.value).draw();
    });

    // Checkbox & Radio 1
    $('.input-chk').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
    });

    staticpageDataTable.on('draw.dt', function () {
        // Checkbox & Radio 1
        $('.input-chk').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
        });
    });

});