$("#enrollBtn").on("click", (event) => {
    const enrollBtn = $("#enrollBtn");
    const enrollement = {
        userId: enrollBtn.attr("data-UserId"),
        webinarId: enrollBtn.attr("data-WebinarId")
    }
    console.table(enrollement)
    $.ajax({
        url: '/Webinar/Enroll',
        headers: { 'Access-Control-Allow-Origin': '*' },
        data: enrollement,
        type: 'POST',
        dataType: 'json',
        success: function (d) {
            console.log(d)
            location.reload();

        },
        error: function (e) {
            console.log("Error")
            console.log(e);
        }
    });
});