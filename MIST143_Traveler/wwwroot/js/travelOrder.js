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

    // ��������
    $('#modalCheck').click(function () {
        $(location).attr('href', "/");
    });

    //�H�Υd�ʵe - �I��X�{ �I���L�I�ڤ覡 �h����
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