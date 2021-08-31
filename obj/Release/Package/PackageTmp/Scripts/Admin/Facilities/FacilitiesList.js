$(document).ready(function () {

    var facilitiesDataTable = $('#facilities-contacts').DataTable();
    // Set the search textbox functionality in sidebar
    $('#search-contacts').on('keyup', function () {
        facilitiesDataTable.search(this.value).draw();
    });

    // Checkbox & Radio 1
    $('.input-chk').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
    });

    facilitiesDataTable.on('draw.dt', function () {
        // Checkbox & Radio 1
        $('.input-chk').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
        });
    });

});