var feedBackController = {
    init: function () {
        feedBackController.registerEvent();
    },
    registerEvent: function () {
        $('.btn_view').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            feedBackController.view_detail(id);
            feedBackController.changeRead(id);
        });

        $('.btn_Delete').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Bạn có muốn xóa phản hồi này không ???", function (result) {
                if (result == true) {
                    feedBackController.Delete(id);
                }
            });
        });

        $('.btn-show-home').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            feedBackController.changeShow(id);
        });
    },
    view_detail: function (id) {
        $.ajax({
            url: '/Admin/Feedback/ViewDetail',
            method: 'POST',
            data: { id: id },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    var dialog = bootbox.dialog({
                        message: res.data.Content,
                    });
                }
            },
            error: function (err) {
                bootbox.alert("Have a error !!! Please check again");
            }
        });
    },
    changeRead: function (id) {
        $.ajax({
            url:'/Admin/Feedback/ChangeRead',
            method:'POST',
            data:{id : id},
            dataType:'json',
            success: function (res) {
                if (res.status == true) {
                    $('#status_read').html("<label class=\"label label-success\">Đã xem</label>");
                }
            },
            error: function (err) {
                bootbox.alert("Have a error !!!Please check again ");
            }
        });
    },
    changeShow: function (id) {
        $.ajax({
            url: '/Admin/Feedback/ChangeShow',
            method: 'POST',
            data: { id: id },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    $('#show_'+id).html("<label class=\"label label-success\">Hiển thị</label>");
                } else {
                    $('#show_' + id).html("<label class=\"label label-danger\">Ẩn</label>");
                }
            },
            error: function (err) {
                bootbox.alert("Have a error !!!Please check again ");
            }
        });
    },
    Delete: function (id) {
        $.ajax({
            url: '/Admin/Feedback/Delete',
            method: 'POST',
            data: { id: id },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    $('.row_'+id).remove();
                }
            },
            error: function (err) {
                bootbox.alert("Have a error !!!Please check again ");
            }
        });
    }
}
feedBackController.init();