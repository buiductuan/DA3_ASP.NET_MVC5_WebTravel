var payController = {
    init: function () {
        payController.registerEvent();
    },
    registerEvent: function () {
        payController.loadData(1);
        //onepay ATM
        $(".btn-onepay-atm-setting").off('click').on('click', function () {
            $('#hidden').removeAttr("hidden");
            $(".btn-onepay-atm-setting").hide();
        });
        $('.btn-unchange').off('click').on('click', function () {
            $(".btn-onepay-atm-setting").show();
            $('#hidden').attr("hidden", "hidden");
        });
        var status_access = false; var status_hash = false;
        $('.show_access_code').off('click').on('click', function () {
            status_access = !status_access;
            if (status_access) {
                $('#access_code').attr("type", "text");
            } else {
                $('#access_code').attr("type", "password");
            }

        });
        $('.show_hash_code').off('click').on('click', function () {
            status_hash = !status_hash;
            if (status_hash) {
                $('#hash_code').attr("type", "text");
            } else {
                $('#hash_code').attr("type", "password");
            }

        });
        $('.btn-save-onepay-atm').off('click').on('click', function () {
            var name = $('#name_OnePayATM').val();
            var account = $('#account_Merchant').val();
            var hashcode = $('#hash_code').val();
            var access = $('#access_code').val();
            var data = {
                ID: 1,
                Name: name,
                MerchantAccount: account,
                HashCode: hashcode,
                AccessCode: access
            }
            payController.saveData(data);
            payController.loadData(1);
            NProgress.set(1.2);
        });
    },
    loadData: function (id) {
        $.ajax({
            url: '/Admin/Pay/GetPay',
            method: 'GET',
            data: { id: id },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    $('#name_OnePayATM').val(res.data.Name);
                    $('#account_Merchant').val(res.data.MerchantAccount);
                    $('#hash_code').val(res.data.HashCode);
                    $('#access_code').val(res.data.AccessCode);
                }
            },
            error: function (err) {
                bootbox.alert('Have a error !!! Please check again');
            }
        });
    },
    saveData: function (data) {
        $.ajax({
            url: '/Admin/Pay/SaveData',
            method: 'POST',
            data: { data: JSON.stringify(data) },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    bootbox.alert("Lưu thành công cấu hình thanh toán của bạn");
                }
            },
            error: function (err) {
                bootbox.alert("Have a error !!! Please check again ");
            }
        });
    }
}
payController.init();