var menu = {
    init: function () {
        menu.registerEvent();
    },
    registerEvent: function () {
        //Add Menu
        $('#addMenutype').off('click').on('click', function () {
            menu.resetForm();
        });
        $('#btnSaveMenuType').off('click').on('click', function () {
            NProgress.start();
            menu.saveMenuType();
        });
        //Change status
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var t = $(this);
            var id = t.data('id');
            $.ajax({
                url: '/Admin/Menu/ChangeStatus',
                method: 'POST',
                data: { id: id },
                dataType: 'json',
                success: function (res) {
                    if (res.status == true) {
                        t.html("<label class=\"label label-success\">Hiển thị</label>");
                    } else {
                        t.html("<label class=\"label label-danger\">Ẩn</label>");
                    }
                },
                error: function (err) {
                    bootbox.alert("Have error !!! Check again");
                }
            });
        });
        //delete
        $('.btn_delete').off('click').on('click', function (e) {
            e.preventDefault();
            var t = $(this);
            var id = t.data('id');
            var row = $('.row_' + id);
            bootbox.confirm("Bạn có muốn xóa menu này không ??? ", function (result) {
                if (result == true) {
                    menu.delete_menu(id, row);
                }
            });
        });
    },
    resetForm: function () {
        $('#error_Name_MenuType').html("Loại menu")
        $('#txtNameMenuType').val('');
    },
    saveMenuType: function () {
        var nameMenuType = $('#txtNameMenuType').val();
        var menuType = {
            Name: nameMenuType
        }
        $.ajax({
            url: '/Admin/Menu/CreateMenuType',
            method: 'POST',
            data: {
                strMenuType: JSON.stringify(menuType)
            },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    //$('#modal_add_menuType').modal('hide');
                    $('#error_Name_MenuType').html(res.message);
                    $('#error_Name_MenuType').addClass('success_label');
                } else {
                    $('#error_Name_MenuType').html(res.message);
                    $('#error_Name_MenuType').addClass('error_label');
                    $('#txtNameMenuType').addClass('invalid');
                    $('#txtNameMenuType').focus();
                }
            },
            error: function (err) {
                alert(err);
            }
        });
    },
    delete_menu: function (id, row) {
        $.ajax({
            url: '/Admin/Menu/DeleteAjax',
            method: 'POST',
            data: { id: id },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    row.remove();
                }
            },
            error: function (err) {
                bootbox.alert("Have a error !!! Don't delete menu ");
            }
        });
    }
}
menu.init();