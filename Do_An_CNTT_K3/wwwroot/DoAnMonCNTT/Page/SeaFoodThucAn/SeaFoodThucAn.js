//Kéo Ảnh
var swiper = new Swiper(".mySwiper", {
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
});
function submitForm(icon) {
    // Find the form element that is a parent of the clicked icon
    const form = icon.closest('form');
    // Submit the form
    form.submit();
}

//thanh tăng giảm số lượng
document.addEventListener("DOMContentLoaded", function () {
    const quantityControls = document.querySelector('.quantity-controls-seedfood');
    const minusButton = quantityControls.querySelector('.minus-seedfood');
    const plusButton = quantityControls.querySelector('.plus-seedfood');
    const quantitySpan = quantityControls.querySelector('.quantity-seedfood');

    if (minusButton && plusButton && quantitySpan) {
        minusButton.addEventListener('click', function () {
            let currentValue = parseInt(quantitySpan.textContent);
            if (currentValue > 1) {
                quantitySpan.textContent = currentValue - 1;
                updateTotal();
            }
        });

        plusButton.addEventListener('click', function () {
            let currentValue = parseInt(quantitySpan.textContent);
            quantitySpan.textContent = currentValue + 1;
            updateTotal();
        });
    }
});


//Đánh giá sao
document.addEventListener("DOMContentLoaded", function () {
    const saoContainers = document.querySelectorAll('.star > .sao1, .star > .sao2');

    saoContainers.forEach((saoContainer) => {
        const saos = saoContainer.querySelectorAll('.motsao, .haisao, .basao, .bonsao, .namsao');

        saos.forEach((sao) => {
            sao.addEventListener('click', () => {
                console.log(`Bạn đã chọn sao: ${sao.textContent.trim()}`);

                // Xóa lớp 'selected' khỏi tất cả các sao trong container
                saos.forEach((s) => {
                    s.classList.remove('selected');
                });

                // Thêm lớp 'selected' cho sao được click
                sao.classList.add('selected');

                // Xóa lớp 'selected' khỏi các container sao khác
                saoContainers.forEach((container) => {
                    if (container !== saoContainer) {
                        container.querySelectorAll('.selected').forEach((s) => {
                            s.classList.remove('selected');
                        });
                    }
                });
            });
        });

        // Xử lý khi di chuột qua sao
        saos.forEach((sao) => {
            sao.addEventListener('mouseover', () => {
                // Thêm lớp 'hover' khi di chuột qua sao
                if (!sao.classList.contains('selected')) {
                    sao.classList.add('hover');
                }
            });

            sao.addEventListener('mouseout', () => {
                // Xóa lớp 'hover' khi di chuột ra khỏi sao
                sao.classList.remove('hover');
            });
        });
    });
});