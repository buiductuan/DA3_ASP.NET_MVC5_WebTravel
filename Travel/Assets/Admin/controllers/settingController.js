var settingController = {
    init: function () {
        settingController.registerEvent();
    },
    registerEvent: function () {
        $(window).load(function () {
            var prefix_code = $('#Prefix').val();
            $('#code_Prefix').html(prefix_code);
            var suffix_code = $('#Suffix').val();
            $('#code_Suffix').html(suffix_code);
        });
        //Code
        $('#Prefix').keyup(function () {
            var prefix_code = $('#Prefix').val();
            $('#code_Prefix').html(prefix_code);
        });
        $('#Suffix').keyup(function () {
            var suffix_code = $('#Suffix').val();
            $('#code_Suffix').html(suffix_code);
        });

        $('#btn_Save_Setting').off('click').on('click', function () {
            var NameWebsite = $('#NameWebsite').val();
            var MetaWebsite = $('#MetaWebsite').val();
            var Description = $('#Description').val();
            var EmailManage = $('#EmailManage').val();
            var EmailNotification = $('#EmailNotification').val();
            var NameCompany = $('#NameCompany').val();
            var Phone = $('#Phone').val();
            var Location = $('#Location').val();
            var Province = $('#Province').val();
            var Country = $('#Country').val();
            var Timezone = $('#Timezone').val();
            var Currency = $('#Currency').val();
            var Prefix = $('#Prefix').val();
            var Suffix = $('#Suffix').val();
            var CodeAnalytics = $('#CodeAnalytics').val();
            var payment_terms = $('#Payment_terms').val();
            var data = {
                ID: 1,
                NameWebsite: NameWebsite,
                MetaWebsite: MetaWebsite,
                Description: Description,
                EmailManage: EmailManage,
                EmailNotification: EmailNotification,
                NameCompany: NameCompany,
                Phone: Phone,
                Location: Location,
                Province: Province,
                Country: Country,
                Timezone: Timezone,
                Currency: Currency,
                Prefix: Prefix,
                Suffix: Suffix,
                CodeAnalytics: CodeAnalytics,
                Payment_terms:payment_terms
            }
            settingController.Update_Setting(data);
        });
    },
    Update_Setting: function (data) {
        $.ajax({
            url: '/Admin/Setting/Update',
            method: 'POST',
            data: {
                strSys: JSON.stringify(data)
            },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    bootbox.alert({
                        title:"Sửa thành công",
                        message:'Bạn đã sửa thành công cấu hình chung của website'
                    });
                }
            },
            error: function (err) {
                bootbox.alert("Have a error !!! Don't update now");
            }
        });
    }
}
settingController.init();