
// Write your JavaScript code.
$(function () {
    $('a[href="#search"]').on('click', function (event) {
        event.preventDefault();
        $('#Footerfixed').removeClass('fixed-bottom');
        /*console.log($("##Footerfixed"))*/
        $('#search').addClass('open');
        $('#search > form > input[type="search"]').focus();
       
    });

    $('#search, #search button.close').on('click keyup', function (event) {
        if (event.target == this || event.target.className == 'close' || event.keyCode == 27) {
            $(this).removeClass('open');
        }
    });


   
});

$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})

//notification Count
$().ready(function () {
    $.ajax({
        url: '/HomePage/GetNotifications?i=count',
        headers: { 'Access-Control-Allow-Origin': '*' },
        type: 'get',
        dataType: 'json',
        success: function (count) {
            console.log(count);
            if (count >= 1) {
                $(".badge").removeAttr('hidden');
            }
            else {
                $(".badge").attr("hidden", true);
            }
        }
    })
})

//notification api call
$('#NotificationBtn').on('click', function () {
    $.ajax({
        url: '/HomePage/GetNotifications?i=hello',
        headers: { 'Access-Control-Allow-Origin': '*' },
        type: 'get',
        dataType: 'json',

        success: function (d) {

            $('.notification-container').html("");
            d.forEach(function (item) {
                let actionName = "";
                let controllerName = "";
                let category = item.categoryType;
                switch (category) {
                    case "@nameof(CategoryEnum.Article)":
                        actionName = "ArticleDetail";
                        controllerName = "Article";
                        break;
                    case "@nameof(CategoryEnum.Youtube)":
                        actionName = "YoutubeDetail";
                        controllerName = "Youtube";
                        break;
                    case "@nameof(CategoryEnum.Podcast)":
                        actionName = "GetPodcastById";
                        controllerName = "Podcast";
                        break;
                    case "@nameof(CategoryEnum.CaseStudies)":
                        actionName = "CaseStudiesDetail";
                        controllerName = "CaseStudie";
                        break;
                    case "@nameof(CategoryEnum.LiveInterview)":
                        actionName = "GetLiveInteviewById";
                        controllerName = "LiveInterview";
                        break;
                    case "@nameof(CategoryEnum.Magazine)":
                        actionName = "GetMagazineById";
                        controllerName = "Magazine";
                        break;
                    case "@nameof(CategoryEnum.EventSchedule)":
                        actionName = "GetEventById";
                        controllerName = "Event";
                        break;
                    case "@nameof(CategoryEnum.Webinar)":
                        actionName = "WebinarDetail";
                        controllerName = "Webinar";
                        break;
                    default:
                        break;
                }
                var url = "/" + controllerName + "/" + actionName + "?SectionMasterId=" + item.sectionMasterId;
                var date = timeSince(new Date(item.createdDate)) + " ago";
                $('.notification-container').append(`
                                                                                        <div class="row my-3 notification-row" onclick="location.href='${url}'">
                                                                                            <div class="col-3"><img class="notificationImg" src = "data:image/png;base64,${item.image}"></div>
                                                                                    <div class="col-9"><b>${item.title}</b> <p class="notification-shortDes mt-1"> ${item.shortDescription}</p>
                                                                                        <div class='notification-info d-flex justify-content-between'>
                                                                                            <h6></h6>
                                                                                                    <h6 class="dateConvert">${date}</h6>
                                                                                        </div>
                                                                                    </div>
                                                                        </div>


                                                                    `)
            })
        },
        error: function (e) {
            console.log("Error")
            console.log(e);
        }
    });
})


$('#NewsletterForm').click(function (e) {
    e.preventDefault();
    var data1 = $("#NewssletterMail").val();
    if (data1 == null) {
        $("#msg").html("This Field is Required");
    }
    else {
        $.ajax({
            type: "POST",
            url: "/HomePage/AddNewsLetter",
            data: { email: $('#NewssletterMail').val(), access_token: $("#access_token").val() },
            success: function (result) {
                if (result == true) {
                    $("#NewssletterMail").val("");
                    const Toast = Swal.mixin({
                        toast: true,
                        position: 'top-end',
                        showConfirmButton: false,
                        timer: 3000,
                        timerProgressBar: true,
                        didOpen: (toast) => {
                            toast.addEventListener('mouseenter', Swal.stopTimer)
                            toast.addEventListener('mouseleave', Swal.resumeTimer)
                        }
                    })

                    Toast.fire({
                        icon: 'success',
                        title: 'congratulations! You Will get Daily updates from TechNews'
                    })
                }
                else {
                    $("#NewssletterMail").val("");
                    $("#msg").html("Enter Valid Email");
                }

            },
            error: function (result) {
                $("#msg").html("Enter Valid Email");
            }


        });
    }
});
$(document).ready(function () {
    $('input').change(function () {
        $.ajax({
            type: "GET",
            url: "/HomePage/EmailsDoesNotExists",
            data: { email: $('#NewssletterMail').val(), access_token: $("#access_token").val() },
            success: function (result) {
                if (result == true) {
                    $("#NewssletterMail").val("");
                    $("#msg").html(" ");
                }

                else if (result == false) {
                    $("#NewssletterMail").val("");
                    $("#msg").html("Email Id Alreday Exist");

                }
                else {
                    $("#NewssletterMail").val("");
                    $("#msg").html("Enter Valid Email");
                }
            },
            error: function (result) {
                $("#msg").html("Enter Valid Email");
            }
        });
    });
});


