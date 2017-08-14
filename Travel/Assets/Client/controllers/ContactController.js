var contactController = {
    init: function () {
        contactController.registerEvent();
    },
    registerEvent: function () {
        $('[data-toggle="popover"]').popover();
        $('.btn_sendFeedback').off('click').on('click', function () {

            var name = $('#Name').val();
            var email = $('#Email').val();
            var content = $('#Content').val();
            var regmail = /^([\w\.])+@([a-zA-Z0-9\-])+\.([a-zA-Z]{2,4})(\.[a-zA-Z]{2,4})?$/;

            if (name == "") {
                $('#error_Name_Contact').html('Vui lòng điền họ và tên');
                $('#error_Name_Contact').addClass('error_label');
                $('#Name').addClass('invalid');
                $('#Name').focus();
            } else {
                if (name.length > 100) {
                    $('#error_Name_Contact').html('Họ và tên dưới 100 ký tự');
                    $('#error_Name_Contact').addClass('error_label');
                    $('#Name').addClass('invalid');
                    $('#Name').focus();
                } else {
                    if (email == "") {
                        $('#error_Email_Contact').html('Vui lòng điền email của bạn');
                        $('#error_Email_Contact').addClass('error_label');
                        $('#Email').addClass('invalid');
                        $('#Email').focus();
                    } else {
                        if (!regmail.test(email)) {
                            $('#error_Email_Contact').html('Email không hợp lệ');
                            $('#error_Email_Contact').addClass('error_label');
                            $('#Email').addClass('invalid');
                            $('#Email').focus();
                        } else {
                            if (email.lenght > 100) {
                                $('#error_Email_Contact').html('Email không quá 100 ký tự');
                                $('#error_Email_Contact').addClass('error_label');
                                $('#Email').addClass('invalid');
                                $('#Email').focus();
                            } else {
                                if (content == "") {
                                    $('#error_Message_Contact').html('Vui lòng điền nội dung của bạn');
                                    $('#error_Message_Contact').addClass('error_label');
                                    $('#Content').addClass('invalid');
                                    $('#Content').focus();
                                } else {
                                    var data = {
                                        Name: name,
                                        Email: email,
                                        Content: content
                                    }
                                    contactController.sendFeed(data);
                                    var name = $('#Name').val("");
                                    var email = $('#Email').val("");
                                    var content = $('#Content').val("");
                                }
                            }
                        }
                    }
                }
            }
        });

        $('#Name').keyup(function () {
            var name = $('#Name').val();
            if (name == "") {
                $('#error_Name_Contact').html('Vui lòng điền họ và tên');
                $('#error_Name_Contact').addClass('error_label');
                $('#Name').addClass('invalid');
                $('#Name').focus();
            } else {
                if (name.length > 100) {
                    $('#error_Name_Contact').html('Họ và tên dưới 100 ký tự');
                    $('#error_Name_Contact').addClass('error_label');
                    $('#Name').addClass('invalid');
                    $('#Name').focus();
                } else {
                    $('#error_Name_Contact').html('');
                    $('#error_Name_Contact').removeClass('error_label');
                    $('#Name').removeClass('invalid');
                    $('#Name').focus();
                }
            }
        });
        $('#Email').keyup(function () {
            var email = $('#Email').val();
            var regmail = /^([\w\.])+@([a-zA-Z0-9\-])+\.([a-zA-Z]{2,4})(\.[a-zA-Z]{2,4})?$/;
            if (email == "") {
                $('#error_Email_Contact').html('Vui lòng điền email của bạn');
                $('#error_Email_Contact').addClass('error_label');
                $('#Email').addClass('invalid');
                $('#Email').focus();
            } else {
                if (!regmail.test(email)) {
                    $('#error_Email_Contact').html('Email không hợp lệ');
                    $('#error_Email_Contact').addClass('error_label');
                    $('#Email').addClass('invalid');
                    $('#Email').focus();
                } else {
                    if (email.lenght > 100) {
                        $('#error_Email_Contact').html('Email không quá 100 ký tự');
                        $('#error_Email_Contact').addClass('error_label');
                        $('#Email').addClass('invalid');
                        $('#Email').focus();
                    } else {
                        $('#error_Email_Contact').html('');
                        $('#error_Email_Contact').removeClass('error_label');
                        $('#Email').removeClass('invalid');
                    }
                }
            }
        });
        $('#Content').keyup(function () {
            var content = $('#Content').val();
            if (content == "") {
                $('#error_Message_Contact').html('Vui lòng điền họ và tên');
                $('#error_Message_Contact').addClass('error_label');
                $('#Content').addClass('invalid');
                $('#Content').focus();
            } else {
                $('#error_Message_Contact').html('');
                $('#error_Message_Contact').removeClass('error_label');
                $('#Content').removeClass('invalid');
                $('#Content').focus();
            }
        });
    },
    sendFeed: function (data) {
        $.ajax({
            url: '/Contact/SendFeedAjax',
            method: 'POST',
            data: { feed: JSON.stringify(data) },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    bootbox.alert("Cảm ơn bạn đã gửi lại phản hồi cho TravelHappy. Chúc bạn một ngày tốt lành.");
                }
            },
            error: function (err) {
                bootbox.alert("Have a error !!! Please check again");
            }
        });
    }
}

contactController.init();