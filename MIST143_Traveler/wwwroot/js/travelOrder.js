$(function () {

    $('#adultPlus').click(function () {
        let currentNum = parseInt($('#adultNum').text())
        currentNum += 1
        $('#adultNum').text(currentNum)

        $('#totalPrice').text(currentNum * 300)
    });
    $('#adultMinus').click(function () {
        let currentNum = parseInt($('#adultNum').text())
        if(currentNum > 0) {
            currentNum -= 1
            $('#adultNum').text(currentNum)
        }
        $('#totalPrice').text(currentNum * 300)
        console.log($('#adultNum').text())
    });
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

    //creditCardBlock
});