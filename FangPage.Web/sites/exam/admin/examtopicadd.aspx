<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>添加编辑试卷大题</title>
    <link href="${adminpath}/css/admin.css" rel="stylesheet" type="text/css"/>
    <%plugin(jquery)%>
    <script type="text/javascript" src="${adminpath}/js/admin.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnback").click(function () {
                window.location.href = "examtopicmanage.aspx?examid=${examid}&paper=${paper}";
            })
            <%set (string){navurl}="exammanage.aspx"%>
            PageNav("${GetSortNav(sortinfo,navurl)}|试题设置,${rawpath}/examtopicmanage.aspx?examid=${examid}&paper=${paper}|添加编辑大题,${rawurl}");
        })
    </script>
</head>
<body>
  <form name="frmpost" method="post" action="" id="frmpost">
    <div class="newslistabout">
      <table class="borderkuang" border="0" cellspacing="0" cellpadding="0" width="100%">
      <tbody>
        <tr>
          <td class="newstitle" bgcolor="#ffffff">添加编辑试卷大题，试卷名称：${examinfo.name}${GetPaper(paper)}</td>
        </tr>
      </tbody>
     </table>
      <table style="width:100%;" cellpadding="2" cellspacing="1" class="border">
        <tbody>
            <tr>
            <td class="td_class"> 大题题型： </td>
            <td>
            <select id="type" name="type" onchange="ShowTopic()">
                <%if ischecked(1,examconfig.questiontype) %>
                <option value="1" <%if examtopic.type==1 %> selected="selected" <%/if %> >单选题</option>
                <%/if %>
                <%if ischecked(2,examconfig.questiontype) %>
                <option value="2" <%if examtopic.type==2 %> selected="selected" <%/if %> >多选题</option>
                <%/if %>
                <%if ischecked(3,examconfig.questiontype) %>
                <option value="3" <%if examtopic.type==3 %> selected="selected" <%/if %> >判断题</option>
                <%/if %>
                <%if ischecked(4,examconfig.questiontype) %>
                <option value="4" <%if examtopic.type==4 %> selected="selected" <%/if %> >填空题</option>
                <%/if %>
                <%if ischecked(5,examconfig.questiontype) %>
                <option value="5" <%if examtopic.type==5 %> selected="selected" <%/if %> >问答题</option>
                <%/if %>
                <%if ischecked(6,examconfig.questiontype) %>
                <option value="6" <%if examtopic.type==6 %> selected="selected" <%/if %> >打字题</option>
                <%/if %>
            </select>
            </td>
            </tr>
            <tr>
            <td class="td_class"> 大题标题： </td>
            <td><input name="title" type="text" value="${examtopic.title}" id="title" style="height:21px;width:400px;"/></td>
            </tr>
            <tr>
            <td class="td_class"> 题目总数： </td>
            <td><input name="questions" type="text" value="${examtopic.questions}" id="questions" style="height:21px;width:400px;"/></td>
            </tr>
            <tr>
            <td class="td_class"> 每题分值： </td>
            <td><input name="perscore" type="text" value="${examtopic.perscore}" id="perscore" style="height:21px;width:400px;"/></td>
            </tr>
            <tr>
            <td class="td_class"> 排序顺序： </td>
            <td><input name="display" type="text" value="${examtopic.display}" id="display" style="height:21px;width:400px;"/></td>
            </tr>
            <tr>
            <td class="td_class"></td>
            <td height="25">
            <input type="submit" name="btnsave" value="保存" id="btnsave" class="adminsubmit_short"/>
            <input type="button" name="btnback" value="返回" id="btnback" class="adminsubmit_short"/>
            </td>
            </tr>
        </tbody>
      </table>
    </div>
</form>
</body>
</html>
