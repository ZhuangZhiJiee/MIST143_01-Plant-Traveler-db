$(function () {


    $('#childPlus').click(function () {
        let currentNum = parseInt($('#childNum').text())
        currentNum += 1
        $('#childNum').text(currentNum)
        let total = currentNum * 300
        $('#shoppingCartTotalPrice').text(total)
        $('#totalPayPrice').text(total)
        
    });
    $('#childMinus').click(function () {
        let currentNum = parseInt($('#childNum').text())
        if(currentNum > 0) {
            currentNum -= 1
            $('#childNum').text(currentNum)
        }
        let total = currentNum * 300
        $('#shoppingCartTotalPrice').text(total)
        $('#totalPayPrice').text(total)
    });
    // $('.goPayBtn').

    // 頁面跳轉
    $('#modalCheck').click(function () {
        $(location).attr('href', "/");
    });

    //信用卡動畫 - 點到出現 點到其他付款方式 則消失
    $('#creditCardRadio').click(function () {
        $('#creditCardBlock').show()
    });
    $('#flexRadioDefault1').click(function () {
        $('#creditCardBlock').hide()
    });
    $('#flexRadioDefault2').click(function () {
        $('#creditCardBlock').hide()
    });
    //點擊確認付款
    $('#clickPayBtn').click(function () {
        let payType = $('input[name=flexRadioDefault]:checked', "#payType").val()
        console.log("payType", payType)
        $("input[name=PaymethodId]").val(payType)
        console.log("dsfasdfasdf", $("input[name=PaymethodId]").val())
    })

    //creditCardBlock
});