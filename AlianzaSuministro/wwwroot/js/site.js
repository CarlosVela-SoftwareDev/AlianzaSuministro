$('#proveedorModal').on('show.bs.modal', function (e) {
    loadProveedores();
});

function openAddModal() {
    $('#proveedorForm').attr('data-mode', 'add');
    $('#proveedorModalLabel').text('Añadir ProductoProveedor');
    clearProveedorForm();
    $('#proveedorModal').modal('show');
    loadProveedores();
}

function openEditModal(id) {
    $('#proveedorForm').attr('data-mode', 'edit');
    $('#proveedorModalLabel').text('Editar ProductoProveedor');
    loadProveedorData(id);
    $('#proveedorModal').modal('show');
}

function loadProveedorData(id) {
    fetch(`/api/proveedor/${id}`)
        .then(response => response.json())
        .then(data => {
            document.getElementById('proveedorId').value = data.id;
            $('#proveedorSelect').val(data.proveedorId); // Seleccionar el proveedor correspondiente
            document.getElementById('costo').value = data.costo;
            document.getElementById('claveProductoProveedor').value = data.claveProductoProveedor;
            $('#proveedorModal').modal('show');
        })
        .catch(error => console.error('Error al obtener los datos del ProductoProveedor:', error));
}

function loadProveedores() {
    $.ajax({
        url: '/api/proveedor', // Asegúrate de que esta ruta sea correcta
        method: 'GET',
        success: function (data) {
            var proveedorSelect = $('#proveedorSelect');
            proveedorSelect.empty();
            if (Array.isArray(data.$values)) {
                data.$values.forEach(function (proveedor) {
                    proveedorSelect.append($('<option></option>').val(proveedor.id).text(proveedor.nombre));
                });
            } else {
                console.error('Error: se esperaba una lista de proveedores.');
            }
        },
        error: function (error) {
            console.error('Error al cargar los proveedores:', error);
        }
    });
}

function saveProveedor() {
    var mode = $('#proveedorForm').attr('data-mode');
    var id = $('#proveedorId').val();
    var proveedorId = $('#proveedorSelect').val();
    var costo = $('#costo').val();
    var claveProductoProveedor = $('#claveProductoProveedor').val();
    var productoId = $('#productoId').val();

    var data = {
        id: mode === 'edit' ? id : null,
        proveedorId: proveedorId,
        productoId: productoId,
        costo: costo,
        claveProductoProveedor: claveProductoProveedor
    };
    var url = '/api/Proveedor' + (mode === 'edit' ? `/${id}` : '');
    var method = mode === 'edit' ? 'PUT' : 'POST';

    $.ajax({
        url: url,
        method: method,
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function () {
            $('#proveedorModal').modal('hide');
            // Actualiza la tabla de proveedores
            reloadProveedorTable(productoId);
        },
        error: function (error) {
            console.error('Error al guardar el proveedor:', error);
        }
    });
}

function reloadProveedorTable(productoId) {
    $.ajax({
        url: `/api/proveedor/producto/${productoId}`,
        method: 'GET',
        success: function (data) {
            var tableBody = '';
            if (data.$values && Array.isArray(data.$values)) {
                data.$values.forEach(function (item) {
                    var productoProveedor = item.productoProveedores && item.productoProveedores.$values && item.productoProveedores.$values.length > 0 ? item.productoProveedores.$values[0] : null;
                    var claveProductoProveedor = productoProveedor ? productoProveedor.claveProductoProveedor : '';
                    var costo = productoProveedor ? productoProveedor.costo : '';
                    tableBody += `
                        <tr>
                            <td>${item.nombre}</td>
                            <td>${claveProductoProveedor}</td>
                            <td>${costo}</td>
                            <td>
                                <i class="bi bi-pencil-square mx-1" onclick="openEditModal(${item.id});" style="cursor: pointer;" data-bs-toggle="tooltip" data-bs-placement="top" title="Editar"></i>
                            </td>
                        </tr>
                    `;
                });
            }
            $('.table-container tbody').html(tableBody);
        }
    });
}

function clearProveedorForm() {
    $('#proveedorId').val('');
    $('#proveedorSelect').val('');
    $('#costo').val('');
    $('#claveProductoProveedor').val('');
}

/*Inicializa los tooltips*/
var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
})