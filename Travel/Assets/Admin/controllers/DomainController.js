var domainController = {
    init: function () {
        $.ajaxStart(function () {
            NProgress.start();
        });
        domainController.LoadData();
        domainController.registerEvent();

        $.ajaxStop(function () {
            NProgress.done();
        });
    },

    registerEvent: function () {
        $('.btn-remove').off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            bootbox.confirm("Xoas", function (result) {
                if (result == true) {
                    domainController.Delete(id);
                };
            });
        });
    },
    LoadData: function () {
        $.ajax({
            url: '/Admin/Domain/LoadData',
            method: 'GET',
            success: function (res) {
                if (res.status == true) {
                    var data = res.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            Name: item.Name,
                            Status: item.Status == true ? "<i class=\"fa fa-check\"></i> OK" : "<i class=\"fa fa-times\"></i> NO",
                            Just: item.Just == true ? "<i class=\"fa fa-check\"></i> OK" : "<i class=\"fa fa-times\"></i> NO"
                        });
                    });
                    $('#tblData').html(html);
                }
            }
        });
    },
    Delete: function (id) {
        $.ajax({
            url: '/Admin/Domain/Delete',
            method: 'GET',
            data: { id: id },
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {
                    domainController.LoadData();
                }
            }
        });
    }
}
domainController.init();