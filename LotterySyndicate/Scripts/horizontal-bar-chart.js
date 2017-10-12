function horizontalBarChart2() {

    var url = "https://localhost:44327/api/webapi";

    var margin = { top: 20, right: 20, bottom: 70, left: 40 },
        width = 540 - margin.left - margin.right,
        height = 270 - margin.top - margin.bottom;

    var svg = d3.select(".horizontalBarChart").append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform",
        "translate(" + margin.left + "," + margin.top + ")");

    //var svg = d3.select("svg"),
    //    margin = { top: 20, right: 20, bottom: 30, left: 80 },
    //    width = +svg.attr("width") - margin.left - margin.right,
    //    height = +svg.attr("height") - margin.top - margin.bottom;

    var tooltip = d3.select("body").append("div").attr("class", "toolTip");

    var x = d3.scaleLinear().range([0, width]);
    var y = d3.scaleBand().range([height, 0]);

    var g = svg.append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

    d3.json(url, function (error, data) {
        if (error) throw error;

        data.sort(function (a, b) { return a.AmountCount - b.AmountCount; });

        x.domain([0, d3.max(data, function (d) { return d.AmountCount; })]);
        y.domain(data.map(function (d) { return d.User; })).padding(0.1);

        g.append("g")
            .attr("class", "x axis")
            .attr("transform", "translate(0," + height + ")")
            .call(d3.axisBottom(x).ticks(5).tickFormat(function (d) { return parseInt(d / 1000); }).tickSizeInner([-height]));

        g.append("g")
            .attr("class", "y axis")
            .call(d3.axisLeft(y));

        g.selectAll(".bar")
            .data(data)
            .enter().append("rect")
            .attr("class", "bar")
            .attr("x", 0)
            .attr("height", y.bandwidth())
            .attr("y", function (d) { return y(d.User); })
            .attr("width", function (d) { return x(d.AmountCount); })
            .on("mousemove", function (d) {
                tooltip
                    .style("left", d3.event.pageX - 50 + "px")
                    .style("top", d3.event.pageY - 70 + "px")
                    .style("display", "inline-block")
                    .html((d.User) + "<br>" + "£" + (d.AmountCount));
            })
            .on("mouseout", function (d) { tooltip.style("display", "none"); });
    });
}