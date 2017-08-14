var notificationController = {
    init: function () {
        notificationController.registerEvent();
    },
    registerEvent: function () {
        $('.btn_update').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $('#modal_Notification').modal('show');
            notificationController.ViewDetail(id);
        });

        $('.btn_Save').off('click').on('click', function () {
            var id = $('.getID').val();
            var emailTitle = $('.txtEmailTitle').val();
            var emailContent = $('.email-content').val();
            var n = {
                ID: id,
                EmailTitle: emailTitle,
                EmailContent: emailContent
            }
            notificationController.UpdateData(n);
        });
    },
    UpdateData: function (data) {
        $.ajax({
            url: '/Admin/Notification/Update',
            method: 'POST',
            data: {
                entity: JSON.stringify(data)
            },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    bootbox.alert('Cập nhật cấu hình thông báo thành công ');
                    //notificationController.ViewDetail(id);
                }
            },
            error: function (err) {
                bootbox.alert('Have a error !!! Please check again <br>' + err);
            }
        });
    },
    ViewDetail: function (id) {
        $.ajax({
            url: '/Admin/Notification/ViewDetail',
            method: 'POST',
            data: {
                id: id
            },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    $('.getID').val(res.id);
                    $('.modal-title').html('Chỉnh sửa mẫu email "' + res.pattern + '"');
                    $('.txtEmailTitle').val(res.emailTitle);
                    $('.email-content').html(res.emailContent);
                }
            },
            error: function (err) {
                bootbox.alert('Have a error !!! Please check again');
            }
        });
    }
}
notificationController.init();