
//工具提示框的相关设置
//$(function () { $('.tooltip-show').tooltip('show'); });
//$(function () { $('.tooltip-hide').tooltip('hide'); });
//$(function () { $('.tooltip-destroy').tooltip('destroy'); });
//$(function () { $('.tooltip-toggle').tooltip('toggle'); });
$(function () {
    $(".tooltip-options a").tooltip({ html: true });
    $("#editPhone").tooltip({ html: true });
    $("#editEmail").tooltip({ html: true });
    $("#editCourseName1").tooltip({ html: true });
    $("#editCourseName2").tooltip({ html: true });
    $("#editCourseName3").tooltip({ html: true });
    $("#btnDownLoadClick").tooltip({ html: true });
    $('[data-toggle="tooltip"]').tooltip({ html: true });
});

$(function () {
    //$("input[name='act_start_time'],input[name='act_stop_time']").datetimepicker();
    $("#SubCourseInfo_main_content").css("height", "1600px");

    SetBeginValue();
    $('[data-toggle="tooltip"]').tooltip();
    
    $(".tooltip-options a").tooltip({ html: true });

    //$("#editEmail").attr("title", "<span class ='myError'> 12312 </span>");
});

//编辑个人信息点击事件
function TeacherInfoEditClick() {
    //alert("sadasd");
    if ($("#StuEditTable").hasClass("myHide")) {
        $("#StuEditTable").removeClass("myHide");
    } else {
        $("#StuEditTable").addClass("myHide");
    }
}
//保存信息点解事件
function TeacherSaveClick() {
    if ($("#editPhone").val() != "" && CheckPhone($("#editPhone").val()) == 1) {
        if ($("#editEmail").val() != "" && CheckEmail($("#editEmail").val()) == 1) {
            //alert("验证完成，准备上传：" + $("#editPhone").val() + "___" + $("#editEmail").val());

            $.post("/UserInfo/UpdateTeacherInfo", { "phone": $("#editPhone").val(), "email": $("#editEmail").val(), "courseName1": $("#editCourseName1").val(), "courseName2": $("#editCourseName2").val(), "courseName3": $("#editCourseName3").val() }, function (data) {
                if (data == "success") {
                    //$("#stuSaveMsg").removeClass("myError");
                    //$("#stuSaveMsg").addClass("mySuccess");   
                    //$("#stuSaveMsg").text("修改成功！");
                    alert("修改成功！");
                    window.location.href = "/UserInfo/Teacher";
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

$(".main_content_left_1").bind("mouseover", {}, function () {
    $(this).css("background-color", "#FFFF99");
});

$(".main_content_left_1").bind("mouseout", {}, function () {
    $(this).css("background-color", "#CCCCFF");
});

$(document).ready(function () {
    //SetBeginValue();
});

function SetBeginValue() {
    var myDate = new Date();
    var Year = myDate.getFullYear();
    var Month = parseInt(myDate.getMonth()) + 1;
    var Day = parseInt( myDate.getDate() );
    var Hour = myDate.getHours();
    var Minute = myDate.getMinutes();
    var Second = myDate.getSeconds();

    //alert(Year);
    $("#beginSelectYear").val(Year);
    $("#beginSelectMonth").val(Month);
    $("#beginSelectDay").val(Day);
    $("#beginSelectHour").val(Hour);
    $("#beginSelectMinute").val(Minute);
    $("#beginSelectSecond").val(Second);
}



//展示总的提示
function ShowTotalMsg(linenumber) {
    var num;
    if (linenumber == "One") {
        num = "一"
    } else if (linenumber == "Two") {
        num = "二";
    } else if (linenumber == "Three") {
        num = "三";
    } else if (linenumber == "Four") {
        num = "四";
    } else if (linenumber == "Five") {
        num = "五";
    } else if (linenumber == "Six") {
        num = "六";
    } else if (linenumber == "Seven") {
        num = "七";
    } else if (linenumber == "Eight") {
        num = "八";
    }
    $("#totalMsg").text("第" + num + "行不合法，请修改再提交！");
    $("#totalMsg").addClass("myError");
    $("#totalMsg").addClass("myMsgLine");
    $("#totalMsg").addClass("totalMsgBG");
}

//清空总的提示
function ClearTotalMsg() {
    $("#totalMsg").text("");
    $("#totalMsg").removeClass("myError");
    $("#totalMsg").removeClass("myMsgLine");
    $("#totalMsg").removeClass("totalMsgBG");
}

//展示行尾的错误提示
function ShowErrorMsg(linenumber) {
    $("#Msg" + linenumber + "Line").html("<span class='glyphicon glyphicon-remove-circle myMsgLine' aria-hidden='true'></span>");
    $("#Msg" + linenumber + "Line").removeClass("mySuccess");
    $("#Msg" + linenumber + "Line").addClass("myError");
}

//展示行尾的成功提示
function ShowSuccessMsg(linenumber) {
    $("#Msg" + linenumber + "Line").html("<span class='glyphicon glyphicon-ok-circle myMsgLine' aria-hidden='true'></span>");
    $("#Msg" + linenumber + "Line").removeClass("myError");
    $("#Msg" + linenumber + "Line").addClass("mySuccess");

    ClearTotalMsg(linenumber);
}

//清空提示的展示， KeyDown事件触发
function ClearMsg(linenumber) {
    $("#Msg" + linenumber + "Line").html("");
    $("#Msg" + linenumber + "Line").removeClass("myError");
    $("#Msg" + linenumber + "Line").removeClass("mySuccess");
}

//检查是否为数字
function CheckIsNumber(number) {
    var t = /^\d+(\.\d+)?$/;
    return t.test(number);
}






function UpdatePwd() {
    //alert("你点击了");
    $(".main_content_right_0").html("&nbsp;&nbsp;&nbsp;&nbsp;当前位置：教师 -----> 修改密码 ");
    $(".main_content_right_content_title").html("修改密码");
    $("#mydiv1").html("");
    $("#mydiv1").addClass("myRoundBorder");
    $("#mydiv1").addClass("myGrayBackground");
    $.post("/UserInfo/HtmlStringUpdatePwd", {}, function (returnStr) {
        $("#mydiv1").html(returnStr);
    });


    $("#teacher_main_content").css("height", "620px");

}

function subBtn() {
    //$("#UpdatePwdForm");

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

function cancelBtn() {
    $("#txtOldPwd").val("");
    $("#txtNewPwd1").val("");
    $("#txtNewPwd2").val("");

}

function myHelpLink() {
    $(".btn-lg").click();
}

//--------------------------------教师提交课程（开始）------------------------------------------------------------//

//①绑定文本框的的KedDown事件
$("#txtCourseName").bind("keydown", { "Type": "CourseName" }, PublicKewDown2);
//$("#txtBeginClassYear").bind("keydown", { "Type": "BeginClassYear" }, PublicKewDown2);
//$("#SelectClassDays").bind("keydown", { "Type": "SelectClassDays" }, PublicKewDown2);

$("#txtCourseName").bind("keydown", { "Type": "One" }, PublicKewDown2);
$("#txtCredit").bind("keydown", { "Type": "Three" }, PublicKewDown2);
$("#txtCount").bind("keydown", { "Type": "Three" }, PublicKewDown2);
$("#txtClassRoom").bind("keydown", { "Type": "Three" }, PublicKewDown2);
$("#txtBeginClassYear").bind("keydown", { "Type": "Four" }, PublicKewDown2);
$("#txtEndClassYear").bind("keydown", { "Type": "Four" }, PublicKewDown2);


function PublicKewDown2(d) {
    var Type = d.data["Type"];

    //$("#txtCourseName").val(Type);

    if (Type == "CourseName") {
        var text = $("#txtCourseName").val();

        if (text.length >= 20) {
            $("#CourseNameErrorMsg").text("长度不能超过20！");
            $("#CourseNameErrorMsg").addClass("myError");
            return;
        }


        $("#CourseNameErrorMsg").text("");
        $("#CourseNameErrorMsg").removeClass("myError");

        if (text.length > 10 && $("#txtCourseName").width() < 700) {
            $("#txtCourseName").css("width", 270 + (text.length - 10) * 27 + "px");


        }
    } else {
        ClearMsg(Type);
        ClearTotalMsg(Type);
    }

  
}


//②绑定文本框的OnBlur事件
$("#txtCourseName").bind("blur", { "Type": "CourseName" }, PublicBlur2);
$("#txtBeginClassYear").bind("blur", { "Type": "BeginClassYear" }, PublicBlur2);

function PublicBlur2(d) {
    var Type = d.data["Type"];
    

}

//③绑定文本框的OnKeyUp事件
$("#txtBeginClassYear").bind("keyup", { "Type": "BeginClassYear" }, PublicKeyUp2);
$("#SelectClassDays").bind("keyup", { "Type": "SelectClassDays" }, PublicKeyUp2);

$("#beginSelectYear").bind("keyup", { "Type": "beginSelectYear" }, PublicKeyUp2);
$("#beginSelectMonth").bind("keyup", { "Type": "beginSelectMonth" }, PublicKeyUp2);
$("#beginSelectDay").bind("keyup", { "Type": "beginSelectDay" }, PublicKeyUp2);
$("#beginSelectHour").bind("keyup", { "Type": "beginSelectHour" }, PublicKeyUp2);
$("#beginSelectMinute").bind("keyup", { "Type": "beginSelectMinute" }, PublicKeyUp2);
$("#beginSelectSecond").bind("keyup", { "Type": "beginSelectSecond" }, PublicKeyUp2);

function PublicKeyUp2(d){
    var Type = d.data["Type"];

    if (Type == "BeginClassYear") {
        if ($("#txt" + Type).val().length == 4) {
            $("#txtEndClassYear").val(parseInt($("#txt" + Type).val()) + 1);
            $("#txtEndClassYear").focus();
            $("txtEndClassYear").attr("readonly", "readonly");
        }
    } else if (Type == "beginSelectYear") {
        if ($("#beginSelectYear").val().length == 4) {
            $("#beginSelectMonth").focus();
        }
    } else if (Type == "beginSelectMonth") {
        if ($("#beginSelectMonth").val().length == 2) {
            $("#beginSelectDay").focus();
        }
    } else if (Type == "beginSelectDay") {
        if ($("#beginSelectDay").val().length == 2) {
            $("#beginSelectHour").focus();
        }
    } else if (Type == "beginSelectHour") {
        if ($("#beginSelectHour").val().length == 2) {
            $("#beginSelectMinute").focus();
        }
    } else if (Type == "beginSelectMinute") {
        if ($("#beginSelectMinute").val().length == 2) {
            $("#beginSelectSecond").focus();
        }
    } else if (Type == "beginSelectSecond") {
        if ($("#beginSelectSecond").val().length == 2) {
            $("#endSelectYear").focus();
        }
    } else if (Type == "SelectClassDays") {
        $("#MsgSelectClassDays").text("");
        $("#MsgSelectClassDays").removeClass("myError");

        //获取文本框的值
        var days = $("#SelectClassDays").val();

        if (days > 0 && days <= 10) {

            $("#endSelectSecond").val($("#beginSelectSecond").val());
            $("#endSelectMinute").val($("#beginSelectMinute").val());
            $("#endSelectHour").val($("#beginSelectHour").val());


            var myDate = new Date();
            var year = myDate.getFullYear();
            var month = myDate.getMonth();
            var day = myDate.getDate();

            var m1 = new Array();
            var totalday = parseInt(day) + parseInt(days);

            //$("#endSelectMonth").val(parseInt( day) + parseInt( days));
            //闰年
            if ((year % 4 == 0 && year % 100 != 0) || (year % 100 == 0 && year % 400 == 0)) {
                m1 = [31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

                if (totalday > m1[month]) {
                    month += 1;
                    day = totalday - m1[month];
                } else {
                    day = totalday;
                }
            } else {//不是闰年
                m1 = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
                if (totalday > m1[month]) {
                    month += 1;
                    day = totalday - m1[month];
                } else {
                    day = totalday;
                }

            }


            if ($("#beginSelectYear").val() != "") {
                $("#endSelectYear").val($("#beginSelectYear").val());
            } else {
                $("#endSelectYear").val(year);
            }

            if ($("#beginSelectMonth").val() != "") {
                $("#endSelectMonth").val($("#beginSelectMonth").val());
            } else {
                $("#endSelectYear").val(month + 1);
            }


            if ($("#beginSelectDay").val() != "") {
                $("#endSelectDay").val( parseInt( $("#beginSelectDay").val()) + parseInt( days));
            } else {
                $("#endSelectDay").val(day);
            }
            //$("#endSelectDay").val(day);
            //$("#endSelectMonth").val(month + 1);
           


          
        }
        else {//当前输入的天数过大
            if (days > 10) {
                $("#MsgSelectClassDays").text("输入有错，请输入 1~10 之间的数字");
                $("#MsgSelectClassDays").addClass("myError");
            }
        }
    }

}

//④绑定文本框的KeyPress时间
$("#beginSelectYear").bind("keypress", { "Type": "beginSelectYear" }, PublicKeyPress2);
$("#beginSelectMonth").bind("keypress", { "Type": "beginSelectMonth" }, PublicKeyPress2);
$("#beginSelectDay").bind("keypress", { "Type": "beginSelectDay" }, PublicKeyPress2);
$("#beginSelectHour").bind("keypress", { "Type": "beginSelectHour" }, PublicKeyPress2);
$("#beginSelectMinute").bind("keypress", { "Type": "beginSelectMinute" }, PublicKeyPress2);
$("#beginSelectSecond").bind("keypress", { "Type": "beginSelectSecond" }, PublicKeyPress2);

function PublicKeyPress2(d) {
    var Type = d.data["Type"];

    //if (Type == "BeginClassYear") {
    //    if ($("#txt" + Type).val().length == 4) {
    //        $("#txtEndClassYear").val(parseInt($("#txt" + Type).val()) + 1);
    //        $("#txtEndClassYear").focus();
    //        $("txtEndClassYear").attr("readonly", "readonly");
    //    }
    //}
    //} else if (Type == "beginSelectYear") {
    //    if ($("#beginSelectYear").val().length == 4) {
    //        $("#beginSelectMonth").focus();
    //    }
    //} else if (Type == "beginSelectMonth") {
    //    if ($("#beginSelectMonth").val().length == 2) {
    //        $("#beginSelectDay").focus();
    //    }
    //} else if (Type == "beginSelectDay") {
    //    if ($("#beginSelectDay").val().length == 2) {
    //        $("#beginSelectHour").focus();
    //    }
    //} else if (Type == "beginSelectHour") {
    //    if ($("#beginSelectHour").val().length == 2) {
    //        $("#beginSelectMinute").focus();
    //    }
    //} else if (Type == "beginSelectMinute") {
    //    if ($("#beginSelectMinute").val().length == 2) {
    //        $("#beginSelectSecond").focus();
    //    }
    //} else if (Type == "beginSelectSecond") {
    //    if ($("#beginSelectSecond").val().length == 2) {
    //        $("#endSelectYear").focus();
    //    }
    //}
}



//提交按钮和取消按钮的样式
$(".myCourseSubBtn1").bind("mouseover", function () {
    $(this).addClass("myCourseSubBtn2");
});

$(".myCourseSubBtn1").bind("mouseout", function () {
    $(this).removeClass("myCourseSubBtn2");
});


//提交按钮的点击事件
$("#courseInfoSubBtn").bind("click",{"Action":"New"}, SubBtnClick);

function SubBtnClick(d) {

    if ($("#txtCourseName").val() != "" && !CheckIsNumber($("#txtCourseName").val())) {
        //var result = CheckIsNumber($("#txtCourseName").val());
        ShowSuccessMsg("One");

        if ($("#courseType").val() != 0) { 
            if ($("#checkType").val() != 0) {
                ShowSuccessMsg("Two");

                if ($("#txtCredit").val() != "" &&  CheckIsNumber($("#txtCredit").val())) {
                    if ($("#txtCount").val() != "" && CheckIsNumber($("#txtCount").val())) {
                        if ($("#txtClassRoom").val() != ""  ) {
                            ShowSuccessMsg("Three");

                           
                            //var result = CheckIsNumber($("#txtBeginClassYear").val());
                            //var result2 = CheckIsNumber($("#txtEndClassYear").val());
                            //alert(result +"-"+result2);
                            if ($("#txtBeginClassYear").val() != "" && CheckIsNumber($("#txtBeginClassYear").val())) {
                                if ($("#txtEndClassYear").val() != "" && CheckIsNumber($("#txtEndClassYear").val())) {
                                    if ($("#team").val() != 0) {
                                        ShowSuccessMsg("Four");

                                        if ($("#beginWeek").val() != 0) {
                                            if ($("#endWeek").val() != 0) {
                                                if ($("#classDay").val() != 0) {
                                                    if ($("#beginClassNumber").val() != 0) {
                                                        if ($("#endClassNumber").val() != 0) {
                                                            ShowSuccessMsg("Five");

                                                            if ($("#beginSelectYear").val() != "" && CheckIsNumber($("#beginSelectYear").val())) {
                                                                if ($("#beginSelectMonth").val() != "" && CheckIsNumber($("#beginSelectMonth").val())) {
                                                                    if ($("#beginSelectDay").val() != "" && CheckIsNumber($("#beginSelectDay").val())) {
                                                                        if ($("#beginSelectHour").val() != "" && CheckIsNumber($("#beginSelectHour").val())) {
                                                                            if ($("#beginSelectMinute").val() != "" && CheckIsNumber($("#beginSelectMinute").val())) {
                                                                                //if ($("#beginSelectSecond").val() != "" && CheckIsNumber($("#beginSelectSecond").val())) {
                                                                                ShowSuccessMsg("Six");
                                                                                if ($("#endSelectYear").val() != "" && CheckIsNumber($("#endSelectYear").val())) {
                                                                                    if ($("#endSelectMonth").val() != "" && CheckIsNumber($("#endSelectMonth").val())) {
                                                                                        if ($("#endSelectDay").val() != "" && CheckIsNumber($("#endSelectDay").val())) {
                                                                                            if ($("#endSelectHour").val() != "" && CheckIsNumber($("#endSelectHour").val())) {
                                                                                                if ($("#endSelectMinute").val() != "" && CheckIsNumber($("#endSelectMinute").val())) {
                                                                                                       
                                                                                                    ShowSuccessMsg("Eight");
                                                                                                    //验证完成，提交信息
                                                                                                    $("#totalMsg").text("提交信息成功！");
                                                                                                    $("#totalMsg").addClass("totalMsgBG");
                                                                                                    $("#totalMsg").removeClass("myError");
                                                                                                    $("#totalMsg").addClass("mySuccess");
                                                                                                    $("#totalMsg").addClass("myMsgLine");

                                                                                                    $.post("/UserInfo/ExecuteInsertAndUpdateCourseInfo", { "CourseName": $("#txtCourseName").val(), "CourseType": $("#courseType").val(), "CheckType": $("#checkType").val(), "Credit": $("#txtCredit").val(), "Count": $("#txtCount").val(), "ClassRoom": $("#txtClassRoom").val(), "BeginClassYear": $("#txtBeginClassYear").val(), "EndClassYear": $("#txtEndClassYear").val(), "Team": $("#team").val(), "BeginWeek": $("#beginWeek").val(), "EndWeek": $("#endWeek").val(), "ClassDay": $("#classDay").val(), "BeginClassNumber": $("#beginClassNumber").val(), "EndClassNumber": $("#endClassNumber").val(), "BeginSelectYear": $("#beginSelectYear").val(), "BeginSelectMonth": $("#beginSelectMonth").val(), "BeginSelectDay": $("#beginSelectDay").val(), "BeginSelectHour": $("#beginSelectHour").val(), "BeginSelectMinute": $("#beginSelectMinute").val(), "EndSelectYear": $("#endSelectYear").val(), "EndSelectMonth": $("#endSelectMonth").val(), "EndSelectDay": $("#endSelectDay").val(), "EndSelectHour": $("#endSelectHour").val(), "EndSelectMinute": $("#endSelectMinute").val(), "Remark": $("#txtRemark").val(), "CourseNum": $("#txtCourseNum").val(), "Action": d.data["Action"] }, function (result) {
                                                                                                        if (result == "Success") {
                                                                                                            if(d.data["Action"] == "New"){
                                                                                                                $("#totalMsg").text("课程发布成功！");
                                                                                                                $("#totalMsg").addClass("totalMsgBG");
                                                                                                                $("#totalMsg").removeClass("myError");
                                                                                                                $("#totalMsg").addClass("mySuccess");
                                                                                                                $("#totalMsg").addClass("myMsgLine");
                                                                                                            }else{
                                                                                                                $("#totalMsg").text("课程修改成功！");
                                                                                                                $("#totalMsg").addClass("totalMsgBG");
                                                                                                                $("#totalMsg").removeClass("myError");
                                                                                                                $("#totalMsg").addClass("mySuccess");
                                                                                                                $("#totalMsg").addClass("myMsgLine");

                                                                                                                //setTimeout($("#courseInfoBackBtn").click(), 5000);
                                                                                                               
                                                                                                            }
                                                                                                        } else if (result == "Error") {
                                                                                                            if(d.data["Action"] == "New"){
                                                                                                                $("#totalMsg").text("课程发布失败，请重试！");
                                                                                                                $("#totalMsg").addClass("totalMsgBG");
                                                                                                                $("#totalMsg").removeClass("mySuccessr");
                                                                                                                $("#totalMsg").addClass("myError");
                                                                                                                $("#totalMsg").addClass("myMsgLine");
                                                                                                            }else{
                                                                                                                $("#totalMsg").text("课程修改失败，请重试！");
                                                                                                                $("#totalMsg").addClass("totalMsgBG");
                                                                                                                $("#totalMsg").removeClass("mySuccessr");
                                                                                                                $("#totalMsg").addClass("myError");
                                                                                                                $("#totalMsg").addClass("myMsgLine");
                                                                                                            }
                                                                                                        }
                                                                                                    });

                                                                                                        
                                                                                                } else {
                                                                                                    //endSelectMinute
                                                                                                    ShowErrorMsg("Eight");
                                                                                                    ShowTotalMsg("Eight");
                                                                                                }
                                                                                            } else {
                                                                                                //endSelectHour
                                                                                                ShowErrorMsg("Eight");
                                                                                                ShowTotalMsg("Eight");
                                                                                            }
                                                                                        } else {
                                                                                            //endSelectDay
                                                                                            ShowErrorMsg("Eight");
                                                                                            ShowTotalMsg("Eight");
                                                                                        }
                                                                                    } else {
                                                                                        //endSelectMonth
                                                                                        ShowErrorMsg("Eight");
                                                                                        ShowTotalMsg("Eight");
                                                                                    }
                                                                                } else {
                                                                                    //endSelectYear
                                                                                    ShowErrorMsg("Eight");
                                                                                    ShowTotalMsg("Eight");
                                                                                }
                                                                                //} else {
                                                                                //    //beginSelectSecond
                                                                                //    ShowErrorMsg("Six");
                                                                                //    ShowTotalMsg("Six");
                                                                                //}
                                                                            } else {
                                                                                //beginSelectMinute
                                                                                ShowErrorMsg("Six");
                                                                                ShowTotalMsg("Six");
                                                                            }
                                                                        } else {
                                                                            //beginSelectHour
                                                                            ShowErrorMsg("Six");
                                                                            ShowTotalMsg("Six");
                                                                        }
                                                                    } else {
                                                                        //beginSelectDay
                                                                        ShowErrorMsg("Six");
                                                                        ShowTotalMsg("Six");
                                                                    }
                                                                } else {
                                                                    //beginSelectMonth
                                                                    ShowErrorMsg("Six");
                                                                    ShowTotalMsg("Six");
                                                                }
                                                            } else {
                                                                //beginSelectYear
                                                                ShowErrorMsg("Six");
                                                                ShowTotalMsg("Six");
                                                            }
                                                        } else {
                                                            //endClassNumber
                                                            ShowErrorMsg("Five");
                                                            ShowTotalMsg("Five");
                                                        }
                                                    } else {
                                                        //beginClassNumber
                                                        ShowErrorMsg("Five");
                                                        ShowTotalMsg("Five");
                                                    }
                                                } else {
                                                    //classDay
                                                    ShowErrorMsg("Five");
                                                    ShowTotalMsg("Five");
                                                }
                                            } else {
                                                //endWeek
                                                ShowErrorMsg("Five");
                                                ShowTotalMsg("Five");
                                            }
                                        } else {
                                            //beginWeek
                                            ShowErrorMsg("Five");
                                            ShowTotalMsg("Five");
                                        }
                                    } else {
                                        //team
                                        ShowErrorMsg("Four");
                                        ShowTotalMsg("Four");
                                    }
                                } else {
                                    //txtEndClassYear
                                    ShowErrorMsg("Four");
                                    ShowTotalMsg("Four");
                                }
                            } else {
                                //txtBeginClassYear
                                ShowErrorMsg("Four");
                                ShowTotalMsg("Four");
                            }
                        } else {
                            //txtClassRoom
                            ShowErrorMsg("Three");
                            ShowTotalMsg("Three");
                        }
                    } else {
                        //txtCount
                        ShowErrorMsg("Three");
                        ShowTotalMsg("Three");
                    }
                } else {
                    //txtCredit
                    ShowErrorMsg("Three");
                    ShowTotalMsg("Three");
                }
            } else {
                //checkType
                ShowErrorMsg("Two");
                ShowTotalMsg("Two");
            }

        } else {
            //courseType
            ShowErrorMsg("Two");
            ShowTotalMsg("Two");
        }
    } else {
        //courseName
        //$("#MsgOneLine").html("<span class='glyphicon glyphicon-remove-circle myMsgLine' aria-hidden='true'></span>");
        //$("#MsgOneLine").addClass("myError");
        ShowErrorMsg("One");
        ShowTotalMsg("One");
    }
}

//取消按钮的点击事件
$("#courseInfoCancelBtn").bind("click", function () {
    if (confirm("你确定要取消？你所填的信息都将会被删除！")) {
        $("#txtCourseName").val("");
        $("#courseType").val(0);
        $("#checkType").val(0);
        $("#txtCredit").val("");
        $("#txtCount").val("");
        $("#txtClassRoom").val("");
        $("#txtBeginClassYear").val("");
        $("#txtendClassYear").val("");
        $("#team").val("");
        $("#classDay").val(0);

        SetBeginValue();

        //情况错误提示信息
        var LineNumber = new Array();
        LineNumber = ["One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight"];

        for (var i = 0; i < 8; i++) {
            ClearMsg(LineNumber[i]);
        }

        //清空总的错误提示信息
        ClearTotalMsg();
    }
    
});


//--------------------------------教师提交课程（结束）------------------------------------------------------------//




//--------------------------------教师修改课程信息（开始）------------------------------------------------------------//
    //删除按钮的事件绑定函数
function DelCourseInfo(CourseNum, trNumber, State) {

    if (State != "待检验") {
        alert("对不起，此项无法删除！");
        return;
    }

    if (confirm("你确定要删除么？")) {
        $.post("/UserInfo/DelCourseInfo", { "courseNum": CourseNum }, function (returnStr) {
            if (returnStr == "Success") {
                $("#tr_" + trNumber).remove();
            } else if( returnStr == "Error") {
                alert("删除失败，请重试！");
            }
        });
    }
}

    //修改按钮的事件绑定函数
function UpdateCourseInfo(CourseNum, State) {
    if (State != "待检验") {
        alert("对不起，此项无法修改！");
        return;
    }
    $("#myDiv3").addClass("myHide");
    $("#myDiv3_2").removeClass("myHide");
    FullAllTextBox(CourseNum, State);
}

//获取相关的课程信息
function FullAllTextBox(courseNum,state) {

    $.post("/UserInfo/GetFullCourseInfoByCourseNum", { "courseNum": courseNum, "state": state }, function (data) {

        if (data == "Error") {
            alert("修改出错！");
            return;
        }
        //var JsonResult = $.parseJSON(data);
        //alert(data["CourseName"]);
        $("#txtCourseName").val(data["CourseName"]);
        $("#txtCourseNum").val(data["CourseNum"]);

        if (data["CourseType"] == "必修课") {
            $("#courseType").val(1);
        } else if (data["CourseType"] == "选修课") {
            $("#courseType").val(2);
        } else if (data["CourseType"] == "通识课") {
            $("#courseType").val(3);
        } else {
            $("#courseType").val(0);
        }

        if (data["CheckWay"] == "考试") {
            $("#checkType").val(1);
        } else if (data["CheckWay"] == "考察") {
            $("#checkType").val(2);
        } else if (data["CheckWay"] == "其他") {
            $("#checkType").val(3);
        } else {
            $("#checkType").val(0);
        }
        
        $("#txtCredit").val(data["Credit"]);
        $("#txtCount").val(data["Count"]);
        $("#txtClassRoom").val(data["ClassRoom"]);

        $("#txtBeginClassYear").val(data["BeginClassYear"]);
        $("#txtEndClassYear").val(data["EndClassYear"]);

        $("#team").val(parseInt(data["Team"]));
        $("#beginWeek").val(parseInt(data["BeginWeek"]));
        $("#endWeek").val(parseInt(data["EndWeek"]));
        $("#classDay").val(parseInt(data["ClassDay"]));

        $("#beginClassNumber").val(parseInt(data["BeginClassNumber"]));
        $("#endClassNumber").val(parseInt(data["EndClassNumber"]));

        $("#beginSelectYear").val(data["BeginSelectYear"]);
        $("#beginSelectMonth").val(data["BeginSelecMonth"]);
        $("#beginSelectDay").val(data["BeginSelectDay"]);
        $("#beginSelectHour").val(data["BeginSelectHour"]);
        $("#beginSelectMinute").val(data["BeginSelectMinute"]);

        $("#endSelectYear").val(data["EndSelectYear"]);
        $("#endSelectMonth").val(data["EndSelectMonth"]);
        $("#endSelectDay").val(data["EndSelecDay"]);
        $("#endSelectHour").val(data["EndSelectHour"]);
        $("#endSelectMinute").val(data["EndSelectMinute"]);

        $("#txtRemark").val(data["Remark"]);

    });

    //$("#txtCourseName").val()


}

$("#courseInfoBackBtn").bind("click", BackBtnClick);

function BackBtnClick() {
    $("#myDiv3").removeClass("myHide");
    $("#myDiv3_2").addClass("myHide");
}

//保存按钮的点击事件
$("#courseInfoSaveBtn").bind("click", { "Action": "Save"}, SubBtnClick);

//--------------------------------教师修改课程信息（结束）------------------------------------------------------------//

