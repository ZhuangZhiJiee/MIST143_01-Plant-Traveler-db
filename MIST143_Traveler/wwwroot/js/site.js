
LOGIN = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#LOGIN .modal-body").html(res);
            $("#LOGIN .modal-title").html(title);
            $("#LOGIN").modal('show');
        }
    })
}
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