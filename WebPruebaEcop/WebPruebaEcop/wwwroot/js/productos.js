let currentProductoId = null;

function openProductoModal() {
    document.getElementById('modalTitle').textContent = 'Nuevo Producto';
    document.getElementById('productoForm').reset();
    document.getElementById('codProducto').value = '';
    document.getElementById('fechaIngreso').value = new Date().toISOString().split('T')[0];

    currentProductoId = null;
    document.getElementById('productoModal').classList.add('show');

    loadCategorias();
    loadUnidadesMedida();
}

function closeProductoModal() {
    document.getElementById('productoModal').classList.remove('show');
}

async function loadCategorias() {
    try {
        const response = await fetch(`/Productos/Index?handler=Categorias`);
        const categorias = await response.json();

        const select = document.getElementById('codCategoria');
        select.innerHTML = '<option value="">Seleccione...</option>';

        categorias.forEach(cat => {
            const option = document.createElement('option');
            option.value = cat.codCategoria;
            option.textContent = cat.desCategoria;
            select.appendChild(option);
        });
    } catch (error) {
        console.error('Error al cargar categorías:', error);
    }
}

async function loadUnidadesMedida() {
    try {
        const response = await fetch(`/Productos/Index?handler=UnidadesMedida`);
        const unidades = await response.json();

        const select = document.getElementById('codUnidadMedida');
        select.innerHTML = '<option value="">Seleccione...</option>';

        unidades.forEach(um => {
            const option = document.createElement('option');
            option.value = um.codUnidadMedida;
            option.textContent = um.desUnidadMedida;
            select.appendChild(option);
        });
    } catch (error) {
        console.error('Error al cargar unidades de medida:', error);
    }
}

async function editProducto(id) {
    try {
        const response = await fetch(`/Productos/Index?handler=Producto&id=${id}`);
        const producto = await response.json();

        if (!producto) return;

        document.getElementById('modalTitle').textContent = 'Editar Producto';
        document.getElementById('codProducto').value = producto.codProducto;
        document.getElementById('codigoBarra').value = producto.codigoBarra || '';
        document.getElementById('desProducto').value = producto.desProducto || '';

        await loadCategorias();
        await loadUnidadesMedida();

        document.getElementById('codCategoria').value = producto.codCategoria || '';
        document.getElementById('codUnidadMedida').value = producto.codUnidadMedida || '';
        document.getElementById('codIva').value = producto.codIva || 1;
        document.getElementById('fechaIngreso').value =
            producto.fechaIngreso ? producto.fechaIngreso.split('T')[0] : '';

        document.getElementById('costo').value = producto.costoUltimo || producto.costoPromedio || '';

        if (producto.precios && producto.precios.length > 0) {
            document.getElementById('precioVenta').value = producto.precios[0].precioVenta || '';
        }

        document.getElementById('activo').checked = producto.activo;
        document.getElementById('descuentaStock').checked = producto.descuentaStock;

        currentProductoId = id;
        document.getElementById('productoModal').classList.add('show');
    } catch (error) {
        toast.error('Error al cargar los datos del producto');
        console.error(error);
    }
}

async function deleteProducto(id, nombre) {
    const confirmed = await confirmModal.danger(
        `Esta seguro de eliminar el producto "${nombre}"?`,
        'Eliminar Producto'
    );

    if (!confirmed) return;

    try {
        const response = await fetch(`/Productos/Index?handler=Delete&id=${id}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            }
        });

        const result = await response.json();

        if (result.success) {
            toast.success(result.message);
            setTimeout(() => location.reload(), 1500);
        } else {
            toast.error(result.message);
        }
    } catch (error) {
        toast.error('Error al eliminar el producto');
        console.error(error);
    }
}

async function saveProducto() {
    const form = document.getElementById('productoForm');

    if (!form.checkValidity()) {
        form.reportValidity();
        return;
    }

    const isEdit = currentProductoId !== null;
    const precioVenta = parseFloat(document.getElementById('precioVenta').value) || 0;
    const costo = parseFloat(document.getElementById('costo').value) || 0;

    if (isEdit) {
        const productoData = {
            codProducto: parseInt(document.getElementById('codProducto').value) || 0,
            codigoBarra: document.getElementById('codigoBarra').value,
            desProducto: document.getElementById('desProducto').value,
            codCategoria: parseInt(document.getElementById('codCategoria').value),
            codUnidadMedida: parseInt(document.getElementById('codUnidadMedida').value),
            codIva: parseInt(document.getElementById('codIva').value),
            fechaIngreso: document.getElementById('fechaIngreso').value || null,
            costoPromedio: costo,
            costoUltimo: costo,
            precioVenta: precioVenta,
            activo: document.getElementById('activo').checked,
            descuentaStock: document.getElementById('descuentaStock').checked
        };

        try {
            const response = await fetch(`/Productos/Index?handler=Update&id=${currentProductoId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
                },
                body: JSON.stringify(productoData)
            });

            const result = await response.json();

            if (result.success) {
                toast.success(result.message);
                closeProductoModal();
                setTimeout(() => location.reload(), 1500);
            } else {
                toast.error(result.message);
            }
        } catch (error) {
            toast.error('Error al actualizar el producto');
            console.error(error);
        }
    } else {
        const productoData = {
            codigoBarra: document.getElementById('codigoBarra').value,
            desProducto: document.getElementById('desProducto').value,
            codCategoria: parseInt(document.getElementById('codCategoria').value),
            codUnidadMedida: parseInt(document.getElementById('codUnidadMedida').value),
            codIva: parseInt(document.getElementById('codIva').value),
            fechaIngreso: document.getElementById('fechaIngreso').value || null,
            costoUltimo: costo,
            descuentaStock: document.getElementById('descuentaStock').checked,
            precios: [
                {
                    numPrecio: '001',
                    desPrecio: 'Precio Estándar',
                    codTipoPrecio: 1,
                    precioVenta: precioVenta
                }
            ]
        };

        try {
            const response = await fetch(`/Productos/Index?handler=Create`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
                },
                body: JSON.stringify(productoData)
            });

            const result = await response.json();

            if (result.success) {
                toast.success(result.message);
                closeProductoModal();
                setTimeout(() => location.reload(), 1500);
            } else {
                toast.error(result.message);
            }
        } catch (error) {
            toast.error('Error al guardar el producto');
            console.error(error);
        }
    }
}

document.addEventListener('click', function (e) {
    const modal = document.getElementById('productoModal');
    if (e.target === modal) {
        closeProductoModal();
    }
});