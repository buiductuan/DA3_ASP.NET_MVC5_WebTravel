var discountController = {
    init: function () {
        discountController.registerEvent();
    },
    registerEvent: function () {

        $(window).load(function () {
            $('#Amount').val("∞");
        });
        // check amount
        $('#Check_amount_discount').off('click').on('click', function () {
            if (this.checked) {
                $('#Amount').val(" ");
                $('#Amount').attr("disabled", "disabled");
                $('#Amount').attr("type", "text");
                $('#Amount').val("∞");
            }
            else {
                $('#Amount').attr("type", "number");
                $('#Amount').removeAttr("disabled");
                $('#Amount').focus();
            }
        });
        //auto code
        $('.btn_auto_code').off('click').on('click', function () {
            var text = "ABCDEFGHYKLMNOPQXYZ";
            var number = "0123456789";
            var text_ran = text[Math.floor(Math.random() * 10)];
            var text_ran2 = text[Math.floor(Math.random() * 12)];
            var text_ran3 = text[Math.floor(Math.random() * 16)];
            var text_ran4 = text[Math.floor(Math.random() * 14)];
            var number_ran = number[Math.floor(Math.random() * 5)];
            var number_ran2 = number[Math.floor(Math.random() * 3)];
            var number_ran3 = number[Math.floor(Math.random() * 8)];
            var number_ran4 = number[Math.floor(Math.random() * 6)];
            $('#ID').val(text_ran + number_ran + text_ran4 + text_ran2 + number_ran2 + number_ran3 + number_ran4 + text_ran3);
            $('#error_ID_Discount').html("Mã khuyến mãi");
            $('#error_ID_Discount').removeClass('error_label');
            $('#ID').removeClass('invalid');
        });
        //select condition
        $('#select_condition').change(function () {
            var id = $('#select_condition option:selected').val();
            if (id == "vnd") {
                $('#discount_price').removeAttr("hidden");
                $('#Disprice').removeAttr("disabled");
                $('#discount_percent').attr("hidden", "hidden");
                $('#Dispercent').attr("disabled", "disabled");
                $('#Disprice').focus();
            } else {
                $('#discount_percent').removeAttr("hidden");
                $('#Dispercent').removeAttr("disabled");
                $('#Disprice').attr("disabled", "disabled");
                $('#discount_price').attr("hidden", "hidden");
                $('#Dispercent').focus();
            }
        });
        // date end 
        $('#check_time-expired').off('click').on('click', function () {
            if (this.checked) {
                $('#DayEnd').attr("disabled", "disabled");
            } else {
                $('#DayEnd').removeAttr("disabled");
            }
        });

        discountController.validation();

        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            discountController.changeStatus(id);
        });
    },
    validation: function () {
        $('.btn-save').off('click').on('click', function () {
            var id = $('#ID').val();
            var disPrice = $('#Disprice').val();
            var disPrecent = $('#Dispercent').val();
            var dayStart = $("#DayStart").val();
            var dayEnd = $("#DayEnd").val();
            var regNumber = /^\d+$/;
            if (id == "") {
                $('#error_ID_Discount').html("Vui lòng điền mã khuyến mãi hoặc chọn mã tự động");
                $('#error_ID_Discount').addClass('error_label');
                $('#ID').addClass('invalid');
                $('#ID').focus();
                return false;
            }else if (id.length > 10) {
                $('#error_ID_Discount').html("Mã khuyến mãi không vượt quá 10 ký tự");
                $('#error_ID_Discount').addClass('error_label');
                $('#ID').addClass('invalid');
                $('#ID').focus();
                return false;
            } else if (disPrice == "") {
                $('#Disprice').addClass('invalid');
                $('#Disprice').focus();
                return false;
            } else if (!regNumber.test(disPrice)) {
                $('#Disprice').addClass('invalid');
                $('#Disprice').focus();
                return false;
            } else if (dayStart == "") {
                $('#DayStart').attr("placeholder", "Vui lòng điền ngày bắt đầu");
                $('#DayStart').addClass('invalid');
                $('#DayStart').focus();
                return false;
            }
            return true;
        });
    },
    changeStatus: function (id) {
        $.ajax({
            url: '/Admin/Discount/ChangeStatus',
            method: 'POST',
            data: { id: id },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    $(".btn-active").html("<label class=\"label label-success btn-lg\">Ngừng</label>");
                } else {
                    $(".btn-active").html("<label class=\"label label-danger btn-lg\">Tiếp tục</label>");
                }
            },
            error: function (err) {
                bootbox.alert("Have a error !!! Please check again");
            }
        })

    }
}
discountController.init();