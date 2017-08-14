$(function () {

    var valid_Login = function () {
        $('#btnSignIn').click(function () {
            var userName = $('#UserName').val();
            var password = $('#Password').val();
            if (userName == "") {
                $('#error_UserName').addClass('error_label');
                $('#error_UserName').html('Nhập tên đăng nhập');
                $('#UserName').addClass('invalid');
                $('#UserName').focus();
                return false;
            } else {
                if (userName.length > 50) {
                    $('#error_UserName').addClass('error_label');
                    $('#error_UserName').html('Tên đăng nhập không quá 50 ký tự');
                    $('#UserName').addClass('invalid');
                    $('#UserName').focus();
                    return false;
                } else {
                    if (password == "") {
                        $('#error_Password').html("Nhập mật khẩu");
                        $("#error_Password").addClass('error_label');
                        $("#Password").addClass('invalid');
                        $('#Password').focus();
                        return false;
                    } else {
                        if (password.length > 32) {
                            $('#error_Password').html("Mật khẩu không quá 32 ký tự");
                            $("#error_Password").addClass('error_label');
                            $("#Password").addClass('invalid');
                            $('#Password').focus();
                            return false;
                        }
                    }
                }
            }
            return true;
        });
        $('#UserName').keyup(function () {
            var userName = $('#UserName').val();
            if (userName == "") {
                $('#error_UserName').addClass('error_label');
                $('#error_UserName').html('Nhập tên đăng nhập');
                $('#UserName').addClass('invalid');
                $('#UserName').focus();
            } else {
                if (userName.length > 50) {
                    $('#error_UserName').addClass('error_label');
                    $('#error_UserName').html('Tên đăng nhập không quá 50 ký tự');
                    $('#UserName').addClass('invalid');
                    $('#UserName').focus();
                } else {
                    $('#error_UserName').removeClass('error_label');
                    $('#error_UserName').html('Tên đăng nhập');
                    $('#UserName').removeClass('invalid');
                    $('#UserName').focus();
                }
            }
        });
        $('#Password').keyup(function () {
            var password = $('#Password').val();
            if (password == "") {
                $('#error_Password').html("Nhập mật khẩu");
                $("#error_Password").addClass('error_label');
                $("#Password").addClass('invalid');
                $('#Password').focus();
            } else {
                if (password.length > 32) {
                    $('#error_Password').html("Mật khẩu không quá 32 ký tự");
                    $("#error_Password").addClass('error_label');
                    $("#Password").addClass('invalid');
                    $('#Password').focus();
                } else {
                    $('#error_Password').html("Mật khẩu");
                    $("#error_Password").removeClass('error_label');
                    $("#Password").removeClass('invalid');
                }
            }
        });
    }

    var valid_product = function () {

        $('.btn-save').click(function () {
            var name = $('#Name').val();
            name = name.trim();
            var description = $('#Description').val();
            description = description.trim();
            var duration = $('#Duration').val();
            var departure = $('#Departure').val();
            var Quantity = $('#Quantity').val();
            var placeOfDeparture = $('#PlaceofDeparture').val();
            placeOfDeparture = placeOfDeparture.trim();
            var price = $('#Price').val();
            var metaTile = $('#MetaTitle').val();
            metaTile = metaTile.trim();

            //regex
            var regNumber = /^\d+$/;
            if (name == "") {
                $('#Name').val(name.trim());
                $('#error_NameProduct').html('Vui lòng điền tên sản phẩm');
                $('#error_NameProduct').addClass('error_label');
                $('#Name').addClass('invalid');
                $('#Name').focus();
                return false;
            }
            else {
                name.trim();
                if (name.length < 50) {
                    $('#Name').val(name.trim());
                    $('#error_NameProduct').html('Tên sản phẩm tối thiểu chứa 50 ký tự');
                    $('#error_NameProduct').addClass('error_label');
                    $('#Name').addClass('invalid');
                    $('#Name').focus();
                    return false;
                }
                else {
                    if (name.length > 250) {
                        $('#Name').val(name.trim());
                        $('#error_NameProduct').html('Tên sản phẩm chứa tối đa 250 ký tự');
                        $('#error_NameProduct').addClass('error_label');
                        $('#Name').addClass('invalid');
                        $('#Name').focus();
                        return false;
                    }
                    else {
                        if (description == "") {
                            $('#Description').val(description.trim());
                            $('#error_DescriptionProduct').html('Vui lòng điền mô tả cho sản phẩm');
                            $('#error_DescriptionProduct').addClass('error_label');
                            $('#Description').addClass('invalid');
                            $('#Description').focus();
                            return false;
                        }
                        else {
                            if (description.length < 100) {
                                $('#Description').val(description.trim());
                                $('#error_DescriptionProduct').html('Mô tả của sản phẩm tối thiểu là 100 ký tự');
                                $('#error_DescriptionProduct').addClass('error_label');
                                $('#Description').addClass('invalid');
                                $('#Description').focus();
                                return false;
                            } else {
                                if (duration == "") {
                                    $('#error_DurationProduct').html('Vui lòng nhập số ngày');
                                    $('#error_DurationProduct').addClass('error_label');
                                    $('#Duration').addClass('invalid');
                                    $('#Duration').focus();
                                    return false;
                                }
                                else {
                                    if (!regNumber.test(duration) || duration < 0) {
                                        $('#error_DurationProduct').html('Số ngày phải là số lớn hơn 0');
                                        $('#error_DurationProduct').addClass('error_label');
                                        $('#Duration').addClass('invalid');
                                        $('#Duration').focus();
                                        return false;
                                    } else if (departure == "") {
                                        $('#error_DepartureProduct').html('Vui lòng điền ngày khởi hành');
                                        $('#error_DepartureProduct').addClass('error_label');
                                        $('#Departure').addClass('invalid');
                                        $('#Departure').focus();
                                        return false;
                                    } else if (Quantity == "") {
                                        $('#error_QuantityProduct').html('Vui lòng điền số người');
                                        $('#error_QuantityProduct').addClass('error_label');
                                        $('#Quantity').addClass('invalid');
                                        $('#Quantity').focus();
                                        return false;
                                    } else if (!regNumber.test(Quantity) || Quantity.length < 0) {
                                        $('#error_QuantityProduct').html('Số người phải là số lơn hơn 0');
                                        $('#error_QuantityProduct').addClass('error_label');
                                        $('#Quantity').addClass('invalid');
                                        $('#Quantity').focus();
                                        return false;
                                    } else if (placeOfDeparture == "") {
                                        $('#PlaceofDeparture').val(placeOfDeparture.trim());
                                        $('#error_PlaceofDepartureProduct').html('Vui lòng điền nơi khởi hành');
                                        $('#error_PlaceofDepartureProduct').addClass('error_label');
                                        $('#PlaceofDeparture').addClass('invalid');
                                        $('#PlaceofDeparture').focus();
                                        return false;
                                    } else if (price == "") {
                                        $('#Price').val("");
                                        $('#error_PriceProduct').html('Vui lòng điền giá cho sản phẩm');
                                        $('#error_PriceProduct').addClass('error_label');
                                        $('#Price').addClass('invalid');
                                        $('#Price').focus();
                                        return false;
                                    } else if (price < 0 || !regNumber.test(price)) {
                                        $('#Price').val("");
                                        $('#error_PriceProduct').html('Giá sản phẩm phải là số lớn hơn 0');
                                        $('#error_PriceProduct').addClass('error_label');
                                        $('#Price').addClass('invalid');
                                        $('#Price').focus();
                                        return false;
                                    } else if (metaTile == "") {
                                        $('#MetaTitle').val(metaTile.trim());
                                        $('#error_MetaTitleProduct').html('Vui lòng điền thẻ tiêu đề để tối ưu SEO cho sản phẩm');
                                        $('#error_MetaTitleProduct').addClass('error_label');
                                        $('#MetaTitle').addClass('invalid');
                                        $('#MetaTitle').focus();
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return true;
        });

        $('#Description').keyup(function () {
            var des = $('#Description').val();
            if (des.Trim() == "") {
                $('#error_DescriptionProduct').html("Vui lòng nhập mô tả sản phẩm");
                $("#error_DescriptionProduct").addClass('error_label');
                $("#Description").addClass('invalid');
                $('#Description').focus();
            } else {
                if (des.length < 100) {
                    $('#error_DescriptionProduct').html('Mô tả của sản phẩm tối thiểu là 100 ký tự');
                    $('#error_DescriptionProduct').addClass('error_label');
                    $('#Description').addClass('invalid');
                    $('#Description').focus();
                } else {
                    $('#error_DescriptionProduct').html("Mô tả");
                    $("#error_DescriptionProduct").removeClass('error_label');
                    $("#Description").removeClass('invalid');
                }
            }
        });

        $('#Duration').keyup(function () {
            var duration = $('#Duration').val();
            var regNumber = "/^\d+$/";
            if (duration == "") {
                $('#error_DurationProduct').html("Vui lòng nhập số ngày");
                $("#error_DurationProduct").addClass('error_label');
                $("#Duration").addClass('invalid');
                $('#Duration').focus();
            } else {
                if (!regNumber.test(duration) || duration < 0) {
                    $('#error_DurationProduct').html('Số ngày phải là số lớn hơn 0');
                    $('#error_DurationProduct').addClass('error_label');
                    $('#Duration').addClass('invalid');
                    $('#Duration').focus();
                } else {
                    $('#error_DurationProduct').html('Số ngày');
                    $('#error_DurationProduct').removeClass('error_label');
                    $('#Duration').removeClass('invalid');
                }
            }
        });

        $('#Departure').keyup(function () {
            var duration = $('#Departure').val();
            var regNumber = "/^\d+$/";
            if (duration == "") {
                $('#error_DepartureProduct').html("Vui lòng nhập ngày khởi hành");
                $("#error_DepartureProduct").addClass('error_label');
                $("#Departure").addClass('invalid');
                $('#Departure').focus();
            } else {
                $('#error_DepartureProduct').html("Ngày khởi hành");
                $("#error_DepartureProduct").removeClass('error_label');
                $("#Departure").removeClass('invalid');
            }
        });
    }
    //call Event
    $(function () {
        valid_Login();
        valid_product();
    });
} ());