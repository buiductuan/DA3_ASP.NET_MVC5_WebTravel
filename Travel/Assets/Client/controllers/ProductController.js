var productController = {
    init: function () {
        productController.registerEvent();
    },
    registerEvent: function () {
        productController.loadImage();
    },
    loadImage: function () {
        $.ajax({
            url: '/Product/GetImagesBindToSlideShow',
            method: 'GET',
            data: {
                id: $('#ProductID').val()
            },
            dataType: 'json',
            success: function (res) {
                var data = res.data;
                var ol = '';
                var slideItem = '';
                $.each(data, function (i, item) {
                    ol += '<li data-target="#carousel_product_images" data-slide-to="' + i + '" class="' + (i == 0 ? 'active' : '') + '"></li>';
                    slideItem += '<div class="item ' + (i == 0 ? 'active' : '') + '"><img class="img-resposive" width="750px" height="500px" style="max-height:500px"  alt="" src="' + item + '"></div>';
                });

                $('.carousel-indicators').html(ol);
                $('.carousel-inner').html(slideItem);
            },
            error: function (err) {
                bootbox.alert('Have a error !!! Please check again : ' + err);
            }
        });
    }

}

productController.init();