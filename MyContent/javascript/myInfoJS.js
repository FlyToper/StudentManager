
$(function () {
    BindBtnClick("All");
});



$(".mybutton").mouseover(function () {
    $(this).addClass("mybuttonhover");
});

$(".mybutton").mouseout(function () {
    $(this).removeClass("mybuttonhover");
});

$(".myEditBtn").mouseover(function () {
    $(this).addClass("myEditBtnhover");
});

$(".myEditBtn").mouseout(function () {
    $(this).removeClass("myEditBtnhover");
});




//---------------------------教师个人信息页面新内容开始-----------------------------------------------------------//

//①为每个修改按钮绑定点击事件
function BindBtnClick(datatype) {
    if (datatype == "All") {
        var strs3 = new Array();
        strs3 = ["TeacherName", "Sex", "IdentityCard", "Education", "Position", "Major", "InstituteName", "Email", "Phone", "CourseName1", "CourseName2", "CourseName3"];
        for (var i = 0 ; i < 12; i++) {
            $("#"+strs3[i]+"Btn").bind("click", { "Type": strs3[i] }, PublicBtnClick);
        }

    }

    $("#" + datatype + "Btn").bind("click", { "Type": datatype }, PublicBtnClick);

    //$("#TeacherNameBtn").bind("click", { "Type": "TeacherName" }, PublicBtnClick);
    //$("#SexBtn").bind("click", { "Type": "Sex" }, PublicBtnClick);
    //$("#IdentityCardBtn").bind("click", { "Type": "IdentityCard" }, PublicBtnClick);
    //$("#EducationBtn").bind("click", { "Type": "Education" }, PublicBtnClick);
    //$("#PositionBtn").bind("click", { "Type": "Position" }, PublicBtnClick);
    //$("#MajorBtn").bind("click", { "Type": "Major" }, PublicBtnClick);
    //$("#InstituteNameBtn").bind("click", { "Type": "InstituteName" }, PublicBtnClick);
    //$("#EmailBtn").bind("click", { "Type": "Email" }, PublicBtnClick);
    //$("#PhoneBtn").bind("click", { "Type": "Phone" }, PublicBtnClick);
    //$("#CourseName1Btn").bind("click", { "Type": "CourseName1" }, PublicBtnClick);
    //$("#CourseName2Btn").bind("click", { "Type": "CourseName2" }, PublicBtnClick);
    //$("#CourseName3Btn").bind("click", { "Type": "CourseName3" }, PublicBtnClick);
}


var type;
var timer1
//①按钮的点击事件
function PublicBtnClick(d) {
    //alert("公共点击事件" + d.data["Type"]);
     type = d.data["Type"];

    //if (type == "TeacherName") {
    //    $("#myinfo_Trip").text("对不起，此项暂时无法修改，请与管理员联系！");
    //     timer1 = setInterval(changeColor, "100");
    //     setTimeout(delTimer, 2000);

    //}

    if (type == "Email" || type == "Phone" || type == "CourseName1" || type == "CourseName2" || type == "CourseName3") {

        //确保文本框的值和数据库的值一样，防止误修改
        $("#" + type + "SaveText").val($("#" + type + "Tip").text());

        //清除原本的提示信息
        $("#" + type + "ErrorMsg").addClass("myhidedisplay");
        $("#" + type + "ErrorMsg").text("");
        $("#" + type + "ErrorMsg").removeClass("errorMsg");
     
        //文本框获取焦点
        //$("#EmailSaveText").onblur();

        if ($("#" + type + "Tip").hasClass("myhidedisplay")) {

            $("#" + type + "Tip").removeClass("myhidedisplay");
            $("#" + type + "HideText").addClass("myhidedisplay");

            //$("#EmailSaveText").focus();
        } else {
            $("#" + type + "Tip").addClass("myhidedisplay");
            $("#" + type + "HideText").removeClass("myhidedisplay");

            //仅仅当时邮箱的时候在获取焦点
            if (type == "Email") {
                $("#EmailSaveText").focus();
            }
            
        }
    } else {//其他的一些不可更改类别的
        //Text(type);

        $("#myinfo_Trip_" + type).text("对不起，此项暂时无法修改，请与管理员联系！");

        var str2 = new Array();
        str2 = ["TeacherName", "Sex", "IdentityCard", "Education", "Position", "Major", "InstituteName"];
        for (var i = 0; i < 7; i++) {
            if (type != str2[i]) {
                $("#" + type + "ErrorMsg").text("");
                $("#" + type + "ErrorMsg").removeClass("errorMsg");
                $("#" + type + "ErrorMsg").addClass("myhidedisplay");
            }
        }

        //setTimeout("delText(type)", 2000);
        timer1 = setInterval("changeColor(type)", 100);
        setTimeout("delTimer(type)", 1500);
    }

    //修改全部的触发事件
    if (type == "All") {
        var strs1 = new Array();
        //strs1 = ["TeacherName", "Sex", "IdentityCard", "Education", "Position", "Major", "InstituteName", "Email", "Phone", "CourseName1", "CourseName2", "CourseName3"];
        strs1 =  ["Email", "Phone", "CourseName1", "CourseName2", "CourseName3"];
        for (var i = 0; i < 5; i++) {
            $("#" + strs1[i] + "Btn").click();
        }
    }
}


