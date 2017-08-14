var newsletter = {
    init: function () {
        newsletter.registerEnvent();
    },
    registerEnvent: function () {

        $('.Selector').click(function () {
            var text_emoij = $('#btn_emoij').val();
            var id = $('#btn_emoij').data('id');
            newsletter.update_emoij(id, text_emoij);
        });

        //$("input[type=hidden]").trigger("change", function () {

        //});
        //$("#btn_emoij").bind('change',function (e) {
        //});
        //$('.faceMocion .angry').click(function () {
        //    var text_emoij = $('#btn_emoij').val();
        //    var id = $('#btn_emoij').data('id');
        //    newsletter.update_emoij(id, text_emoij);
        //});
        //$('.faceMocion .scare').click(function () {
        //    var text_emoij = $('#btn_emoij').val();
        //    var id = $('#btn_emoij').data('id');
        //    newsletter.update_emoij(id, text_emoij);
        //});
        //$('.faceMocion .enjoy').click(function () {
        //    var text_emoij = $('#btn_emoij').val();
        //    var id = $('#btn_emoij').data('id');
        //    newsletter.update_emoij(id, text_emoij);
        //});
        //$('.faceMocion .like').click(function () {
        //    var text_emoij = $('#btn_emoij').val();
        //    var id = $('#btn_emoij').data('id');
        //    newsletter.update_emoij(id, text_emoij);
        //});
        //$('.faceMocion .sad').click(function () {
        //    var text_emoij = $('#btn_emoij').val();
        //    var id = $('#btn_emoij').data('id');
        //    newsletter.update_emoij(id, text_emoij);
        //});
        //$('.faceMocion .glad').click(function () {
        //    var text_emoij = $('#btn_emoij').val();
        //    var id = $('#btn_emoij').data('id');
        //    newsletter.update_emoij(id, text_emoij);
        //});
    },
    update_emoij: function (id, text_emoij) {
        $.ajax({
            url: '/Newsletter/Up_Emoij',
            method: 'POST',
            data: { id: id, text_emoij: text_emoij },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    bootbox.alert("Cảm ơn bạn đã đưa ra cảm nhận với tin tức của chúng tôi !!! Chúc bạn một ngày tốt lành");
                }
            },
            error: function (err) {
                bootbox.alert("Có vẻ đã có một lỗi nào đó xảy ra !!! Vui lòng kiểm tra lại : " + err);
            }
        });
    }
}

newsletter.init();