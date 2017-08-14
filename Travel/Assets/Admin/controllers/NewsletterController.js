var news = {
    
    init: function () {
        news.registerEvent();
    },
    registerEvent: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var b = $(this);
            var id = b.data('id');
            $.ajax({
                url: '/Admin/Newsletter/ChangeStatus',
                method:'POST',
                data:{id : id},
                dataType:'json',
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
        news.searchAutoComplete();

        //get name by metatile
        $(".btn_getname").off('click').on('click', function () {
            var name = $('#Name').val();
            if (name != "") {
                $('#MetaTitle').val(name);
            } else {
                $('#error_NameNewsletter').html("Vui lòng điền tiêu đề tin tức");
                $('#error_NameNewsletter').addClass('error_label');
                $('#Name').addClass('invalid');
                $('#Name').focus();
            }
        });
    },
    searchAutoComplete: function () {
        $.ajax({
            url: '/Admin/Newsletter/GetDataBindToAutoComplete',
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
news.init();
