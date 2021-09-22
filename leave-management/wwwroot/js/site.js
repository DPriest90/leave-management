// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Code to setup datatable from DataTable.net (when we create a new table on  page we will need to make sure the table has an id="value")
$(document).ready(function () {
    $('#tblData').DataTable();
});