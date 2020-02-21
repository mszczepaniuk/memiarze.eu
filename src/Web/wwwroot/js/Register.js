$(document).ready(function () {
    $("#registerForm").submit(function () {
        $("#passwordValidationInfo").html("");
        var errorHtml = "";
        var isValid = true;

        if ($("#passwordInput").val().length < 6) {
            errorHtml += "Hasło jest za krótkie. <br/>";
            isValid = false;
        }
        if ($("#passwordInput").val().match(/[a-z]/) === null) {
            errorHtml += "Hasło powinno posiadać przynajmniej jedną małą literę. <br/>";
            isValid = false;
        }
        if ($("#passwordInput").val().match(/[0-9]/) === null){
            errorHtml += "Hasło powinno posiadać przynajmniej jedną cyfrę. <br/>";
            isValid = false;
        }

        $("#passwordValidationInfo").html(errorHtml);
        return isValid;
    });
});