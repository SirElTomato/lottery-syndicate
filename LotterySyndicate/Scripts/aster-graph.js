
function asterGraph()
{
    var url = "https://localhost:44327/api/webapi";

    var width = 400,
        height = 400,
        radius = Math.min(width, height) / 2,
        innerRadius = 0.3 * radius;

    var pie = d3.layout.pie()
        .sort(null)
        .value(function (d) { return d.width; });

    var tip = d3.tip()
        .attr('class', 'd3-tip')
        .offset([0, 0])
        .html(function (d) {
            return d.data.User + ": <span style='color:orangered'>" + d.data.AmountCount + "</span>";
        });

    var arc = d3.svg.arc()
        .innerRadius(innerRadius)
        .outerRadius(function (d) {
            return (radius - innerRadius) * (d.data.AmountCount / 100.0) + innerRadius;
        });

    var outlineArc = d3.svg.arc()
        .innerRadius(innerRadius)
        .outerRadius(radius);

    var svg = d3.select(".asterGraph").append("svg")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

    svg.call(tip);

    
    var total = 0;
    var jsonCount = 0;
    d3.json(url, function (error, data) {
        console.log(data);

        data.forEach(function (d) {
            var color = '#' + (Math.random() * 0xFFFFFF << 0).toString(16);
            //d.id = d.id;
            //d.order = +d.order;
            d.color = color;
            //d.weight = +d.weight;
            d.AmountCount = +d.AmountCount;
            //d.width = +d.weight;
            d.width = 10;
            d.User = d.User;
            total = total + d.AmountCount;
            jsonCount = jsonCount + 1;
        });
        // for (var i = 0; i < data.AmountCount; i++) { console.log(data[i].id) }

        var path = svg.selectAll(".solidArc")
            .data(pie(data))
            .enter().append("path")
            .attr("fill", function (d) { return d.data.color; })
            //.attr("fill", "blue")
            .attr("class", "solidArc")
            .attr("stroke", "gray")
            .attr("d", arc)
            .on('mouseover', tip.show)
            .on('mouseout', tip.hide);

        var outerPath = svg.selectAll(".outlineArc")
            .data(pie(data))
            .enter().append("path")
            .attr("fill", "none")
            .attr("stroke", "gray")
            .attr("class", "outlineArc")
            .attr("d", outlineArc);


        // calculate the mean AmountCount
        var asterScore = total / jsonCount;

        svg.append("svg:text")
            .attr("class", "aster-score")
            .attr("dy", ".35em")
            .attr("text-anchor", "middle") // text-align: right
            .text(Math.round(asterScore));

    });
}
