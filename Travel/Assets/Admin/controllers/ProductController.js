var product = {

    init: function () {
        product.registerEvent();
    },
    registerEvent: function () {
        //Load pages
        $(window).load(function () {
            $('.form_note').hide();
        });

        //change status
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var b = $(this);
            var id = b.data('id');
            $.ajax({
                url: '/Admin/Product/ChangeStatus',
                method: 'POST',
                data: { id: id },
                dataType: 'json',
                success: function (res) {
                    if (res.status == true) {
                        b.html("<label class=\"label label-success\">Hiển thị</label>");
                    } else {
                        b.html("<label class=\"label label-danger\">Ẩn</label>");
                    }
                },
                error: function (err) {
                    bootbox.alert("Have a error !!! Please check again");
                }
            });
        });

        //check name
        $('#Name').keyup(function () {
            var name = $('#Name').val();
            $('#Name').removeClass('invalid');
            $('#error_NameProduct').removeClass('error_label');
            if (name == "") {
                $('#error_NameProduct').html('Vui lòng điền tên sản phẩm');
                $('#error_NameProduct').addClass('error_label');
                $('#Name').addClass('invalid');
                $('#Name').focus();
            }
            product.checkNameProduct(name);
        });

        //Delete product
        $('.btn_Delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm(
                "Bạn có muốn xóa sản phẩm có ID = " + id + " không ???",
                function (res) {
                    if (res == true) {
                        product.deleteProduct(id);
                    }
                });
        });

        //Show form_note
        $('.btn_show_note').off('click').on('click', function (e) {
            e.preventDefault();
            $('.form_note').addClass('animated fadeInUp');
            $('.form_note').show();
            $('.btn_show_note').hide();
        });

        //Get title by name
        $('.btn_getTitle').click(function (e) {
            e.preventDefault();
            var name = $('#Name').val();
            if (name == "") {
                $('#error_NameProduct').html('Vui lòng điền tên sản phẩm');
                $('#error_NameProduct').addClass('error_label');
                $('#Name').addClass('invalid');
                $('#Name').focus();
            }
            $('#MetaTitle').val(name);
        });

        product.searchAutoComplete();

        //manage images
        $('.btn-images').off('click').on('click', function (e) {
            e.preventDefault();
            $('#modal_Images').modal('show');
            $('#ProductID').val($(this).data('id'));
            product.loadImages();
        });

        $('#btn_choose_image').off('click').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('.list_images').append('<div class="img_box col-md-3 clearfix"><a href="#" class="btn_remove_image"><i class="fa fa-times pull-right"></i></a><br><img width="100" height="60" src = \"' + url + '\" /></div> ');
                $('.btn_remove_image').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                });
            }
            finder.popup();
        });

        $('.btn-save-image').off('click').on('click', function (e) {
            var images = [];
            var id = $('#ProductID').val();
            $.each($(".list_images img"), function (i, item) {
                images.push($(item).prop('src'));
            });
            $.ajax({
                url: '/Admin/Product/SaveMoreImages',
                method:'POST',
                data: {
                    id:id,
                    images: JSON.stringify(images)
                },
                dataType:'json',
                success: function (res) {
                    if (res.status == true) {
                        $('#modal_Images').modal('hide');
                        bootbox.alert("Lưu danh sách ảnh thành công");
                    } else {
                        $('#modal_Images').modal('hide');
                        bootbox.alert("Có lỗi xảy ra không thể danh sách ảnh");
                    }
                },
                error:function(err){
                    bootbox.alert("Have a error !!! Please check again : "+err);
            }
            });
        });
    },
    loadImages: function(){
        $.ajax({
            url: '/Admin/Product/LoadImages',
            method : 'GET',
            data: {
                id: $('#ProductID').val()
            },
            dataType:'json',
            success: function (res) {
                var data = res.data;
                var html = '';
                $('.list_images').html('');
                $.each(data, function (i, item) {
                    html += '<div class="img_box col-md-3 clearfix"><a href="#" class="btn_remove_image"><i class="fa fa-times pull-right"></i></a><br><img width="100" height="60" src = \"' + item + '\" /></div> ';
                });
                $('.list_images').append(html);
                $('.btn_remove_image').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();
                });
            },
            error: function (err) {
                bootbox.alert('Have a error !!! Please check again : ' + err);
            }
        });
    },
    checkNameProduct: function (name) {
        $.ajax({
            url: '/Admin/Product/CheckNameProduct',
            method: 'POST',
            data: { name: name },
            success: function (res) {
                if (res.status == true) {
                    $('#error_NameProduct').html('Tên sản phẩm đã tồn tại ! Xin vui lòng nhập tên khác');
                    $('#error_NameProduct').addClass('text-danger');
                    $('#error_NameProduct').removeClass('text-success');
                    $('#Name').addClass('invalid');
                } else {
                    $('#error_NameProduct').removeClass('text-danger');
                    $('#error_NameProduct').html('Tên sản phẩm có thể sử dụng');
                    $('#error_NameProduct').addClass('text-success');
                    $('#Name').removeClass('invalid');
                }
            },
            error: function (err) {
                bootbox.alert('Have a error ! Check again');
            }
        });
    },
    deleteProduct: function (id) {
        $.ajax({
            url: '/Admin/Product/DeleteAjax',
            method: 'POST',
            data: { id: id },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    $('.row_' + id).remove();
                }
            },
            error: function (err) {
                bootbox.alert("Have a error !!! Please check again");
            }
        });
    },
    searchAutoComplete: function () {
        $.ajax({
            url: '/Admin/Product/GetDataBindToAutoComplete',
            method: 'GET',
            data: {},
            dataType: 'json',
            success: function (res) {
                $('#txtSearch').autocomplete({
                    source: res.data
                });
            },
            error: function (err) {
                bootbox.alert("Have a error !!! Please check again : " + err);
            }
        });
    }
}
product.init();