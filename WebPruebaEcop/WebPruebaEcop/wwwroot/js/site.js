document.addEventListener('DOMContentLoaded', function () {
    initSidebar();
});

function initSidebar() {
    const sidebar = document.getElementById('sidebar');
    const sidebarToggle = document.getElementById('sidebarToggle');

    if (!sidebar) return;

    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();

            sidebar.classList.toggle('collapsed');

            localStorage.setItem(
                'sidebarCollapsed',
                sidebar.classList.contains('collapsed')
            );
        });
    }

    if (localStorage.getItem('sidebarCollapsed') === 'true') {
        sidebar.classList.add('collapsed');
    }

    const menuItems = document.querySelectorAll('.nav-link[data-submenu]');

    menuItems.forEach(item => {
        item.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();

            const submenuId = this.dataset.submenu;
            const submenu = document.getElementById(`submenu-${submenuId}`);
            const parent = this.parentElement;

            if (!submenu || !parent) return;

            document.querySelectorAll('.nav-item.active').forEach(el => {
                if (el !== parent) {
                    el.classList.remove('active');
                }
            });

            parent.classList.toggle('active');
        });
    });
}
async function fetchData(url, options = {}) {
    try {
        const response = await fetch(url, {
            headers: {
                'Content-Type': 'application/json',
                ...options.headers
            },
            ...options
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        return await response.json();
    } catch (error) {
        console.error('Fetch error:', error);
        if (typeof toast !== 'undefined' && toast.error) {
            toast.error('Error en la comunicación con el servidor');
        }
        throw error;
    }
}