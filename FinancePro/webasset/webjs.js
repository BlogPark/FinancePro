$(function () {
});
function valuechang() {
    var transfernum = $("#transfernum").val();
    var feetype = $("#feetype").val();
    var typenum = $("#transfer_TransferType").val();
    var feenum = 0;
    var finalnum = 0;
    if (typenum == feetype) {
        feenum = transfernum * 0.3;
        finalnum = transfernum - feenum;
    }
    else {
        feenum = transfernum * 0;
        finalnum = transfernum - feenum;
    }
    $("#transfer_CounterFee").val(feenum);
    $("#transfer_TransferNumber").val(finalnum);
}