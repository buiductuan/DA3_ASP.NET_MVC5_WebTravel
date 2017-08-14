var validation = {
    init: function () {
        validation.registerEvent();
    },
    registerEvent: function () {
        validation.valid_Form_Contact();
    },
    valid_Form_Contact: function () {
        $('.btn_sendFeedback').click(function () {
            var name = $('#Name').val();
            var email = $('#Email').val();
            var content = $('#Content').val();

            
            return true;
        });
    }
}

validation.init();