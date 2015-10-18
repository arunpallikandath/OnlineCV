/*
* Javascript functions for Admin related pages
*/

/*
* Validate the profile photo by its permitted extensions
*
* -------------------------------------------------------------------------------
* Description       Author      Date           Comments
 * -------------------------------------------------------------------------------
* Created           Arun        17-Oct-2015    
* -------------------------------------------------------------------------------
*/
$(document).ready(function () {
    $('#filePhoto').change(function (e) {
        var ext = this.value.match(/\.(.+)$/)[1];
        switch (ext) {
            case 'jpg':
            case 'bmp':
            case 'png':
            case 'tif':
                break;
            default:
                alert('not allowed');
                this.value = '';
        }
    });
});
