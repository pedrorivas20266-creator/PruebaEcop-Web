class ConfirmModal {
    constructor() {
        this.container = document.getElementById('modalContainer');
        if (!this.container) {
            this.container = document.createElement('div');
            this.container.id = 'modalContainer';
            this.container.className = 'modal-container';
            document.body.appendChild(this.container);
        }
    }

    show(options = {}) {
        return new Promise((resolve) => {
            const {
                title = 'Esta seguro?',
                message = 'Desea continuar con esta accion?',
                confirmText = 'Confirmar',
                cancelText = 'Cancelar',
                type = 'warning'
            } = options;

            const modal = document.createElement('div');
            modal.className = 'modal confirm-modal show';
            
            modal.innerHTML = `
                <div class="modal-content modal-sm">
                    <div class="modal-header modal-header-${type}">
                        <h2>${this.getIcon(type)} ${title}</h2>
                    </div>
                    <div class="modal-body">
                        <p>${message}</p>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-secondary" data-action="cancel">
                            ${cancelText}
                        </button>
                        <button class="btn btn-${this.getButtonClass(type)}" data-action="confirm">
                            ${confirmText}
                        </button>
                    </div>
                </div>
            `;

            this.container.appendChild(modal);

            const handleClick = (e) => {
                const action = e.target.dataset.action;
                if (action) {
                    modal.classList.remove('show');
                    setTimeout(() => {
                        modal.remove();
                        resolve(action === 'confirm');
                    }, 300);
                }
            };

            modal.addEventListener('click', (e) => {
                if (e.target === modal) {
                    modal.classList.remove('show');
                    setTimeout(() => {
                        modal.remove();
                        resolve(false);
                    }, 300);
                }
            });

            modal.querySelectorAll('[data-action]').forEach(btn => {
                btn.addEventListener('click', handleClick);
            });
        });
    }

    getIcon(type) {
        const icons = {
            warning: '<i class="fas fa-exclamation-triangle"></i>',
            danger: '<i class="fas fa-exclamation-circle"></i>',
            info: '<i class="fas fa-info-circle"></i>',
            success: '<i class="fas fa-check-circle"></i>'
        };
        return icons[type] || icons.info;
    }

    getButtonClass(type) {
        const classes = {
            warning: 'warning',
            danger: 'danger',
            info: 'primary',
            success: 'success'
        };
        return classes[type] || 'primary';
    }

    confirm(message, title = 'Esta seguro?') {
        return this.show({ title, message, type: 'warning' });
    }

    danger(message, title = 'Eliminar') {
        return this.show({ 
            title, 
            message, 
            type: 'danger',
            confirmText: 'Eliminar'
        });
    }
}

const confirmModal = new ConfirmModal();