//②修改全部按钮绑定事件
$("#allBtn").bind("click", { "Type": "All" }, PublicBtnClick);
$("#allBtn2").bind("click", { "Type": "All" }, PublicBtnClick);

//颜色改变的定时器，调用
function changeColor(type) {
    if ($("#myinfo_Trip_"+ type).hasClass("myWhiteColor")) {
        $("#myinfo_Trip_" + type).removeClass("myWhiteColor");
        $("#myinfo_Trip_" + type).addClass("errorMsg");
    }
    else {
        $("#myinfo_Trip_" + type).addClass("myWhiteColor");
        $("#myinfo_Trip_" + type).removeClass("errorMsg");
    }
}

//删除定时器
function delTimer(type) {
    clearInterval(timer1);
    $("#myinfo_Trip_" + type).text("");
}

function delText(type) {
    $("#myinfo_Trip_" + type).text("");
}

//④保存按钮绑定点击事件
$("#EmailSaveBtn").bind("click", { "Type": "Email" }, PublicSaveBtn);
$("#PhoneSaveBtn").bind("click", { "Type": "Phone" }, PublicSaveBtn);
$("#CourseName1SaveBtn").bind("click", { "Type": "CourseName1" }, PublicSaveBtn);
$("#CourseName2SaveBtn").bind("click", { "Type": "CourseName2" }, PublicSaveBtn);
$("#CourseName3SaveBtn").bind("click", { "Type": "CourseName3" }, PublicSaveBtn);


function PublicSaveBtn(d) {
    var type = d.data["Type"];

    if ($("#"+type+"SaveText").val() != "") {
        //上传更新数据
        //alert("123");
        if (type == "Email") {
            if (CheckEmail($("#EmailSaveText").val()) == -1) {
                return;
            }
        }else if (type == "Phone") {
            if (CheckPhone($("#PhoneSaveText").val()) == -1) {
                return;
        }
        } else {
            //alert($("#" + type + "SaveText").val().length + "__" + CheckLength($("#" + type + "SaveText").val(), 20));
            if (CheckLength($("#" + type + "SaveText").val(), 20) == -1) {
                $("#" + type + "ErrorMsg").removeClass("myhidedisplay");
                $("#" + type + "ErrorMsg").text(" * 请输入长度少于20的课程名 *");
                $("#" + type + "ErrorMsg").removeClass("successMsg");
                $("#" + type + "ErrorMsg").addClass("errorMsg");
                return;
            }
        }

        $.post("/UserInfo/UpdateUserInfo", {"usertype":"Teacher", "item":type, "data": $("#"+type+"SaveText").val() }, function (result) {
            if (result == "success") {
                $("#"+type+"ErrorMsg").removeClass("myhidedisplay");
                $("#"+type+"ErrorMsg").text(" * 保存成功 *");
                $("#"+type+"ErrorMsg").addClass("successMsg");
                $("#"+type+"ErrorMsg").removeClass("errorMsg");

                //setTimeout(TrueNameBtnClick, 2000);
                //setTimeout($("#" + type + "Btn").click(), 2000, {"Type":type});
                //setTimeout("PublicBtnClick({'Type':type})", 2000);
                text = $("#" + type + "SaveText").val();
                setTimeout("SaveBtnSuccess(type,text)", 1000);
            } else if (result == "error") {
                $("#"+type+"ErrorMsg").removeClass("myhidedisplay");
                $("#"+type+"ErrorMsg").text(" * 保存失败 *");
                $("#"+type+"ErrorMsg").addClass("errorMsg");
                $("#"+type+"ErrorMsg").removeClass("successMsg");
            }
        });

    } else {
        $("#"+type+"ErrorMsg").removeClass("myhidedisplay");
        $("#" + type + "ErrorMsg").text(" * 文本框不能为空 *");
        $("#" + type + "ErrorMsg").removeClass("successMsg");
        $("#" + type + "ErrorMsg").addClass("errorMsg");

    }
}

//保存成功之后的一些操作
function SaveBtnSuccess(d,text) {
    var type = d;
    $("#" + type + "Btn").click();

    $("#" + type + "Tip").text(text);

    //$("#" + type + "_showSet").text("");
    $("#" + type + "_showSet").html("<span><span class='successMsg'><span class='glyphicon glyphicon-ok-circle' aria-hidden='true'></span> 已设置 </span>| <input type='button' value='修改' class='myEditBtn' id='" + type + "Btn' /> </span>");

    //再次为新按钮绑定事件
    BindBtnClick(type);
}

