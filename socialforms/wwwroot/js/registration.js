/*alert("registration wurde eingebunden")*/

$(document).ready(() => {
    $("#Username").blur(() => {
        let name = $("#Username").val();
        $.ajax({
            url: "/user/checkUsername",
            method: "GET",
            data: { username: $("#Username").val() }
        })
            .done((dataFromServer) => {
                if (dataFromServer == true) {
                    $("#nameMessage").css("visibility", "visible");
                    $("#Username").addClass("redBorder");
                } else {
                    $("#nameMessage").css("visibility", "hidden");
                    $("#Username").removeClass("redBorder");
                }
            })
            .fail(() => {
                alert("Server/URL nicht erreichbar")
            });
    });

});