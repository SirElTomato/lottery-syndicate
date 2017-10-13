


$(document).ready(function () {

    var url = "https://localhost:44327/api/webapi";


    var options = {
        chart: {
            renderTo: 'container',
            type: 'spline'
        },
        series: [{}]
    };

    $.getJSON(url, function(data) {
        options.series[0].data = data;
        var chart = new Highcharts.Chart(options);
    });

});