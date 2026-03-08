let detallesPedido = [];
let lineaNumero = 1;

function openPedidoModal() {
    document.getElementById('pedidoForm').reset();
    detallesPedido = [];
    lineaNumero = 1;
    updateDetalleTable();
    calculateTotals();
    document.getElementById('pedidoModal').classList.add('show');
}

function closePedidoModal() {
    document.getElementById('pedidoModal').classList.remove('show');
}

document.getElementById('productoSelect')?.addEventListener('change', function () {
    const selectedOption = this.options[this.selectedIndex];
    const precio = selectedOption.dataset.precio || 0;
    document.getElementById('precioUnitario').value = parseFloat(precio).toFixed(2);
});

function addDetalle() {
    const productoSelect = document.getElementById('productoSelect');
    const cantidad = parseFloat(document.getElementById('cantidad').value);
    const precioUnitario = parseFloat(document.getElementById('precioUnitario').value);

    if (!productoSelect.value) {
        toast.warning('Debe seleccionar un producto');
        return;
    }

    if (!cantidad || cantidad <= 0) {
        toast.warning('La cantidad debe ser mayor a 0');
        return;
    }

    if (!precioUnitario || precioUnitario <= 0) {
        toast.warning('El precio debe ser mayor a 0');
        return;
    }

    const selectedOption = productoSelect.options[productoSelect.selectedIndex];

    const detalle = {
        lineaNumero: lineaNumero++,
        codProducto: parseInt(productoSelect.value),
        nombreProducto: selectedOption.dataset.nombre,
        cantidad: cantidad,
        precioUnitario: precioUnitario,
        subtotal: cantidad * precioUnitario
    };

    detallesPedido.push(detalle);

    updateDetalleTable();
    calculateTotals();

    productoSelect.value = '';
    document.getElementById('cantidad').value = '1';
    document.getElementById('precioUnitario').value = '';

    toast.success('Producto agregado al pedido');
}

function removeDetalle(lineaNum) {
    detallesPedido = detallesPedido.filter(d => d.lineaNumero !== lineaNum);
    updateDetalleTable();
    calculateTotals();
    toast.info('Producto removido del pedido');
}

function updateDetalleTable() {
    const tbody = document.getElementById('detalleTableBody');
    tbody.innerHTML = '';

    if (detallesPedido.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="6" class="text-center">No hay productos agregados</td>
            </tr>
        `;
        return;
    }

    detallesPedido.forEach(detalle => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${detalle.lineaNumero}</td>
            <td>${detalle.nombreProducto}</td>
            <td>${detalle.cantidad}</td>
            <td>? ${detalle.precioUnitario.toFixed(2)}</td>
            <td>? ${detalle.subtotal.toFixed(2)}</td>
            <td>
                <button type="button" class="btn-icon btn-delete"
                        onclick="removeDetalle(${detalle.lineaNumero})">
                    <i class="fas fa-trash"></i>
                </button>
            </td>
        `;
        tbody.appendChild(row);
    });
}

function calculateTotals() {
    const total = detallesPedido.reduce((sum, d) => sum + d.subtotal, 0);

    const ivaIncluido = total * (10 / 110);
    const subtotalSinIva = total - ivaIncluido;

    document.getElementById('subtotalPedido').textContent = `? ${subtotalSinIva.toFixed(2)}`;
    document.getElementById('ivaPedido').textContent = `? ${ivaIncluido.toFixed(2)}`;
    document.getElementById('totalPedido').textContent = `? ${total.toFixed(2)}`;
}

async function savePedido() {
    const codCliente = document.getElementById('codCliente').value;

    if (!codCliente) {
        toast.warning('Debe seleccionar un cliente');
        return;
    }

    if (detallesPedido.length === 0) {
        toast.warning('Debe agregar al menos un producto al pedido');
        return;
    }

    const pedidoData = {
        codCliente: parseInt(codCliente),
        codMoneda: 1,
        detalles: detallesPedido.map(d => ({
            codProducto: d.codProducto,
            cantidad: d.cantidad,
            precioUnitario: d.precioUnitario,
            lineaNumero: d.lineaNumero
        }))
    };

    try {
        const response = await fetch(`/Pedidos/Index?handler=Create`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
            },
            body: JSON.stringify(pedidoData)
        });

        const result = await response.json();

        if (result.success) {
            toast.success(result.message);
            closePedidoModal();
            setTimeout(() => location.reload(), 1500);
        } else {
            toast.error(result.message);
        }
    } catch (error) {
        toast.error('Error al guardar el pedido');
        console.error(error);
    }
}

async function viewPedido(id) {
    try {
        const response = await fetch(`/Pedidos/Index?handler=Pedido&id=${id}`);
        const pedido = await response.json();
        console.log(pedido);
    } catch (error) {
        toast.error('Error al cargar el pedido');
    }
}

async function anularPedido(id, numPedido) {
    const confirmed = await confirmModal.danger(
        `Esta seguro de anular el pedido "${numPedido}"?`,
        'Anular Pedido'
    );

    if (!confirmed) return;

    const motivo = prompt('Ingrese el motivo de anulación:');

    if (!motivo) {
        toast.warning('Debe ingresar un motivo');
        return;
    }

    try {
        const response = await fetch(`/Pedidos/Index?handler=Anular&id=${id}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify({ motivo: motivo })
        });

        const result = await response.json();

        if (result.success) {
            toast.success(result.message);
            setTimeout(() => location.reload(), 1500);
        } else {
            toast.error(result.message);
        }
    } catch (error) {
        toast.error('Error al anular el pedido');
    }
}

document.addEventListener('click', function (e) {
    const modal = document.getElementById('pedidoModal');
    if (e.target === modal) {
        closePedidoModal();
    }
});