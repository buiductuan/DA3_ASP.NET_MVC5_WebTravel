var productCategory = {
    init: function () {
        productCategory.registerEvent();
    },
    registerEvent: function () {
        //change status product category
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var b = $(this);
            var id = b.data('id');
            $.ajax({
                url: '/Admin/ProductCategory/ChangeStatus',
                method: 'POST',
                data: { id: id },
                dataType: 'json',
                success: function (res) {
                    console.log(res);
                    if (res.status == true) {
                        b.text("Hiển thị");
                    } else {
                        b.text("Ẩn");
                    }
                },
                error: function (err) {
                    console.log(err);
                    alert(err);
                }
            });
        });
    }
}
productCategory.init();