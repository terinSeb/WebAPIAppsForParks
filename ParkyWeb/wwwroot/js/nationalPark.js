﻿let dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').dataTable({
        "ajax": {
            "url": "/NationalParks/GetAllNationalParks",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "50%" },
            { "data": "state", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                            <a href="NationalParks/Upsert/${data}" class = "btn btn-success text-white"
                            style='cursor:pointer;'><i class ="fa fa-edit"></i></a>
                            <a onclick=Delete("NationalParks/Delete/${data}") class = "btn btn-danger text-white"
                            style='cursor:pointer;'><i class ="fa fa-trash-alt"></i></a></div>
                            `
                }, "width" : "30%"
            }
        ]
    })
}
function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((WillDelete) => {
        if (WillDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}