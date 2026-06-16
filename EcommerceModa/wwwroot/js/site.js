// Atualiza contador do carrinho via AJAX
async function atualizarContadorCarrinho() {
    try {
        const res = await fetch('/Carrinho/Count');
        const count = await res.json();
        const el = document.getElementById('cartCount');
        if (el) {
            el.textContent = count;
            el.style.display = count > 0 ? 'inline-flex' : 'none';
        }
    } catch (e) { /* silencioso */ }
}

document.addEventListener('DOMContentLoaded', atualizarContadorCarrinho);
