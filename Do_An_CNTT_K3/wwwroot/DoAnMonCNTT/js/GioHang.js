document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.quantity-controls button').forEach(function (button) {
        button.addEventListener('click', function () {
            // JavaScript code to handle quantity change

            // Call function to update total price
            updateTotalPrice();
        });
    });
});

function updateTotalPrice() {
    var totalPrice = 0;
    document.querySelectorAll('.Thongtingiohang').forEach(function (container) {
        var price = parseFloat(container.dataset.price);
        var quantity = parseInt(container.querySelector('.quantity').value);
        totalPrice += price * quantity;
    });
    document.getElementById('total-price').textContent = totalPrice.toFixed(2);
}
document.querySelectorAll('.quantity').forEach(input => {
    input.addEventListener('change', function () {
        var productId = input.dataset.productId; // L?y productId t? thu?c t�nh dataset
        var quantity = parseInt(input.value); // L?y s? l??ng t? gi� tr? nh?p v�o
        updateCart(productId, quantity); // G?i h�m updateCart ?? c?p nh?t gi? h�ng
    });
});
