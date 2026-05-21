var dataTable;

$(document).ready(function () {
    loadDataTable();
});

// Cargar la tabla de categorías
//esto se va a conectar con un api GetAll
//que esta en el controlador de categorias y que devuelve un json con todas las categorias
function loadDataTable() {
    dataTable = $("#tblSliders").DataTable({
        "ajax": {
            "url": "/Admin/Slider/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "40%" },
            /* EMPIEZA EL MAPEO DE ESTADO*/
            {
                "data": "isActive",
                "render": function (isActive) {
                    if (isActive) {
                        return "Activo"
                    } else {
                        return "Inactivo"
                    }
                }
            },
            /* FIN MAPEO DE ESTADO*/
            {
                "data": "url", "render": function (imagen) {
                    return `<img src="../${imagen}" width="120" >`;
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Slider/Edit/${data}" 
                                class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-edit"></i>Editar
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Slider/Delete/${data}") 
                                class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-trash-alt"></i>Borrar
                                </a>
                            </div>`;
                },
                "width": "40%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros de sliders",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 a 0 de 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    })
}

function Delete(url) {

    swal({
        title: "Esta seguro de borrar?",
        "text": "Este contenido no se puede recuperar",
        "type": "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: "DELETE",
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                } else {
                    toastr.error(data.message);
                }
            }
        })
    });
}