$(function () {
    $("#valiCode").bind("click", function () {
        this.src = "/WebFormArea/Login/GetImg?time=" + (new Date()).getTime();
    });
    $("#redrpprovince").change(function () {
        var provincename = $("#redrpprovince").find("option:selected").text();
        var provinceid = $("#redrpprovince").val();
        webchang(provinceid, 'redrpcity');
        $("#member_MemberProvince").val(provincename);
    });
    $("#redrpcity").change(function () {
        var cityname = $("#redrpcity").find("option:selected").text();
        var cityid = $("#redrpcity").val();
        webchang(cityid, 'redrparea');
        $("#member_MemberCity").val(cityname);
    });
    $("#redrparea").change(function () {
        var areaname = $("#redrparea").find("option:selected").text();
        var areaid = $("#redrparea").val();
        $("#member_MemberArea").val(areaname);
    });
    var raval = $("input:radio[name=reagerRadios1]:checked").val();
    $("#member_MemberSex").val(raval);
    $("input:radio[name=reagerRadios1]").change(function () {
        var raval = $("input:radio[name=reagerRadios1]:checked").val();
        $("#member_MemberSex").val(raval);
    });
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
    var basenum = $("#basenum").val();//基数
    var minnum = $("#mincashnum").val();//最小
    var maxnum = $("#maxnum").val();//最大
    var feenum = $("#feenum").val();//手续费
    var memberpoints = $("#memberpoints").html();//剩余积分
    var cashnum = $("#cashorder_CashNum").val();//提现金额
    if (cashnum % basenum > 0) {
        alert("提现金额不正确");
        return;
    }
    if (cashnum < minnum) {
        alert("提现金额小于平台限定值");
        return;
    }
    var max = memberpoints * feenum / 100;
    if (cashnum > max) {
        return "提现金额超出平台限定值";
        return;
    }
    var fee = cashnum * feenum / 100;
    $("#cashorder_CashNum").val(fee);
}
function webchang(id, control) {
    $.ajax({
        url: '/public/obtainreagin',
        dataType: 'Json',
        data: { 'id': id },
        type: 'POST',
        success: function (data) {
            var optionstr = '<option value="0"></option>';
            $.each(data, function (i, item) {
                optionstr = optionstr + '<option value="' + item.REGION_ID + '">' + item.REGION_NAME + '</option>';
            });
            $("#" + control).html('');
            $("#" + control).html(optionstr);
            if (control == 'redrpcity') {
                $("#member_MemberCity").val('');
                $("#redrparea").html('<option value="0"></option>');
                $("#member_MemberArea").val('');
            }
        }
    });
}
function activemember(id) {
    $.ajax({
        url: '/WebFormArea/WebHome/ActiveMember',
        dataType: 'Json',
        data: { 'memberid': id },
        type: 'POST',
        success: function (data) {
            if (data == '1') {
                location.reload();
            }
            else {
                alert(data);
            }
        }
    });
}