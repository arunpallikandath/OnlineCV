
/*
    Javascript for home controller releated pages
*/

/*
* Call ajax function when contact form is submitted
*
* -------------------------------------------------------------------------------
* Description       Author      Date           Comments
 * -------------------------------------------------------------------------------
* Created           Arun        17-Oct-2015    
* -------------------------------------------------------------------------------
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