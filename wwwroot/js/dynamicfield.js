function showCorrectFields(value) {
    console.log({fieldTypeValue: value})

    $(".change").attr("hidden", true);

    if (value == "Text-Area") {
        console.log("inside if")
        $("#PlaceHolder").removeAttr("hidden");
        $("#MaxLength").removeAttr("hidden");
        $("#MinLength").removeAttr("hidden");
        $("#FieldCode").removeAttr("hidden");
    }
    else if (value == "TextBox") {

        $("#PlaceHolder").removeAttr("hidden");
        $("#MaxLength").removeAttr("hidden");
        $("#MinLength").removeAttr("hidden");
        $("#FieldCode").removeAttr("hidden");
    }
    else if (value == "RadioButton") {
        console.log("inside if")
        $("#Options").removeAttr("hidden");
        $("#FieldCode").removeAttr("hidden");

    }
    else if (value == "DropDown") {

        $("#Options").removeAttr("hidden");
        $("#FieldCode").removeAttr("hidden");

    }
    else if (value == "CheckBox") {

        $("#Options").removeAttr("hidden");
        $("#FieldCode").removeAttr("hidden");
    }
    else if (value == "Files") {

        $("#PlaceHolder").removeAttr("hidden");
        $("#FieldCode").removeAttr("hidden");
    }

}

console.log('test')
$(document).ready(function () {
    console.log("Jquery working")
    const fieldType = $("#FieldType");
    showCorrectFields(fieldType.val())
    $('#FieldType').change(function (e) {
        console.log({fieldType: e.target.value });
        showCorrectFields(fieldType.val())
    });

});