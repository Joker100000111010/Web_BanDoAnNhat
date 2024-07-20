
function submitForm(icon) {
    // Find the form element that is a parent of the clicked icon
    const form = icon.closest('form');
    // Submit the form
    form.submit();
}
// Khởi tạo swiper với cấu hình ban đầu
var swiper = new Swiper(".mySwiper", {
  slidesPerView: 4,
  spaceBetween: 15,
  freeMode: true,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
});

// Cập nhật cấu hình swiper dựa trên kích thước màn hình
function updateSwiper() {
  if (window.innerWidth < 560) {
      swiper.params.slidesPerView = 1; // Hiển thị 1 ảnh trên màn hình nhỏ hơn 560px
  } else {
      swiper.params.slidesPerView = 4; // Hiển thị 3 ảnh trên màn hình lớn hơn hoặc bằng 560px
  }
  swiper.update(); // Cập nhật swiper với cấu hình mới
}

// Gọi hàm updateSwiper khi màn hình thay đổi kích thước
window.addEventListener("resize", updateSwiper);

// Gọi hàm updateSwiper khi trang được tải
document.addEventListener("DOMContentLoaded", function() {
  updateSwiper();
});

document.getElementById('buyNowButton').addEventListener('click', function () {
    document.querySelector('.body1').scrollIntoView({ behavior: 'smooth' });
});

function increaseViewsById(id) {
    $.ajax({
        url: '/SanPham/UpdateViewCount',
        method: 'POST',
        data: { postId: id },
        success: function (response) {
            if (response.success) {
                // Tăng lượt xem trong giao diện người dùng
                var viewsElement = document.getElementById('views' + id);
                if (viewsElement) {
                    viewsElement.innerText = parseInt(viewsElement.innerText) + 1;
                }

                // Cập nhật ngày hiện tại
                updateDate(id);
            } else {
                alert("Cập nhật lượt xem không thành công");
            }
        },
        error: function () {
            alert("Có lỗi xảy ra khi gửi yêu cầu");
        }
    });
}

function updateDate(id) {
    var newTime = new Date().toISOString();
    console.log("Sending new time:", newTime); // Thêm log để kiểm tra thời gian gửi đi
    console.log("Post ID:", id); // Thêm log để kiểm tra postId

    $.ajax({
        url: '/SanPham/UpdatePostTime',
        method: 'POST',
        data: { postId: id, newTime: newTime },
        success: function (response) {
            console.log("Server response:", response); // Log phản hồi từ server
            if (response.success) {
                var currentDate = new Date(newTime); // Sử dụng newTime để đồng bộ với thời gian cập nhật
                var day = currentDate.getDate();
                var month = currentDate.getMonth() + 1;
                var year = currentDate.getFullYear();
                var formattedDate = day.toString().padStart(2, '0') + '/' + month.toString().padStart(2, '0') + '/' + year;

                var dateElement = document.getElementById('date' + id);
                if (dateElement) {
                    dateElement.innerText = formattedDate;
                    console.log("Date updated on UI:", formattedDate); // Log để kiểm tra cập nhật giao diện người dùng
                }
            } else {
                alert("Cập nhật ngày không thành công");
            }
        },
        error: function () {
            alert("Có lỗi xảy ra khi gửi yêu cầu");
        }
    });
}




