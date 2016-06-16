
//Adinm ------------------- 用户管理专用JS
$(function () {
    $('[data-toggle="tooltip"]').tooltip({ html: true });
    LoadRegisterUserInfo(1,10,0,0);//加载注册用户信息
});

//SaveSearchWay = "";
//SaveCondition = "";

/// <summary>
/// 加载注册【用户信息】
/// </summary>
/// <param name="pageIndex">页号</param>
/// <param name="pageSize">显示条数</param>
/// <param name="searchWay">搜索方式</param>
/// <param name="condition">条件</param>
/// <returns></returns>
function LoadRegisterUserInfo(pageIndex, pageSize,searchWay,condition) {
    //alert(searchWay + "____" + condition);
    //alert(pageIndex + "_______" + pageSize);
    //【条件筛选】初始化数据
    var selectCondition = 0;
    if (searchWay == "1") {//【按照角色查找】
        condition = $("#search_role").val();
    }
    if (searchWay == "2") {//【按账号查找】
        if ($("#search_accountNumber").val() != "") {
            condition = 0;
            selectCondition = $("#search_accountNumber").val();
        }
        else {
            alert("请填写需要查询的账号！");
            return;
        }
    }
    if (searchWay == "3") {//【按照姓名查找】
        if ($("#search_name").val() != "") {
            condition = 0;
            selectCondition = $("#search_name").val();
        } else {
            alert("请填写需要查询的姓名！");
            return;
        }
    }


    //①先清空原来的表格
    $("#RegisterUserInfoTable tr:gt(0)").remove();

    var k = 0;
    //②发生请求
    $.post("/Admin/GetAllRegisterUserInfo", { "pageIndex": pageIndex, "pageSize": pageSize, "searchWay": searchWay, "condition": condition ,"selectCondition":selectCondition}, function (data) {
        //alert(result["Grade1"]);
        //alert("Total" + data["Total"]);
        
        if (data["Total"] <= 0) {
            $("#RegisterUserInfoTable").append("<tr class=' mytr2-1' align = 'left'><td colspan = '7'>暂无学生信息</td> </tr>");
        }
        //var str = "<a href=javascript:void(0); onclick=SaveScoreBtnClick('" + stuNum + "','" + courseNum + "')>保存</a>";
        //alert(searchWay + "____" + condition);
      
        for (var i = 0; i < data["Total"]; i++) {
         
            //拼接删除链接字符串
            var str1 = "<a href=javascript:void(0); onclick=DeleteRegisterUser('" + data['Row'][i].UserNum + "','" + data['Row'][i].Role + "'," + data["Row"][i].ListID + ")>删除</a>"

            //拼接通过连接字符串
            var str2 = "<a href=javascript:void(0); onclick=PassRegisterUser('" + data['Row'][i].UserNum + "','" + data['Row'][i].Role + "'," + data["Row"][i].ListID + ")>通过</a>"

            if (k == 0) {//处理隔行不同样式展示
                $("#RegisterUserInfoTable").append("<tr class='mytr2 mytr2-1' id= "+data["Row"][i].ListID+"><td>" + data["Row"][i].ListID + "</td><td>" + data["Row"][i].UserNum + "</td><td>" + data["Row"][i].UserName + "</td><td>" + data["Row"][i].Sex + "</td><td>" + data["Row"][i].IdentityCard + "</td><td>" + data["Row"][i].Role + "</td><td>"+str1+" | "+str2+"</td></tr>");
                k = 1;
            }
            else {
                $("#RegisterUserInfoTable").append("<tr class=' mytr2-1' id=" + data["Row"][i].ListID + "><td>" + data["Row"][i].ListID + "</td><td>" + data["Row"][i].UserNum + "</td><td>" + data["Row"][i].UserName + "</td><td>" + data["Row"][i].Sex + "</td><td>" + data["Row"][i].IdentityCard + "</td><td>" + data["Row"][i].Role + "</td><td>" + str1 + " | " + str2 + "</td></tr>");
                k = 0;
            }
        }
        
        $(".myShowPage").html("");
        $(".myShowPage").html(data["PageNumber"]);//填充处理数据

    });

}

//【删除】
function DeleteRegisterUser(UserNum, Role, trId) {
    if (confirm("你确定删除该用户么？")) {
        $.post("/Admin/DeletAndPassRegisterUserInfo", { "method": "Delete", "userNum": UserNum, "role": Role }, function (data) {
            if (data == "Success") {
                $("#"+trId).remove();
            }
            else if (data == "Error") {
                alert("操作失败，请重试！");
            }
        });
    }
}

//【通过】
function PassRegisterUser(UserNum, Role,trId) {
    $.post("/Admin/DeletAndPassRegisterUserInfo", { "method": "Pass", "userNum": UserNum, "role": Role }, function (data) {
        if (data == "Success") {
            $("#" + trId).remove();
        }
        else if (data == "Error") {
            alert("操作失败，请重试！");
        }
    });
}





//----------------------------------------------------------
//【状态管理】
function LoadUserState() {
    
}

