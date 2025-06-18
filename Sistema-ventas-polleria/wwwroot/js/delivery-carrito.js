// Mostrar productos del carrito en el resumen de Delivery y enviar pedido al backend

document.addEventListener('DOMContentLoaded', function () {
    function cargarCarrito() {
        const data = localStorage.getItem('carrito');
        return data ? JSON.parse(data) : [];
    }

    function renderResumenPedido() {
        const carrito = cargarCarrito();
        const resumen = document.getElementById('resumen-pedido');
        const totalSpan = document.getElementById('total-pedido');
        let html = '';
        let total = 0;

        if (carrito.length === 0) {
            resumen.innerHTML = '<p class="text-muted">No hay productos en el carrito.</p>';
            totalSpan.textContent = 'S/ 0.00';
            return;
        }

        carrito.forEach(item => {
            html += `
                <div class="d-flex align-items-center mb-2">
                    <img src="${item.imagen}" alt="${item.nombre}" style="width:40px;height:40px;object-fit:cover;border-radius:8px;margin-right:10px;">
                    <div class="flex-grow-1">
                        <strong>${item.nombre}</strong> x${item.cantidad}
                    </div>
                    <span class="text-danger">S/ ${(item.precio * item.cantidad).toFixed(2)}</span>
                </div>
            `;
            total += item.precio * item.cantidad;
        });

        resumen.innerHTML = html;
        totalSpan.textContent = `S/ ${total.toFixed(2)}`;
    }

    renderResumenPedido();
    window.carritoParaEnvio = cargarCarrito();
});

function confirmarEnvio() {
    // Obtener datos del formulario
    const direccion = document.getElementById('direccion').value;
    const referencia = document.getElementById('referencia').value;
    const enlaceGoogleMaps = document.getElementById('enlaceGoogleMaps').value;
    const telefono = document.getElementById('telefono').value;
    const carrito = window.carritoParaEnvio || [];

    if (carrito.length === 0) {
        alert('El carrito está vacío.');
        return;
    }

    // Puedes agregar validaciones aquí...

    // Enviar datos al backend
    fetch('/Cliente/ConfirmarPedido', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            direccion,
            referencia,
            enlaceGoogleMaps,
            telefono,
            carrito
        })
    })
    .then(response => {
        if (response.ok) {
            alert('¡Pedido realizado con éxito!');
            localStorage.removeItem('carrito');
            window.location.href = '/Cliente/Clientelog';
        } else {
            alert('Error al realizar el pedido.');
        }
    })
    .catch(error => {
        alert('Error de red: ' + error);
    });
} 