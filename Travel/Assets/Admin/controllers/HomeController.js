$(document).ready(function () {
    $(window).load(function () {
        $('#modal_Report_Home_Admin').modal('show');
    });
    $.ajax({
        url: '/Admin/Home/GetDataMorris',
        method:'GET',
        data:{},
        dataType:'json',
        success: function (res) {
            new Morris.Line({
                // ID of the element in which to draw the chart.
                element: 'myfirstchart',
                // Chart data records -- each entry in this array corresponds to a point on
                // the chart.
                data: res,
                // The name of the data record attribute that contains x-values.
                xkey: 'year',
                // A list of names of data record attributes that contain y-values.
                ykeys: ['value'],
                // Labels for the ykeys -- will be displayed when you hover over the
                // chart.
                labels: ['Lượt truy cập']
            });
        },
        error: function (err) {
            bootbox.alert("Have a error !!! Please check again : "+err);
        }
    })
    //$.get('/Admin/Home/GetDataMorris', function (result) {

        
    //});
});