$(function () {
    //$(".btn-lg").click();
    ////setInterval(Timer,1000);
    $('[data-toggle="tooltip"]').tooltip({ html: true });
    $('[data-toggle="tooltip"]').tooltip();
    var t1_length = $("#t1 tr").length;
    var t2_length = $("#t2 tr").length;
    var t_length = t1_length + t2_length;

    if (t_length > 15) {
        $(".score_main_content").css("height", (t_length)*40+"px");
    }
});


$(".main_content_left_1").bind("mouseover", {}, function () {
    $(this).css("background-color", "#FFFF99");
});

$(".main_content_left_1").bind("mouseout", {}, function () {
    $(this).css("background-color", "#CCCCFF");
});

function cancelBtn() {
    $("#txtOldPwd").val ("");
    $("#txtNewPwd1").val("");
    $("#txtNewPwd2").val("");

}


function subBtn(){
    //$("#UpdatePwdForm");

    if ($("#txtOldPwd").val() != "") {
        if($("#txtNewPwd1").val() !=""){
            if($("#txtNewPwd2").val() != ""){
                if ($("#txtNewPwd1").val() == $("#txtNewPwd2").val()) {
                    $.post("/UserInfo/UpdateUserPwd", {"oldPwd":$("#txtOldPwd").val(), "newPwd1":$("#txtNewPwd1").val(), "newPwd2":$("#txtNewPwd2").val() }, function(returnStr){
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
            else{
                alert("请再次输入新密码！");
            }
        }
        else{
            alert("请输入新密码！")
        }
    }
    else {
        alert("请输入旧密码！");
    }
}



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

//function ScoreInfoLinkClick() {
//    $(".main_content_right_0").html("&nbsp;&nbsp;&nbsp;&nbsp;当前位置：学生 -----> 成绩信息查询 ");
//    $(".main_content_right_content_title").html("成绩信息查询");
//    //设置左导航条
//    $("#mynavLeft1").html(" &nbsp; <a href='javascript:void(0);' onclick='ShowScoreInfo()'>成绩信息查询</a>");
//    $("#mynavLeft2").html(" &nbsp; <a href='javascript:void(0);' onclick='ShowScoreInfo()'>成绩信息查询</a>");
    
//    //设置头部导航条
//    $("#mydiv1").html("");
//    $("#mynavL2").addClass("active");
//    $("#mynavL1").removeClass("active");
//    $("#mynavL1").html("<a href='/UserInfo/Student' >个人信息</a>");

//    //$("#mydiv1").html("<div><span>学年</span><span>学期</span><span>课程名称</span><span>课程类型</span><span>任课教师</span><span>考核方式</span><span>平时成绩</span><span>期中成绩</span><span>实验成绩</span><span>期末成绩</span><span>总评成绩</span><span>补考成绩</span><span>重修成绩</span><span>应得学分</span><span>学分绩点</span></div>");
//    $("#mydiv1").html("<table></table>");
//}


//function ShowScoreInfo() {

//}
//var i = 0;
//function Timer() {
//    $("#Timer").text("运行页面 "+i + " 秒");
//    i++;
//}

function delLink(courseNum) {
    if (confirm("确定要退选吗？"))
    {
        $.post("/UserInfo/DeleteSelectClass", { "courseNum": courseNum }, function (str) {
            if (str == "Success") {
                window.location.href = "/UserInfo/ShowSelectClassInfo";
            } else if (str == "Error") {
                alert("退选失败！");
            }
        });
    }
}

function myHelpLink() {
    $(".btn-lg").click();
}

function btnSelectClass() {

    //var row1 = {};
    //row1.name = 'david';
    //row1.age = '20';

    ////或者
    //var row2 = { name: 'peter', age: '23' };

    //var data = [];
    //data.push(row1);
    //data.push(row2);

    ////此时data的json字符串为 [{name:'david',age:'20'},{name:'peter',age'23'}]
    ////如果后台是PHP，将得到
    ///*
    //print_r($_GET);
    //Array(0=>array('name' => 'david'....
    //*/

    //但是建议采用如下的形式
    //var data = { records: [] };
    //data.records.push(row1);
    //data.records.push(row2);
    ////此时data的json字符串为 {records:[{name:'david',age:'20'},{name:'peter',age'23'}]}



    
    //var row = {};
    //var data = { records: [] };
    //var d1 = 
    $('#t2').find(':checkbox').each(function () {
        if ($(this).is(":checked")) {
            //alert("checked12312312");
            //alert($(this).attr("id") + "   " + $(this).attr("tNum"));
            //row = { courseNum: $(this).attr("id"), tNum: $(this).attr("tNum") };
            //data.records.push(row);

            //alert( "courseNum:"+ $(this).attr("id")+"   "+ "tNum:"+ $(this).attr("tNum"));
            $.post("/UserInfo/InsertSelectClass", { courseNum: $(this).attr("id"), teacherNum: $(this).attr("tNum") }, function (ret) {
                //alert(ret);
                if (ret == "Success") {
                    alert("选课成功！");
                    window.location.href = "ShowSelectClassInfo";

                } else if (ret == "Error1") {
                    alert("课程已经选过了，请不要重复选！");
                } else {
                    alert("选课失败，请重试！");
                }
            });
        }
    });

   
    //window.location.href = "ShowSelectClassInfo";


    //for (var i = 0; i < data.records.length; i++)
    //{
    //    alert(data.records[i].courseNum + "  " + data.records[i].tNum);
    //}
    //$("input[type=checkbox]").each(function () {
        
    //    if ($(this).attr("checked") && $(this).attr("checked") == "checked") {
    //        alert("选中了" + $(this).attr("tNum"));

    //    }
    //    if ($(this).attr("checked") == true) {
    //        alert("checkbook");
    //    }

    //    if ($(this).checked == true) {
    //        alert("checkbook");
    //    }

    //    //未选中

    //});
}


function stuInfoEditClick() {
    //alert("sadasd");
    if ($("#StuEditTable").hasClass("myHide")) {
        $("#StuEditTable").removeClass("myHide");
    } else {
        $("#StuEditTable").addClass("myHide");
    }
}

function StuSaveClick() {
    if ($("#editPhone").val() != "" && CheckPhone($("#editPhone").val()) == 1) {
        if ($("#editEmail").val() != "" && CheckEmail($("#editEmail").val()) == 1) {
            //alert("验证完成，准备上传：" + $("#editPhone").val() + "___" + $("#editEmail").val());

            $.post("/UserInfo/UpdateStuInfo", { "phone": $("#editPhone").val(),"email":$("#editEmail").val() }, function (data) {
                if (data == "success") {
                    //$("#stuSaveMsg").removeClass("myError");
                    //$("#stuSaveMsg").addClass("mySuccess");   
                    //$("#stuSaveMsg").text("修改成功！");
                    alert("修改成功！");
                    window.location.href = "/UserInfo/Student";
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
    var phoneReg = /^(13+\d{9})|(147+\d{8})|(150+\d{8})|(151+\d{8})|(152+\d{8})|(153+\d{8})|(155+\d{8})|(156+\d{8})|(157+\d{8})|(158+\d{8})(159+\d{8})|(180+\d{8})|(182+\d{8})|(185+\d{8})|(186+\d{8})|(187+\d{8})|(188+\d{8})|(189+\d{8})$/;

    if (!phoneReg.test(phone) || phone.length != 11) {
        return -1;
    }
    return 1;
}




