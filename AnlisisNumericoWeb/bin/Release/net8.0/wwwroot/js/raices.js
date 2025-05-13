function showSection(id) {
    const sections = document.querySelectorAll('.content');
    sections.forEach(sec => sec.classList.remove('visible'));
    document.getElementById(id).classList.add('visible');

    const items = document.querySelectorAll('#submenu .menu-item');
    items.forEach(item => item.classList.remove('active'));

    const textMatch = {
        biseccion: 'Bisección',
        regla: 'Regla Falsa',
        newton: 'Newton-Raphson',
        secante: 'Secante'
    };

    items.forEach(item => {
        if (item.textContent.trim() === textMatch[id]) {
            item.classList.add('active');
        }
    });
}

function toggleSubmenu() {
    const submenu = document.getElementById('submenu');
    submenu.style.display = submenu.style.display === 'none' ? 'flex' : 'none';
}