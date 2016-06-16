
//①查看历史开班信息
function GetOldClassInfo(courseNum) {
    //alert(courseNum);
    $("#MyCourseInfoTable").addClass("myHide");
    $("#MyClassInfo").removeClass("myHide");

    //发送请求，获取学生的个人数据
    GetStuInfo(courseNum);
   
}

//①-② 发送请求方法

function GetStuInfo(courseNum) {
    var pcourseNum = "'" + courseNum + "'";
    $.post("/UserInfo/GetStudentInfoInSelectClass", { "courseNum": courseNum }, function (data) {//发送异步请求，获取学生信息
        $("#t3 tr:gt(0)").remove();//清空学生信息表
        var count = data["total"];//读取学生人数

        if (count <= 0) {
            $("#t3").append("<tr class='myOtherTr ' align = 'left'><td colspan = '8'>暂无学生信息</td> </tr>");//如果返回小于0，表示无学生信息
            return;
        }
        for (var i = 0; i < count; i++) {//循环添加学生个人信息
            var str = "<a href=javascript:void(0); onclick=EditScore(" + data['Row'][i].StuNum + ",'" + data['Row'][i].StuName + "',"  + "'" + courseNum + "'" + ")>成绩编辑</a>";

            if (i % 2 == 0) {
                $("#t3").append("<tr class='myOtherTr '><td>" + (i + 1) + "</td><td>" + data['Row'][i].StuNum + "</td><td>" + data['Row'][i].StuName + "</td><td>" + data['Row'][i].Sex + "</td> <td>" + data['Row'][i].Major + "</td><td>" + data['Row'][i].Phone + "</td><td>" + data['Row'][i].Email + "</td> <td>"+str+"</td> </tr>");
            } else {
                $("#t3").append("<tr class='myOtherTr myTrBackgroudcolor'><td>" + (i + 1) + "</td><td>" + data['Row'][i].StuNum + "</td><td>" + data['Row'][i].StuName + "</td><td>" + data['Row'][i].Sex + "</td> <td>" + data['Row'][i].Major + "</td><td>" + data['Row'][i].Phone + "</td><td>" + data['Row'][i].Email + "</td> <td>"+str+"</td> </tr>");
            }

        }

    });
}


//②查看最新的开班信息
function GetNewClassInfo(courseNum) {
    $("#MyCourseInfoTable").addClass("myHide");
    $("#MyClassInfo").removeClass("myHide");

    //发送请求，获取学生的个人数据
    GetStuInfo(courseNum);
}


//③返回按钮1点击事件
function StudentMangage_BackBtnClick() {
    $("#MyCourseInfoTable").removeClass("myHide");
    $("#MyClassInfo").addClass("myHide");
}

//④返回按钮2点击事件
function StudentMangage_BackBtnClick2() {
    $("#myEditStuScore").addClass("myHide");
    $("#MyClassInfo").removeClass("myHide");
}


//⑤单个学生成绩编辑
function EditScore(stuNum, stuName, courseNum) {
    //alert("Edit");
    //$("#t3").addClass("myHide");
    $("#myEditStuScore").removeClass("myHide");
    $("#MyClassInfo").addClass("myHide");
    
    //清空表数据
    $("#t4 tr:gt(0)").remove();

    //加载学生成绩信息
    $.post("/UserInfo/TGetStuScoreInfo", { "stuNum": stuNum, "courseNum": courseNum }, function (result) {
        //alert(result["Grade1"]);
        var i = 1;
        var str = "<a href=javascript:void(0); onclick=SaveScoreBtnClick('" + stuNum + "','" + courseNum  + "')>保存</a>";

        //$("#t4").append("<tr class='myOtherTr '><td>" + i + "</td><td>" + stuNum + "</td><td>" + stuName + "</td><td>" + result["Grade1"] + "</td> <td>" + result["Grade2"] + "</td><td>" + result["Grade3"] + "</td><td>" + result["Grade4"] + "</td> <td>" + result["Grade"] + "</td><td>" + "保存" + "</td> </tr>");
        $("#t4").append("<tr class='myOtherTr '><td>" + i + "</td><td>" + stuNum + "</td><td>" + stuName + "</td><td>" + "<input type='text' maxlength='2' class='mySetScoreTextBox myInput2' id='Grade1' value =  '" + result["Grade1"] + "'</td> <td>" + "<input type='text' maxlength='2' class='mySetScoreTextBox myInput2' id='Grade2' value =  '" + result["Grade2"] + "'</td><td>" + "<input type='text' maxlength='2' class='mySetScoreTextBox myInput2' id='Grade3' value =  '" + result["Grade3"] + "'</td><td>" + "<input type='text' maxlength='2' class='mySetScoreTextBox myInput2' id='Grade4' value =  '" + result["Grade4"] + "'</td> <td>" + " <input type='text'  maxlength='2' class='mySetScoreTextBox myInput2' id='Grade' value =  '" + result["Grade"] + "'</td><td>" + str  + "</td> </tr>");
    });
}

//⑥执行保存按钮的点击事件
function SaveScoreBtnClick(stuNum,courseNum) {
   

    //先进行验证数据，然后提交成绩
    if (CheckIsNumber($("#Grade1").val())) {
        if (CheckIsNumber($("#Grade2").val())) {
            if (CheckIsNumber($("#Grade3").val())) {
                if (CheckIsNumber($("#Grade4").val())) {
                    if (CheckIsNumber($("#Grade").val())) {
                        //提交成绩信息
                        $.post("/UserInfo/TSvaeStuScore", { "stuNum": stuNum, "courseNum": courseNum, "grade1": $("#Grade1").val(), "grade2": $("#Grade2").val(), grade3: $("#Grade3").val(), "grade4": $("#Grade4").val(), "grade": $("#Grade").val() }, function (result) {
                            //alert(result);
                            if (result == "Success") {
                                alert("保存成功！");
                            } else {
                                alert("编辑成绩错误，请重试！");
                            }
                        });
                    } else {
                        alert("总评成绩项格式错误，请输入【0-99】的成绩！");
                    }
                } else {
                    alert("期末成绩项格式错误，请输入【0-99】的成绩！");
                }
            } else {
                aler("实验成绩项格式错误，请输入【0-99】的成绩！");
            }
        } else {
            aler("期中成绩项格式错误，请输入【0-99】的成绩！");
        }
    } else {
        alert("平时成绩项格式错误，请输入【0-99】的成绩！");
    }

       // 
}

//检出是否为正整数
function CheckIsNumber(number) {
    //var t = /^\d+(\.\d+)?$/;
    var t = /^[0-9]*[1-9][0-9]*$/;
    return t.test(number);
}