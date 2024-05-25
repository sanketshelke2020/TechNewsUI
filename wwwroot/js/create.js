$(document).ready(function () {
    let dict = {};
    if ($('#CategoryType').val() != null) {
        var e = $('#CategoryType');
        valChange(e);
    }
    var ddlArticleCategory = $('#ArticleCategoryInputId');
    $.ajax({
        url: '/Admin/GetAllArticleCategories',
        headers: { 'Access-Control-Allow-Origin': '*' },
        type: 'POST',
        dataType: 'json',

        success: function (d) {
            ddlArticleCategory.empty();
            ddlArticleCategory.append($("<option></option>").val("").html("Select Article Category"));

            $.each(d, function (i, articleCategory) {
                ddlArticleCategory.append($("<option></option>").val(articleCategory.articleCategoryId).html(articleCategory.articleCategoryName));
            });
        },
        error: function (e) {
            console.log("Error")
            console.log(e);
        }
    });
    //Image Validation
    $("#Image").change(function () {
        var imageExt = ["jpg", "JPG", "png", "PNG", "JPEG", "jpeg"];
        var filType = document.getElementById("Image").value;
        var ext = filType.substring(filType.lastIndexOf(".") + 1, filType.length);

        if (filType != "" || filType != Undefined) {
            if ($.inArray(ext, imageExt) == -1) {
                $("#Image").val("");
                $("#ImageError").text("Only png, jpg and jpeg Files are allowed.");
            }
            else {
                $("#ImageError").text("");
            }
        }
        else {
            $("#ImageError").text("");
        }
    })
    //File Validation
    function checkFileType(...extensions) {
        var filType = document.getElementById("StoredFile").value;
        var ext = filType.substring(filType.lastIndexOf(".") + 1, filType.length);

        if (filType != "" || filType != Undefined) {
            if ($.inArray(ext, extensions) == -1) {
                $("#StoredFile").val("");
                $("#StoredFileError").text("Only " + extensions.join(", ") + " Files are allowed.");
            }
            else {
                $("#StoredFileError").text("");
            }
        }
        else {
            $("#StoredFileError").text("");
        }
    }


    function checkEndDate() {
        var dateString = document.getElementById('EndDateInput').value;
        var myDate = new Date(dateString);
        var dateString1 = document.getElementById('StartDateInput').value;
        var startDate = new Date(dateString1);
        if (myDate == null) {

        }
        else if (myDate < startDate) {
            $('#EndDateError').text("End Date should be greater than Start date");
            $('#EndDateInput').val("");
        }
        else {
            $('#EndDateError').text("");
        }
    }
    //Evnet Date
    $("#EventDateInput").change(function () {
        var dateString = document.getElementById('EventDateInput').value;
        var myDate = new Date(dateString);
        var today = new Date();
        if (myDate < today) {
            $('#EventDateError').text("Event Date and Time should be greater than Current date and time");
            $('#EventDateInput').val("");
        }
        else {
            $('#EventDateError').text("");
        }
    });
    //datetime greater than today
    $("#StartDateInput").change(function () {
        var dateString = document.getElementById('StartDateInput').value;
        var myDate = new Date(dateString);
        var today = new Date();
        checkEndDate();
        if (myDate < today) {
            $('#StartDateError').text("Start Date should be greater than today date");
            $('#StartDateInput').val("");
        }
        else {
            $('#StartDateError').text("");
        }
    });
    $("#EndDateInput").change(function () {
        checkEndDate();
    });

    $('#CategoryType').change(function () {
        console.log("helle");
        var e = $(this);
        valChange(e);
    });

    function valChange(e) {
        $(".change").attr("hidden", true);
        if (e.val() === "Youtube") {
            $('#YoutubeVideoLink').removeAttr('hidden');
        }
        else if (e.val() == "CaseStudies") {
            $('#NumberOfChapter').removeAttr('hidden');
            $('#File').removeAttr('hidden');
            $("#StoredFile").change(function () {
                checkFileType("pdf");
            })

        }
        else if (e.val() == "Article") {
            $('#ArticleCategory').removeAttr('hidden');
            $('#ArticleSubCategory').removeAttr('hidden');
            $('#CountryId').removeAttr('hidden');
            $('#YoutubeVideoLink').removeAttr('hidden');
            $('#File').removeAttr('hidden');
            $("#StoredFile").change(function () {
                checkFileType("jpg", "jpeg", "png");
            })
            $('#articleRadio').removeAttr('hidden');
            radioHideShowFileds();
            $('input[name="flexRadioDefault"]').change(function () {
                radioHideShowFileds();
            });
        }
        else if (e.val() == "Webinar") {
            $('#StartDate').removeAttr('hidden');
            $('#EndDate').removeAttr('hidden');
            $('#SpeakerName').removeAttr('hidden');
            $('#YoutubeVideoLink').removeAttr('hidden');
            $('#Name').removeAttr('hidden');
            $('#Designation').removeAttr('hidden');
            $('#ContactNumber').removeAttr('hidden');
            $('#Email').removeAttr('hidden');
            $('#TotalSeat').removeAttr('hidden');
            //$('#File').removeAttr('hidden');
            $('#EventTopic').removeAttr('hidden');

            //$("#StoredFile").change(function () {
            //    checkFileType("mp4");
            //})
        }
        else if (e.val() == "Podcast") {
            $('#SpeakerName').removeAttr('hidden');
        }
        else if (e.val() == "EventSchedule") {
            $('#SpeakerName').removeAttr('hidden');
            $('#EventTopic').removeAttr('hidden');
            $('#EventDate').removeAttr('hidden');
            //$('#File').removeAttr('hidden');
            $('#YoutubeVideoLink').removeAttr('hidden');
            //$("#StoredFile").change(function () {
            //    checkFileType("mp4");
            //})
        }
        else if (e.val() == "Magazine") {
            $('#NumberOfPages').removeAttr('hidden');
            $('#File').removeAttr('hidden');
            $("#StoredFile").change(function () {
                checkFileType("pdf");
            })
        }
        else if (e.val() == "LiveInterview") {
            $('#YoutubeVideoLink').removeAttr('hidden');
        }

    }
    if ($('#CategoryType').val() != null) {
        var e = $('#CategoryType');
        valChange(e);
    }


    //Article

    $("#ArticleCategoryInputId").change(function () {
        var articleCategoryId = parseInt($(this).val());

        if (!isNaN(articleCategoryId)) {
            var ddlArticleSubCategory = $('#ArticleSubCategoryIdInput');
            ddlArticleSubCategory.empty();
            $.ajax({
                url: '/Admin/GetAllArticleSubCategories',
                type: 'POST',
                dataType: 'json',
                data: { articleCategoryId: articleCategoryId },
                success: function (d) {
                    ddlArticleSubCategory.empty();
                    ddlArticleSubCategory.append($("<option></option>").val("").html("--Article SubCategories--"));
                    $.each(d, function (i, articleSubCategories) {
                        ddlArticleSubCategory.append(
                            $("<option></option>")
                                .val(articleSubCategories.articleSubCategoryId)
                                .html(articleSubCategories.articleSubCategoryName)
                        );
                    });

                },
                error: function () {
                    alert('Error!');
                    console.log(f);
                }
            });
        }

    });
    var ddlCountry = $('#CountryIdInput');
    $.ajax({
        url: '/Admin/GetAllCountries',
        headers: { 'Access-Control-Allow-Origin': '*' },
        type: 'POST',
        dataType: 'json',
        //data: {countryId: countryId },
        success: function (d) {
            ddlCountry.empty();
            ddlCountry.append($("<option></option>").val("").html("--Country--"));

            $.each(d, function (i, country) {
                ddlCountry.append($("<option></option>").val(country.countryId).html(country.countryName));
            });
        },
        error: function (e) {
            console.log("Error")
            console.log(e);
        }
    });
    function radioHideShowFileds() {
        if ($('#flexRadioDefault1').is(':checked')) {
            $("#YoutubeVideoLink").css('display', 'none');
            $("#YoutubeInput").val("");
            $("#File").css('display', 'block');
        }
        else {
            $("#YoutubeVideoLink").css('display', 'block');
            $("#File").css('display', 'none');

        }
    }


    $("#CategoryType").change(function () {
        console.log("Inside function");
        console.log($('#CategoryType').val());
        if ($('#CategoryType').val() != "empty") {

            $.ajax({
                url: '/Admin/GetDynamicFieldByCategory',
                type: 'POST',
                dataType: 'json',
                data: { category: $('#CategoryType').val() },
                success: function (d) {
                    if (d.length > 0) {
                        let dynamicFields = '';
                        $.each(d, function (i, item) {
                            switch (item.fieldType) {
                                case "TextBox":
                                    dynamicFields += AppendtextBox(item);
                                    break;
                                case "DropDown":
                                    dynamicFields += AppendDropDown(item);
                                    break;
                                case "RadioButton":
                                    dynamicFields += AppendRadioButton(item);
                                    break;
                                case "CheckBox":
                                    dynamicFields += AppendCheckBox(item);
                                    break;
                                case "Files":
                                    dynamicFields += AppendFile(item);
                                    break;
                                case "TextArea":
                                    dynamicFields += AppendTextArea(item);
                                    break;
                                default:
                                    dynamicFields += '';
                                    break;
                            }
                        });
                        $('#DynamicFields').html(dynamicFields);
                        var elements = document.getElementsByClassName("dynamicData");
                        for (var i = 0; i < elements.length; i++) {
                            elements[i].addEventListener('change', myFunction, false);

                        }
                    }

                },
                error: function () {
                    alert('Error!');

                }
            });
        } else
        {
            $('#DynamicFields').html("");
        }
        

    });

    function AppendtextBox(itemInput) {
        console.log("AppendtextBox")
        let field = ` <div>
                                    <label class="control-label">${itemInput.labelText}</label>
                                    <input class="form-control  dynamicData" required type="${itemInput.isNumber ? "number" : "text"}"  data-fieldCode="${itemInput.fieldCode}" placeholder="${itemInput.placeHolder}"   }" />
                                    <span  class="text-danger"></span>
                                </div>`
        return field;
    }
    function AppendTextArea(itemTextArea) {
        console.log("AppendtextBox")
        let field = ` <div>
                                    <label class="control-label my-2">${itemTextArea.labelText}</label>
                                     <textarea id="w3review" required name="w3review" rows="4" cols="56" data-fieldCode="${itemTextArea.fieldCode}"></textarea>                                    
                                     <span  class="text-danger"></span>
                                </div>`
        return field;
    }
    function AppendFile(itemfile) {
        console.log("AppendFile")
        let field = ` <div>
                                    <label class="control-label my-2">${itemfile.labelText}</label>
                                    <input class="form-control  dynamicData" required type="file"  data-fieldCode="${itemfile.fieldCode}" data-filetype = "File" }" />
                                    <span  class="text-danger"></span>
                                </div>`
        return field;
    }
    function AppendDropDown(itemdropdown) {
        console.log("AppendDropDown")
        let field = `<div>
                <label  class="control-label my-2">${itemdropdown.labelText}</label>
                <select class="form-control dynamicData" data-fieldCode="${itemdropdown.fieldCode} " required>
                    <option value="">Select Option</option>`;
        $.each(itemdropdown.options.split(','), function (i, option) {
            field += `<option>${option}</option>`;
        });
        field += `</select>
                <span  class="text-danger"></span>
              </div>`;
        return field;

    }
    function AppendRadioButton(itemRadio) {
        console.log("AppendRadioButton");
        let field = ` <div class="my-2"> ${itemRadio.labelText}: <br>`;
        $.each(itemRadio.options.split(','), function (i, option) {
            field += ` <input type="radio" id="html" name="fav_language"  required data-fieldCode="${itemRadio.fieldCode}"  class="dynamicData" value="${option}">
                       <label for="html">${option}</label><br>`;
        });
        field += `</div>`;
        return field;
    }
    function AppendCheckBox(Checkboxitem) {
        console.log("AppendCheckBox");
        let field = `<div class="my-2">  ${Checkboxitem.labelText}: <br>`;
        $.each(Checkboxitem.options.split(','), function (i, option) {
            field += ` <input type="checkbox" required  value="${option}" data-fieldCode="${Checkboxitem.fieldCode}" class="dynamicData" />
                <label>${option}</label> <br>`;
        });
        field += `</div>`;
        return field;


    }
    var myFunction = async function () {
        let inputtype = this.getAttribute("data-filetype");
        if (inputtype == "File")
        {
           
                     var files =  this.files[0];
                     let attribute = this.getAttribute("data-fieldCode");
                    var Filebyte = [];
                    const reader = new FileReader();
                    let fileName = files.name;
                    console.log("file");
                    console.log(files);
                    //reader.onload = event => dict[`${attribute}`] = { fileData: event.target.result, fileExt : fileName };
                    Filebyte = await getAsByteArray(files);
                    var fileString = await bin2String(Filebyte);
                    dict[`${attribute}`] = { fileData: fileString, fileExt: fileName };
                    
                    $('#dynamicDatahidden').val(JSON.stringify(dict));
                   console.log(JSON.stringify(dict));
                
        }
        else
        {
            let attribute = this.getAttribute("data-fieldCode");
            let value = this.value;
            dict[`${attribute}`] = value;
            $('#dynamicDatahidden').val(JSON.stringify(dict));
            console.log(JSON.stringify(dict));
        }

    };
    async function getAsByteArray(file) {
        var fileByteArray = [];
        array = new Uint8Array(await readFile(file))
        for (var i = 0; i < array.length; i++) {
            fileByteArray.push(array[i]);
        }
        return fileByteArray;
    }
    function readFile(file) {
        return new Promise((resolve, reject) => {
            // Create file reader
            let reader = new FileReader()

            // Register event listeners
            reader.addEventListener("loadend", e => resolve(e.target.result))
            reader.addEventListener("error", reject)

            // Read file
            reader.readAsArrayBuffer(file)
        })
    }
    async function bin2String(array) {
        var result = "";
        for (var i = 0; i < array.length; i++) {
            result += String.fromCharCode(parseInt(array[i], 2));
        }
        return result;
    }

});

