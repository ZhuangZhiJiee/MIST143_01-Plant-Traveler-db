
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

OrderCancel = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#exampleModal .modal-body").html(res);
            $("#exampleModal .modal-title").html(title);
            $("#exampleModal").modal('show');
        }
    })
}
Odd = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#ORD .modal-body").html(res);
            $("#ORD .modal-title").html(title);
            $("#ORD").modal('show');
        }
    })
}