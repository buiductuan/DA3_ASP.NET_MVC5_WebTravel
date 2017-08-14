var registrationController = {
    init: function () {
        registrationController.registerEvent();
    },
    registerEvent: function () {
        $('.btn-save').off('click').on('click', function () {

            var name = $('#Name').val();
            var email = $('#Email').val();
            var UserName = $('#UserName').val();
            var password = $('#Password').val();
            var password_confirmation = $('#password_confirmation').val();
            var regmail = /^([\w\.])+@([a-zA-Z0-9\-])+\.([a-zA-Z]{2,4})(\.[a-zA-Z]{2,4})?$/;
            if (name == "") {
                $('#Name').attr("placeholder", "Vui lòng điền họ và tên");
                $('#Name').addClass("invalid");
                $('#Name').focus();
                return false;
            } else if (name.length > 100) {
                $('#Name').attr("placeholder", "Họ và tên không quá 100 ký tự");
                $('#Name').addClass("invalid");
                $('#Name').focus();
                return false;
            } else if (name.length < 5) {
                $('#Name').attr("placeholder", "Họ và tên bắt đầu từ 5 ký tự");
                $('#Name').addClass("invalid");
                $('#Name').focus();
                return false;
            } else if (email == "") {
                $('#Email').attr("placeholder", "Vui lòng điền email của bạn");
                $('#Email').addClass("invalid");
                $('#Email').focus();
                return false;
            } else if (!regmail.test(email)) {
                $('#Email').attr("placeholder", "Email không hợp lệ");
                $('#Email').addClass("invalid");
                $('#Email').focus();
                return false;
            } else if (registrationController.checkEmail(email) == false) {
                $('.error_UserName_SignUp').html("Tên tài khoản đã được sử dụng");
                $('#UserName').addClass('invalid');
                $('#UserName').focus();
                return false;
            } else if (UserName == "") {
                $('#UserName').attr("placeholder", "Vui lòng điền tên đăng nhập");
                $('#UserName').addClass("invalid");
                $('#UserName').focus();
                return false;
            } else if (registrationController.checkUserName(UserName) == false) {
                $('#UserName').attr("placeholder", "Vui lòng điền tên đăng nhập");
                $('#UserName').addClass("invalid");
                $('#UserName').focus();
                return false;
            } else if (password == "") {
                $('#Password').attr("placeholder", "Vui lòng điền mật khẩu");
                $('#Password').addClass("invalid");
                $('#Password').focus();
                return false;
            } else if (password_confirmation == "") {
                $('#password_confirmation').attr("placeholder", "Vui lòng nhập lại mật khẩu");
                $('#password_confirmation').addClass("invalid");
                $('#password_confirmation').focus();
                return false;
            } else if (password_confirmation != password) {
                $('#password_confirmation').attr("placeholder", "Nhập lại mật khẩu không chính xác");
                $('#password_confirmation').addClass("invalid");
                $('#password_confirmation').focus();
                return false;
            } else {
                var data = {
                    Name: name,
                    Email: email,
                    UserName: UserName,
                    Password: password
                }
                registrationController.register(data);
                //refresh form
                $('#Name').val("");
                $('#Email').val("");
                $('#UserName').val("");
                $('#Password').val("");
                $('#password_confirmation').val("");
                $('#Name').focus();
            }
        });

        $('#Name').keyup(function () {
            var name = $('#Name').val();
            if (name == "") {
                $('#Name').attr("placeholder", "Vui lòng điền họ và tên");
                $('#Name').addClass("invalid");
                $('#Name').focus();
                return false;
            } else if (name.length > 100) {
                $('#Name').attr("placeholder", "Họ và tên không quá 100 ký tự");
                $('#Name').addClass("invalid");
                $('#Name').focus();
                return false;
            } else if (name.length < 5) {
                $('#Name').attr("placeholder", "Họ và tên bắt đầu từ 5 ký tự");
                $('#Name').addClass("invalid");
                $('#Name').focus();
                return false;
            } else {
                $('#Name').attr("placeholder", "Họ và tên");
                $('#Name').removeClass("invalid");
            }
        });

        $("#UserName").keyup(function () {
            var username = $("#UserName").val();
            registrationController.checkUserName(username);
        });
        $("#Email").keyup(function () {
            var email = $("#Email").val();
            registrationController.checkEmail(email);
        });
    },
    register: function (data) {
        $.ajax({
            url: '/Registration/SignUp',
            method: 'POST',
            data: { data: JSON.stringify(data) },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    bootbox.alert('Đăng ký tài khoản thành công');
                }
            },
            error: function (err) {
                bootbox.alert("Have a error !!! Please check again ");
            }
        });
    },
    checkUserName: function (username) {
        $.ajax({
            url: '/Registration/CheckUserName',
            method: 'POST',
            data: { username: username },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    $('.error_UserName_SignUp').html("Tên tài khoản đã được sử dụng");
                    $('#UserName').addClass('invalid');
                    $('#UserName').focus();
                    return false;
                } else {
                    $('.error_UserName_SignUp').html("");
                    $('#UserName').removeClass('invalid');
                    $('#UserName').focus();
                }
            },
            error: function (err) {
                bootbox.alert("Have a error !!! Please check again ");
            }
        });
    },
    checkEmail: function (email) {
        $.ajax({
            url: '/Registration/CheckEmail',
            method: 'POST',
            data: { email: email },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    $('.error_Email_SignUp').html("Email đã được sử dụng");
                    $('#Email').addClass('invalid');
                    $('#Email').focus();
                    return false;
                } else {
                    $('.error_Email_SignUp').html("");
                    $('#Email').removeClass('invalid');
                }
            },
            error: function (err) {
                bootbox.alert("Have a error !!! Please check again ");
            }
        });
    }
}
registrationController.init();