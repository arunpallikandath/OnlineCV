
/*
    Javascript for controller specific functionalities
*/

$(document).ready(function () {
    
    $("#frmContact").submit(function () {
        $("#btnSendContact").attr("disabled", true);
        var action = $('#frmContact').attr("action");
        var serializedForm = $('#frmContact').serialize();
        $.ajax({
            type: 'POST',
            url: action,
            data: serializedForm,
            success: function (data, textStatus, request) {
                if (!data == "") {
                    // redisplay partial view
                    $('#frmContact').trigger("reset");
                    $("#messages").text(data);
                    $("#btnSendContact").removeAttr("disabled");
                    $("#msgPanel").removeAttr("hidden");
                }
                else {
                    $("#messages").text(data);
                    $("#btnSendContact").removeAttr("disabled");
                    $("#msgPanel").removeAttr("hidden");
                }
            }
        });

        return false;
    });

});