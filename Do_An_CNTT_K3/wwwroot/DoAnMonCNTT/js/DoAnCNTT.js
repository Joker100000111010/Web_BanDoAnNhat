

function submitForm(icon) {
    // Find the form element that is a parent of the clicked icon
    const form = icon.closest('form');
    // Submit the form
    form.submit();
}
// Lấy phần tử biểu tượng menu và menu
const menuBar = document.querySelector(".header2 .fa-bars");
const menu = document.querySelector(".header2 .mucluc ul");
const menuCloseBtn = document.querySelector(".header2 .mucluc .thanhxoa");

// Thêm sự kiện click cho biểu tượng menu để hiển thị menu
menuBar.addEventListener("click", function () {
    menu.classList.add("show-menu");
});

// Thêm sự kiện click cho nút đóng để ẩn menu
menuCloseBtn.addEventListener("click", function () {
    menu.classList.remove("show-menu");
});

// Thêm sự kiện click cho nút đóng để ẩn menu
const cartIcon = document.querySelector(".giohang a");
const cart = document.querySelector(".giohang .cart-content");
const cartCloseBtn = document.querySelector(".cart-close");

cartIcon.addEventListener("click", function (event) {
    if (window.innerWidth <= 768) { // Kiểm tra nếu là màn hình điện thoại
        event.preventDefault(); // Ngăn chặn hành động mặc định của liên kết
        cart.classList.add("show-cart");
    } else {
        // Nếu là màn hình máy tính, chuyển hướng tới trang khác
        window.location.href = "../Page/lienhe.html";
    }
});

cartCloseBtn.addEventListener("click", function () {
    cart.classList.remove("show-cart");
}); 


//tin tuc


  //Tin tien trong  gio hang
  let discountRate = 0; // Tỷ lệ giảm giá mặc định

  function updateTotal() {
      const items = document.querySelectorAll('.Thongtingiohang');
      let total = 0;

      items.forEach(item => {
          const quantity = item.querySelector('.quantity').value;
          const price = item.getAttribute('data-price');
          const itemTotalPrice = item.querySelector('.item-total-price');

          const totalPriceForItem = quantity * price;
          itemTotalPrice.textContent = totalPriceForItem;
          total += totalPriceForItem;
      });

      document.getElementById('total-price').textContent = total;

      applyDiscount(total);
  }

  function applyDiscount(total) {
      const discountedTotal = total * (1 - discountRate);
      document.getElementById('discounted-price').textContent = discountedTotal;
  }

  document.querySelectorAll('.quantity').forEach(input => {
      input.addEventListener('change', updateTotal);
  });

  document.querySelectorAll('.delete-item').forEach(button => {
      button.addEventListener('click', (event) => {
          const item = event.target.closest('.Thongtingiohang');
          item.remove();
          updateTotal();
      });
  });

  document.querySelectorAll('.increase-quantity').forEach(button => {
      button.addEventListener('click', (event) => {
          const quantityInput = event.target.closest('.quantity-controls').querySelector('.quantity');
          quantityInput.value = parseInt(quantityInput.value) + 1;
          updateTotal();
      });
  });

  document.querySelectorAll('.decrease-quantity').forEach(button => {
      button.addEventListener('click', (event) => {
          const quantityInput = event.target.closest('.quantity-controls').querySelector('.quantity');
          if (quantityInput.value > 1) {
              quantityInput.value = parseInt(quantityInput.value) - 1;
              updateTotal();
          }
      });
  });

  document.getElementById('apply-discount').addEventListener('click', () => {
      const code = document.getElementById('discount-code').value;
      
      // Các mã giảm giá ví dụ
      const discountCodes = {
          "long10": 0.1,
          "long20": 0.2
      };

      if (discountCodes[code]) {
          discountRate = discountCodes[code];
          document.getElementById('discounted-price-container').classList.remove('hidden');
      } else {
          discountRate = 0;
          alert('Mã giảm giá không hợp lệ');
          document.getElementById('discounted-price-container').classList.add('hidden');
      }

      updateTotal();
  });

window.addEventListener('load', updateTotal);







  





