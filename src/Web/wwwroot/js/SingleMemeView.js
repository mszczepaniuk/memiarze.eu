$(document).ready(function () {
    $("#commentForm").submit(function () {
        $("#validationError").text("");
        if (!$("#commentInput").val()) {
            $("#validationError").text("Komentarz nie może byc pusty");
            return false;
        }
        if ($("#commentInput").val().length > 300) {
            $("#validationError").text("Maksymalna ilość znaków to 300");
            return false;
        }
    });
});