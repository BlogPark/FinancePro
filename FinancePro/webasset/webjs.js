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

function cashvaluechange() {
    var basenum = $("#basenum").val();//����
    var minnum = $("#mincashnum").val();//��С
    var maxnum = $("#maxnum").val();//���
    var feenum = $("#feenum").val();//������
    var memberpoints = $("#memberpoints").html();//ʣ�����
    var cashnum = $("#cashorder_CashNum").val();//���ֽ��
    if (cashnum % basenum > 0)
    {
        alert("���ֽ���ȷ");
        return;
    }
    if (cashnum < minnum)
    {
        alert("���ֽ��С��ƽ̨�޶�ֵ");
        return;
    }
    var max = memberpoints * feenum / 100;
    if (cashnum > max)
    {
        return "���ֽ���ƽ̨�޶�ֵ";
        return;
    }
    var fee = cashnum * feenum / 100;
    $("#cashorder_CashNum").val(fee);
}