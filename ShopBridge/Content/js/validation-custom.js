
$(document).ready(function () {
   
    //This condition is used to check whether to validate hidden fields or not.
    if ($(".form-validate").length > 0) {
        if (!$(".form-validate").hasClass('ignore-hidden')) {
            $.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" })
        }
        else {
            $.validator.setDefaults({ ignore: [] })
        }
    }

    // Form Validation
    $(".form-validate").validate({
        rules: {
            required: {
                required: true
            },
            mobile: {
                required: true,
                minlength: 9,
                maxlength: 10,
                number: true
            },
            number: {
                required: true,
                number: true
            },
            email: {
                required: true,
                email: true
            },
            date: {
                required: true,
                date: true
            },
            url: {
                required: true,
                url: true
            },
            zip: {
                required: true,
                digits: true,
                minlength: 5,
                maxlength: 5
            },
            digit: {
                digits: true
            },
            uniqueValueonly: {
                uniqueValue: true
            },
            uniqueValueWhileEdit: {
                uniqueValueWhileEdit: true
            },
            uniqueValueWithParent: {
                uniqueValueWithParent: true
            },
            uniqueValueWithParentWhileEdit: {
                uniqueValueWithParentWhileEdit: true
            }
        },
        errorClass: "help-inline text-danger",
        errorElement: "span",
        highlight: function (element, errorClass, validClass) {
            $(element).parents('.form-group').addClass('has-error');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents('.form-group').removeClass('has-error');
            $(element).parents('.form-group').addClass('has-success');
        },
        invalidHandler: function (e, validator) {
            //validator.errorList contains an array of objects, where each object has properties "element" and "message".  element is the actual HTML Input.
            for (var i = 0; i < validator.errorList.length; i++) {
                console.log(validator.errorList[i]);
            }

            //validator.errorMap is an object mapping input names -> error messages
            for (var i in validator.errorMap) {
                console.log(i, ":", validator.errorMap[i]);
            }
        }
    });


    // SignUp Form Validation
    $("#signup_form").validate({
        rules: {
            required: {
                required: true
            },
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 6
            },
            confirmPassword: {
                required: true,
                minlength: 6,
                equalTo: "#password"
            },
            url: {
                required: true,
                url: true
            },
            username: {
                required: true,
                minlength: 5,
                maxlength: 16
            },
            ddlitemreq:
           {
              required: true
           }
        },
        errorClass: "help-inline text-danger",
        errorElement: "span",
        highlight: function (element, errorClass, validClass) {
            $(element).parents('.form-group').addClass('has-error');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).parents('.form-group').removeClass('has-error');
            $(element).parents('.form-group').addClass('has-success');
        }
    });


});