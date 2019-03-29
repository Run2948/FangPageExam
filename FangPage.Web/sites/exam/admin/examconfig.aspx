<%using(FangPage.WMS.Model) %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考试系统配置</title>
    <link href="${adminpath}/css/admin.css" rel="stylesheet" type="text/css"/>
    <%plugin(jquery) %>
    <%plugin(layer) %>
    <script type="text/javascript">
        $(function () {
            $("#btnsave").click(function () {
                $("#action").val("save")
                $("#frmpost").submit();
            });
            $("#btnreset").click(function () {
                $("#action").val("reset")
                $("#frmpost").submit();
            });
            $("#btnreset").click(function () {
                if (confirm("您确定要清除题库吗?")) {
                    $("#action").val("clear")
                    $("#frmpost").submit();
                }
            });
        });
    </script>
</head>
<body>
  <form name="frmpost" method="post" action="" id="frmpost">
    <input id="action" name="action" value="" type="hidden" />
    <div class="newslistabout">
      <table class="borderkuang" border="0" cellspacing="0" cellpadding="0" width="100%">
      <tbody>
        <tr>
          <td class="newstitle" bgcolor="#ffffff">考试系统配置</td>
        </tr>
      </tbody>
     </table>
      <table style="width:100%;" cellpadding="2" cellspacing="1" class="border">
        <tbody>
        <tr>
            <td class="td_class"> 自动保存答案时间： </td>
            <td><input name="autotime" type="text" value="${examconfiginfo.autotime}" id="autotime" style="height:21px;width:200px;"/>&nbsp;分钟，0为不自动保存。</td>
        </tr>
        <tr>
            <td class="td_class"> 练习最大题数： </td>
            <td><input name="testcount" type="text" value="${examconfiginfo.testcount}" id="testcount" style="height:21px;width:200px;"/>&nbsp;道，必须大于0。</td>
        </tr>
        <tr>
            <td class="td_class"> 练习时间： </td>
            <td><input name="testtime" type="text" value="${examconfiginfo.testtime}" id="testtime" style="height:21px;width:200px;"/>&nbsp;分钟，必须大于0。</td>
        </tr>
        <tr>
            <td class="td_class"> 允许查看考试答案： </td>
            <td>
                <input id="showanswer" name="showanswer" value="1" <%if examconfiginfo.showanswer==1 %> checked="checked" <%/if %> type="checkbox" />是/否允许用户查看考试历史答案
            </td>
        </tr>
        <tr>
            <td class="td_class"> 开启关闭用户练习： </td>
            <td>
                <input id="teststatus" name="teststatus" value="1" <%if examconfiginfo.teststatus==1 %> checked="checked" <%/if %> type="radio" />开启
                <input id="teststatus" name="teststatus" value="0" <%if examconfiginfo.teststatus==0 %> checked="checked" <%/if %> type="radio" />关闭
            </td>
        </tr>
        <tr>
            <td class="td_class"> 使用题目类型： </td>
            <td>
                <input id="questiontype" name="questiontype" value="1" <%if ischecked(1,examconfiginfo.questiontype)%> checked="checked" <%/if %> type="checkbox" />单选题
                <input id="questiontype" name="questiontype" value="2" <%if ischecked(2,examconfiginfo.questiontype)%> checked="checked" <%/if %> type="checkbox" />多选题
                <input id="questiontype" name="questiontype" value="3" <%if ischecked(3,examconfiginfo.questiontype)%> checked="checked" <%/if %> type="checkbox" />判断题
                <input id="questiontype" name="questiontype" value="4" <%if ischecked(4,examconfiginfo.questiontype)%> checked="checked" <%/if %> type="checkbox" />填空题
                <input id="questiontype" name="questiontype" value="5" <%if ischecked(5,examconfiginfo.questiontype)%> checked="checked" <%/if %> type="checkbox" />问题题
                <input id="questiontype" name="questiontype" value="6" <%if ischecked(6,examconfiginfo.questiontype)%> checked="checked" <%/if %> type="checkbox" />打字题
            </td>
        </tr>
        <tr>
        <td class="td_class"></td>
        <td height="25">
            <input type="button" name="btnsave" value="保存配置" id="btnsave" class="adminsubmit_short"/>
            <input type="button" name="btnreset" value="重置题库统计" id="btnreset" class="adminsubmit_long"/>
            <input type="button" name="btnclear" value="清空题库" id="btnclear" class="adminsubmit_long"/>
        </td>
        </tr>
      </tbody>
      </table>
    </div>
</form>
<%if ispost %>
   <script type="text/javascript">
       layer.msg('${msg}', 2, 1);
    </script>
<%/if %>
</body>
</html>
