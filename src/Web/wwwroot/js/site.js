// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$("#test").click(function () {
    $.ajax({
        url: "https://localhost:44313/api/test",
        method: "POST"
    });
});