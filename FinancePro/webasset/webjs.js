$(function () {
    $("#idfpic").hide();
    $("#erridfpic").hide();
    $("#idbpic").hide();
    $("#erridbpic").hide();
    $("#idwhbpic").hide();
    $("#erridwhbpic").hide();
    $("#cbpic").hide();
    $("#errcbpic").hide();
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
    $("#fileupload").fileupload({
        dataType: 'json',
        type: "POST",
        singleFileUploads: true,
        autoUpload: true,
        done: function (e, data) {
            var path = data.result.path;
            var urlpath = data.result.urlpath;
            var ustatus = data.result.status;
            var msg = data.result.meg;
            if (ustatus) {
                $("#upload1").hide();
                $("#idfpic").show();
                $("#erridfpic").hide();
                $("#idfpic").attr("src", urlpath);
                $("#applymodel_IDNumberFrontPath").val(path);
            }
            else {
                $("#erridfpic").show();
                $("#erridfpic").html(msg);
            }
        }
    });
    $("#fileupload1").fileupload({
        dataType: 'json',
        type: "POST",
        singleFileUploads: true,
        autoUpload: true,
        done: function (e, data) {
            var path = data.result.path;
            var urlpath = data.result.urlpath;
            var ustatus = data.result.status;
            var msg = data.result.meg;
            if (ustatus) {
                $("#upload2").hide();
                $("#idbpic").show();
                $("#erridbpic").hide();
                $("#idbpic").attr("src", urlpath);
                $("#applymodel_IDNumberBackPath").val(path);
            }
            else {
                $("#erridbpic").show();
                $("#erridbpic").html(msg);
            }
        }
    });
    $("#fileupload2").fileupload({
        dataType: 'json',
        type: "POST",
        singleFileUploads: true,
        autoUpload: true,
        done: function (e, data) {
            var path = data.result.path;
            var urlpath = data.result.urlpath;
            var ustatus = data.result.status;
            var msg = data.result.meg;
            if (ustatus) {
                $("#upload3").hide();
                $("#idwhbpic").show();
                $("#erridwhbpic").hide();
                $("#idwhbpic").attr("src", urlpath);
                $("#applymodel_IDWithHandPath").val(path);
            }
            else {
                $("#erridwhbpic").show();
                $("#erridwhbpic").html(msg);
            }
        }
    });
    $("#fileupload3").fileupload({
        dataType: 'json',
        type: "POST",
        singleFileUploads: true,
        autoUpload: true,
        done: function (e, data) {
            var path = data.result.path;
            var urlpath = data.result.urlpath;
            var ustatus = data.result.status;
            var msg = data.result.meg;
            if (ustatus) {
                $("#upload4").hide();
                $("#cbpic").show();
                $("#errcbpic").hide();
                $("#cbpic").attr("src", urlpath);
                $("#applymodel_CardFrontPath").val(path);
            }
            else {
                $("#errcbpic").show();
                $("#errcbpic").html(msg);
            }
        }
    });
});
function valuechang() {
    var transfernum = $("#transfer_Transfernum").val();
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
    var max = memberpoints * maxnum / 100;
    if (cashnum > max) {
        alert("提现金额超出平台限定值");
        return;
    }
    var fee = cashnum * feenum / 100;
    $("#cashorder_CommissionNum").val(fee);
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
function updatepwd() {
    var newpwd = $("#newpwd").val();
    var cnewpwd = $("#cnewpwd").val();
    if (newpwd == cnewpwd) {
        $.ajax({
            url: '/WebFormArea/WebHome/UpdateLogpwd',
            dataType: 'Json',
            data: { 'newpwd': cnewpwd },
            type: 'POST',
            success: function (data) {
                if (data == '1') {
                    $("#pwderror").html("操作成功");
                }
                else {
                    alert("操作失败！");
                }
            }
        });
    }
    else {
        $("#pwderror").html("两次密码输入不一致");
    }
}
function updatesecpwd() {
    var newpwd = $("#newsecpwd").val();
    var cnewpwd = $("#cnewsecpwd").val();
    if (newpwd == cnewpwd) {
        $.ajax({
            url: '/WebFormArea/WebHome/UpdateSecpwd',
            dataType: 'Json',
            data: { 'newpwd': cnewpwd },
            type: 'POST',
            success: function (data) {
                if (data == '1') {
                    $("#scpwderror").html("操作成功");
                }
                else {
                    alert("操作失败！");
                }
            }
        });
    }
    else {
        $("#scpwderror").html("两次密码输入不一致");
    }
}