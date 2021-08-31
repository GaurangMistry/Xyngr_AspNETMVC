$(document).ready(function () {

    var categoryDataTable = $('#category-contacts').DataTable();
    // Set the search textbox functionality in sidebar
    $('#search-contacts').on('keyup', function () {
        categoryDataTable.search(this.value).draw();
    });

    // Checkbox & Radio 1
    $('.input-chk').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
    });

    categoryDataTable.on('draw.dt', function () {
        // Checkbox & Radio 1
        $('.input-chk').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
        });
    });
});