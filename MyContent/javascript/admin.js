$(function () {
    $('[data-toggle="tooltip"]').tooltip({ html: true });
    $('[data-toggle="tooltip"]').tooltip();
});

//系统帮助和说明的点击事件
function myHelpLink() {
    $(".btn-lg").click();
}

//展示更改密码的页面
function UpdatePwd() {
    //alert("你点击了");
    $(".main_content_right_0").html("&nbsp;&nbsp;&nbsp;&nbsp;当前位置：学生 -----> 修改密码 ");
    $(".main_content_right_content_title").html("修改密码");
    $("#mydiv1").html("");
    $("#mydiv1").addClass("myRoundBorder");
    $("#mydiv1").addClass("myGrayBackground");
    $.post("/UserInfo/HtmlStringUpdatePwd", {}, function (returnStr) {
        $("#mydiv1").html(returnStr);
    });
}

//更改密码的提交按钮
function subBtn() {
    //$("#UpdatePwdForm");
    //alert("恭喜的1等等的");

    if ($("#txtOldPwd").val() != "") {
        if ($("#txtNewPwd1").val() != "") {
            if ($("#txtNewPwd2").val() != "") {
                if ($("#txtNewPwd1").val() == $("#txtNewPwd2").val()) {
                    $.post("/UserInfo/UpdateUserPwd", { "oldPwd": $("#txtOldPwd").val(), "newPwd1": $("#txtNewPwd1").val(), "newPwd2": $("#txtNewPwd2").val() }, function (returnStr) {
                        if (returnStr == "Success") {
                            $("#errorMsg").removeClass("myError");
                            $("#errorMsg").addClass("mySuccess");
                            $("#errorMsg").html("修改密码成功！");
                            cancelBtn();
                        }
                        else if (returnStr == "Error1") {
                            //alert("提交的表单出错！");
                            $("#errorMsg").removeClass("mySuccess");
                            $("#errorMsg").addClass("myError");
                            $("#errorMsg").html("提交表单出错！");
                        }
                        else if (returnStr == "Error2") {
                            //alert("更改密码失败！");
                            $("#errorMsg").removeClass("mySuccess");
                            $("#errorMsg").addClass("myError");
                            $("#errorMsg").html("更改密码失败！");
                        }
                    });
                }
                else {
                    alert("两次输入的密码不一致！");
                }
            }
            else {
                alert("请再次输入新密码！");
            }
        }
        else {
            alert("请输入新密码！")
        }
    }
    else {
        alert("请输入旧密码！");
    }
}

//更改密码的取消按钮
function cancelBtn() {
    $("#txtOldPwd").val("");
    $("#txtNewPwd1").val("");
    $("#txtNewPwd2").val("");

}

function AdminInfoEditClick() {
    //alert("sadasd");
    if ($("#StuEditTable").hasClass("myHide")) {
        $("#StuEditTable").removeClass("myHide");
    } else {
        $("#StuEditTable").addClass("myHide");
    }
}

function AdminSaveClick() {
    if ($("#editPhone").val() != "" && CheckPhone($("#editPhone").val()) == 1) {
        if ($("#editEmail").val() != "" && CheckEmail($("#editEmail").val()) == 1) {
            //alert("验证完成，准备上传：" + $("#editPhone").val() + "___" + $("#editEmail").val());

            $.post("/Admin/UpdateAdminInfo", { "phone": $("#editPhone").val(), "email": $("#editEmail").val() }, function (data) {
                if (data == "success") {
                    //$("#stuSaveMsg").removeClass("myError");
                    //$("#stuSaveMsg").addClass("mySuccess");   
                    //$("#stuSaveMsg").text("修改成功！");
                    alert("修改成功！");
                    window.location.href = "/Admin/Index";
                } else if (data == "error") {
                    $("#stuSaveMsg").removeClass("mySuccess");
                    $("#stuSaveMsg").addClass("myError");
                    $("#stuSaveMsg").text("修改失败，请重试！");
                }
                else {
                    alert("修改失败，请重试！");
                }
            });


        } else {

            $("#stuSaveMsg").removeClass("mySuccess");
            $("#stuSaveMsg").addClass("myError");
            $("#stuSaveMsg").text("邮箱格式不正确！");
        }
    } else {
        $("#stuSaveMsg").removeClass("mySuccess");
        $("#stuSaveMsg").addClass("myError");
        $("#stuSaveMsg").text("联系电话格式不正确！");
    }
}


$("#editPhone").bind("focus", function () {
    $("#stuSaveMsg").text("");
});

$("#editEmail").bind("focus", function () {
    $("#stuSaveMsg").text("");
});

//检查是否为数字
function CheckIsNumber(number) {
    var t = /^\d+(\.\d+)?$/;
    return t.test(number);
}

//邮箱格式验证
function CheckEmail(email) {
    var strReg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;

    if (!strReg.test(email)) {
        return -1;
    }
    return 1;
}

//验证联系电话的方式
function CheckPhone(phone) {
    //134，135，136，137，138，139，147，150，151，152，157，158，159，182，187，188【16】
    //130，131，132，155，156，185，186【7】
    //133，153，180，189【4】
    //----------------------------------------------
    //130——————139【10】
    //147————————【1】
    //150，151，152，153，155，156，157，158，159【9】
    //180，182，185，186，187，188，189【7】
    var phoneReg = /^(13+\d{9})|(147+\d{8})|(150+\d{8})|(151+\d{8})|(152+\d{8})|(153+\d{8})|(155+\d{8})|(156+\d{8})|(157+\d{8})|(158+\d{8})(159+\d{8})|(180+\d{8})|(182+\d{8})|(185+\d{8})|(186+\d{8})|(187+\d{8})|(188+\d{8})|(189+\d{8})$/;

    if (!phoneReg.test(phone) || phone.length != 11) {
        return -1;
    }
    return 1;
}

//收缩菜单的点击事件
function show() {
    // alert("222");
    if ($("#li").css("display") == "none") {
        $("#li").css("display", "");
        $(".myLeftMenu").html("");
        $(".myLeftMenu").html("<span class='glyphicon glyphicon-menu-down' aria-hidden='true'></span>&nbsp;");
    } else {
        $("#li").css("display", "none");
        $(".myLeftMenu").html("");
        $(".myLeftMenu").html("<span class='glyphicon glyphicon-menu-left' aria-hidden='true'></span>&nbsp;");
    }
   
}

//菜单的相关事件
$(".main_content_left_1").bind("mouseover", {}, function () {
    $(this).css("background-color", "#FFFF99");
});

$(".main_content_left_1").bind("mouseout", {}, function () {
    $(this).css("background-color", "#CCCCFF");
});

$("#myListmenu").bind("mousedown", {}, function () {
    show();
});





//单选框的全选功能
function btnSelectAll() {
    $('#t2').find(':checkbox').each(function () {
        if ($(this).is(":checked")) {
            $(this).attr("checked",false);
            //$(this).removeAttr("checked");
        }
        else {
            //$(this).addAttr("checked","checked");
            $(this).attr("checked", true);
        }
    });
}