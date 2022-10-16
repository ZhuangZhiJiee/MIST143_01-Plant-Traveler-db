
showmodal = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#newstaticBackdrop .modal-body").html(res);
            $("#newstaticBackdrop .modal-title").html(title);
            $("#newstaticBackdrop").modal('show');
        }
    })
}