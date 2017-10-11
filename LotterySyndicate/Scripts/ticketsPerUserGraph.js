﻿function start()
{
    d3.select("body")
        .append("p")
        .text("Locd text with d3.js");
    console.log(d3);
}

function svgExample() 
{
    var canvas = d3.select("body") //canvas is tranditionally what people call the area dedicated to d3 drawings
        .append("svg") //adding an svg element to the canvas
        .attr("width", 700)
        .attr("height", 700);

    var rectangle = canvas.append("rect")
        .attr("width", 100)
        .attr("height", 100);

    var circle = canvas.append("circle")
        .attr("cx", 100) //center x
        .attr("cy", 100) //center y
        .attr("r", 50) // radius
        .attr("fill", "blue"); //colour

    var line = canvas.append("line")
        .attr("x1", 0) //line start x
        .attr("x2", 200) //line end x
        .attr("y1", 100) //line start y
        .attr("y2", 300) //line end y
        .attr("stroke", "grey") //line colour
        .attr("stroke-width", 3); //line thickness

}

function visualiseOranges() 
{
    var orangeData = [10, 30, 50, 100];
    var orangeJson = [{ "AmountCount": 30 }, { "AmountCount": 80 }];

    var canvas = d3.select(".orangeContainer")
        .append("svg")
        .attr("width", 768)
        .attr("height", 1024);

    var oranges = canvas.selectAll("circle") //oranges is going to apply to every circle in the container
        .data(orangeJson)
        .enter()
        .append("circle")
        .attr("fill", "orange")
        .attr("cx", function (d, i) //d = data, i = index
        {
            return d.AmountCount + (i * 100);
        })
        .attr("cy", function (d)
        {
            return d.AmountCount;
        })
        .attr("r", function (d)
        {
            return d.AmountCount;
        });

}

function importData()
{
    var url = "https://localhost:44327/api/webapi";
    d3.json(url, function (data)
    {
        var canvas = d3.select(".dataContainer").append("svg")
            .attr("width", 400)
            .attr("height", 300)

        canvas.selectAll("rect")
            .data(data)
            .enter()
            .append("rect")
            .attr("width", function (d) {
                return d.AmountCount;
            })
            .attr("height", 50)
            .attr("y", function (d, i) {
                return i * 80
            })
            .attr("fill", "red");

        canvas.selectAll("text")
            .data(data)
            .enter()
            .append("text")
            .attr("fill", "blue")
            .attr("y", function (d, i) 
            {
                return i * 80 + 25; 
            })
            .attr("x", 5)
            .text(function (d)
            {
                return " User: " + d.User + " amount: " + d.AmountCount + " tickets " +  d.TicketCount;
             })
    
    })

}

function barChart() 
{
    var url = "https://localhost:44327/api/webapi";

    // set the dimensions of the canvas
    var margin = { top: 20, right: 20, bottom: 70, left: 40 },
        width = 600 - margin.left - margin.right,
        height = 300 - margin.top - margin.bottom;


    // set the ranges
    var x = d3.scale.ordinal().rangeRoundBands([0, width], .05);

    var y = d3.scale.linear().range([height, 0]);

    // define the axis
    var xAxis = d3.svg.axis()
        .scale(x)
        .orient("bottom")


    var yAxis = d3.svg.axis()
        .scale(y)
        .orient("left")
        .ticks(10);


    // add the SVG element
    var svg = d3.select(".ticketsPerUser").append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform",
        "translate(" + margin.left + "," + margin.top + ")");


    // load the data
    d3.json(url, function (error, data) {

        data.forEach(function (d) {
            d.User = d.User;
            d.AmountCount = +d.AmountCount;
        });

        // scale the range of the data
        x.domain(data.map(function (d) { return d.User; }));
        y.domain([0, d3.max(data, function (d) { return d.AmountCount; })]);

        // add axis
        svg.append("g")
            .attr("class", "x axis")
            .attr("transform", "translate(0," + height + ")")
            .call(xAxis)
            .selectAll("text")
            .style("text-anchor", "end")
            .attr("dx", "-.8em")
            .attr("dy", "-.55em")
            .attr("transform", "rotate(-90)");

        svg.append("g")
            .attr("class", "y axis")
            .call(yAxis)
            .append("text")
            .attr("transform", "rotate(-90)")
            .attr("y", 5)
            .attr("dy", ".71em")
            .style("text-anchor", "end")
            .text("AmountCountuency");


        // Add bar chart
        svg.selectAll("bar")
            .data(data)
            .enter().append("rect")
            .attr("class", "bar")
            .attr("x", function (d) { return x(d.User); })
            .attr("width", x.rangeBand())
            .attr("y", function (d) { return y(d.AmountCount); })
            .attr("height", function (d) { return height - y(d.AmountCount); });

    });

}

function countObjectKeys(obj) {
    return Object.keys(obj).length;
}

function asterGraph()
{
    var url = "https://localhost:44327/api/webapi";

    var width = 500,
        height = 500,
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