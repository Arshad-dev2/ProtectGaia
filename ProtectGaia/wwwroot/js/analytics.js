
var jsonActivity = JSON.parse($("#ActivityData").val());
var Activity_label = [];
var Activity_data = [];
console.log(jsonActivity);
for (x in jsonActivity) {
    Activity_label.push(x);
    Activity_data.push(jsonActivity[x])
}
var PieActivity = JSON.parse($("#CarbonActivityData").val());
console.log(PieActivity)
var Carbon_label = [];
var Carbon_Data = [];
for (x in PieActivity) {
    Carbon_label.push(x);
    Carbon_Data.push(PieActivity[x])
}


new Chart(document.getElementById("doughnut-chart"), {
    type: 'doughnut',
    data: {
        labels: Carbon_label,
        datasets: [
            {
                label: "CO2 Emissions",
                backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9"],
                data: Carbon_Data
            }
        ]
    },
    options: {
        title: {
            display: true,
            text: 'Carbon Emissions Saved till Now'
        },
        responsive: true
        
    }
});


new Chart(document.getElementById("line-chart"), {
    type: 'line',
    data: {
        labels: Activity_label,
        datasets: [{
            data: Activity_data,
            label: "Overall Score",
            borderColor: "#3e95cd",
            fill: false
        }]
    },
    options: {
        title: {
            display: true,
            text: 'Your Current Progress on Saving Planet'
        },
        responsive: true,
        maintainAspectRatio: false
    }
});