//⑤编辑文本框的 绑定keydown 事件
$("#EmailSaveText").bind("keydown", { "Type": "Email" }, PublicTextKeydown);
$("#PhoneSaveText").bind("keydown", { "Type": "Phone" }, PublicTextKeydown);
$("#CourseName1SaveText").bind("keydown", { "Type": "CourseName1" }, PublicTextKeydown);
$("#CourseName2SaveText").bind("keydown", { "Type": "CourseName2" }, PublicTextKeydown);
$("#CourseName3SaveText").bind("keydown", { "Type": "CourseName3" }, PublicTextKeydown);

//文本框的公共KeyDown事件
function PublicTextKeydown(d) {
    var type = d.data["Type"];

    $("#"+type+"ErrorMsg").text("");
    $("#" + type + "ErrorMsg").removeClass("errorMsg");
    $("#" + type + "ErrorMsg").addClass("myhidedisplay");
}

//⑥文本框的焦点离开
$("#EmailSaveText").bind("blur", { "Type": "Email" }, PublicTextOnblur);
$("#PhoneSaveText").bind("blur", { "Type": "Phone" }, PublicTextOnblur);
$("#CourseName1SaveText").bind("blur", { "Type": "CourseName1" }, PublicTextOnblur);
$("#CourseName2SaveText").bind("blur", { "Type": "CourseName2" }, PublicTextOnblur);
$("#CourseName3SaveText").bind("blur", { "Type": "CourseName3" }, PublicTextOnblur);

//文本框公共blur事件
function PublicTextOnblur(d) {
    var type = d.data["Type"];

    if ($("#" + type + "SaveText").val() == "") {
        $("#" + type + "ErrorMsg").removeClass("myhidedisplay");
        $("#" + type + "ErrorMsg").text(" * 文本框不能为空 *");
        $("#" + type + "ErrorMsg").removeClass("successMsg");
        $("#" + type + "ErrorMsg").addClass("errorMsg");
    }

    if (type == "Email") {
        var email = $("#EmailSaveText").val();
        if (CheckEmail(email) == -1) {
            $("#" + type + "ErrorMsg").removeClass("myhidedisplay");
            $("#" + type + "ErrorMsg").text(" * 邮箱格式错误 *");
            $("#" + type + "ErrorMsg").removeClass("successMsg");
            $("#" + type + "ErrorMsg").addClass("errorMsg");
        }
        
    } else if (type == "Phone") {

        var phone = $("#PhoneSaveText").val();
        if (CheckPhone(phone) == -1) {
            $("#" + type + "ErrorMsg").removeClass("myhidedisplay");
            $("#" + type + "ErrorMsg").text(" * 联系电话格式错误 *");
            $("#" + type + "ErrorMsg").removeClass("successMsg");
            $("#" + type + "ErrorMsg").addClass("errorMsg");
        }
    } else {
        if (CheckLength($("#" + type + "SaveText").val(), 20) == -1) {
            $("#" + type + "ErrorMsg").removeClass("myhidedisplay");
            $("#" + type + "ErrorMsg").text(" * 请输入长度少于20的课程名 *");
            $("#" + type + "ErrorMsg").removeClass("successMsg");
            $("#" + type + "ErrorMsg").addClass("errorMsg");
        }
    }
}

//验证邮箱的方法
function  CheckEmail(email) {
    var strReg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;

    if (!strReg.test(email)) {
        //$("#" + type + "ErrorMsg").removeClass("myhidedisplay");
        //$("#" + type + "ErrorMsg").text(" * 邮箱格式错误 *");
        //$("#" + type + "ErrorMsg").removeClass("successMsg");
        //$("#" + type + "ErrorMsg").addClass("errorMsg");
        return -1;
    }
    return 1;
}

//验证联系电话的方式
function CheckPhone(phone) {
    var phoneReg = /^(13+\d{9})|(147+\d{8})|(150+\d{8})|(151+\d{8})|(152+\d{8})|(153+\d{8})|(155+\d{8})|(156+\d{8})|(157+\d{8})|(158+\d{8})(159+\d{8})|(180+\d{8})|(182+\d{8})|(185+\d{8})|(186+\d{8})|(187+\d{8})|(188+\d{8})|(189+\d{8})$/;

    if (!phoneReg.test(phone) || phone.length != 11) {
        $("#" + type + "ErrorMsg").removeClass("myhidedisplay");
        $("#" + type + "ErrorMsg").text(" * 联系电话格式错误 *");
        $("#" + type + "ErrorMsg").removeClass("successMsg");
        $("#" + type + "ErrorMsg").addClass("errorMsg");
        return -1;
    }
    return 1;
}

//验证长度是否超出预订的范围
function CheckLength(text, maxlength){
    if(text.length > maxlength){
        return -1;
    }
    return 1;
}

//-------------------------------教师个人信息页面新内容结束-------------------------------------------------------//




