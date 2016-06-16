
$("#btnLogin").click(SubLogin);

function SubLogin() {
    if ($("#username").val() != "") {
        if ($("#password").val() != "") {
            if ($("#roleSelect").val() != 0) {
                //可以提交登录验证
                //$("#form1").submit();

                //暂时不对游客开放
                if ($("#roleSelect").val() == 1) {
                    alert("对不起，本系统暂时不对游客开放，请登录！");
                    return;
                }

                $.post("/Home/CheckLogin", { "username": $("#username").val(), "password": $("#password").val(), "usertype": $("#roleSelect").val() }, function (returnString) {
                    if (returnString == "Student_Success") {
                        window.location.href = "/UserInfo/Student";
                    }
                    else if(returnString == "Teacher_Success"){
                        window.location.href = "/UserInfo/Teacher";
                    }
                    else if (returnString == "Admin_Success") {
                        window.location.href = "/Admin/Index";
                    }
                    else if (returnString == "Error1") {
                        //$("#password").val() = "";
                        alert("用户名或者密码错误！");
                        return;
                    } else if (returnString == "Error2") {
                        alert("该用户被禁用，请与管理员联系！");
                        return;
                    }
                    else if (returnString == "Error4") {
                        alert("该账号正在检验中，请与管理员联系！");
                        return;
                    }
                    else {//登录出错
                         window.location.href = "/Home/Error404";
                        //alert(returnString);
                    }
                });
            } else {
                alert("请选择用户角色！");
            }
        } else {
            alert("密码不能为空！");
        }
    } else {
        alert("用户名不能为空!");
    }
}

//取消按钮的 onmuserover 事件
$("#spanCancel").mouseover(function () {
    $(this).addClass("myLoginSpanBtnHover");
});

//取消按钮的 onmuserout 事件
$("#spanCancel").mouseout(function () {
    $(this).removeClass("myLoginSpanBtnHover");
});

//取消按钮的 onclick 事件
$("#spanCancel").click(function () {
      window.opener = null;
      window.open('', '_self');
      window.close();
});


$(function () {
    //登录按下回车键
    $(document).keyup(function (e) {
        var key = e.which;
        if (key == 13) {
            //alert("按下回车键！");
            SubLogin();
        }
    });
});
//$("#aCancel").click(function () {
//    //alert("你点击了a标签");
//    this.window.opener = null;
//    window.close();
//});




