let currentClienteId = null;

function openClienteModal() {
    document.getElementById('modalTitle').textContent = 'Nuevo Cliente';
    document.getElementById('clienteForm').reset();
    document.getElementById('codCliente').value = '';
    currentClienteId = null;
    document.getElementById('clienteModal').classList.add('show');
}

function closeClienteModal() {
    document.getElementById('clienteModal').classList.remove('show');
}

async function editCliente(id) {
    try {
        const response = await fetch(`/Clientes/Index?handler=Cliente&id=${id}`);
        const cliente = await response.json();

        if (cliente) {
            document.getElementById('modalTitle').textContent = 'Editar Cliente';
            document.getElementById('codCliente').value = cliente.codCliente;
            document.getElementById('nombres').value = cliente.nombres || '';
            document.getElementById('apellidos').value = cliente.apellidos || '';
            document.getElementById('codTipoDocumento').value = cliente.codTipoDocumento || 1;
            document.getElementById('numeroDocumento').value = cliente.numeroDocumento || '';
            document.getElementById('numeroTelefono').value = cliente.numeroTelefono || '';
            document.getElementById('correo').value = cliente.correo || '';
            document.getElementById('direccion').value = cliente.direccion || '';
            document.getElementById('activo').checked = cliente.activo;

            currentClienteId = id;
            document.getElementById('clienteModal').classList.add('show');
        }
    } catch (error) {
        toast.error('Error al cargar los datos del cliente');
        console.error(error);
    }
}

async function deleteCliente(id, nombre) {
    const confirmed = await confirmModal.danger(
        `Esta seguro de eliminar al cliente "${nombre}"?`,
        'Eliminar Cliente'
    );

    if (!confirmed) return;

    try {
        const response = await fetch(`/Clientes/Index?handler=Delete&id=${id}`, {
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
        toast.error('Error al eliminar el cliente');
        console.error(error);
    }
}

async function saveCliente() {
    const form = document.getElementById('clienteForm');

    if (!form.checkValidity()) {
        form.reportValidity();
        return;
    }

    const clienteData = {
        codCliente: parseInt(document.getElementById('codCliente').value) || 0,
        nombres: document.getElementById('nombres').value,
        apellidos: document.getElementById('apellidos').value,
        codTipoDocumento: parseInt(document.getElementById('codTipoDocumento').value),
        numeroDocumento: document.getElementById('numeroDocumento').value,
        numeroTelefono: document.getElementById('numeroTelefono').value,
        correo: document.getElementById('correo').value,
        direccion: document.getElementById('direccion').value,
        activo: document.getElementById('activo').checked
    };

    const handler = currentClienteId ? 'Update' : 'Create';
    const url = currentClienteId 
        ? `/Clientes/Index?handler=Update&id=${currentClienteId}`
        : `/Clientes/Index?handler=Create`;

    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
            },
            body: JSON.stringify(clienteData)
        });

        const result = await response.json();

        if (result.success) {
            toast.success(result.message);
            closeClienteModal();
            setTimeout(() => location.reload(), 1500);
        } else {
            toast.error(result.message);
        }

    } catch (error) {
        toast.error('Error al guardar el cliente');
        console.error(error);
    }
}

document.addEventListener('click', function (e) {
    const modal = document.getElementById('clienteModal');
    if (e.target === modal) {
        closeClienteModal();
    }
});