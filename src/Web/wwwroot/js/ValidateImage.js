//https://stackoverflow.com/a/44505315

function ValidateImage(file) {
    var FileSize = file.files[0].size / 1024;
    var fileType = file.files[0].name.split('.').pop().toLowerCase();
    var errorHtml = "";
    var imageTypes = ["jpg","jpeg","png","bmp"];
    if (FileSize > 500) {
        errorHtml += "Plik powinien być mniejszy niż 500 kB. <br/>";
        $(file).val('');
    }
    if (!imageTypes.includes(fileType)) {
        errorHtml += "Plik powinien być typu jpg, jpeg, png albo bmp. <br/>";
        $(file).val('');
    }
    $("#imageValidationInfo").html(errorHtml);
}