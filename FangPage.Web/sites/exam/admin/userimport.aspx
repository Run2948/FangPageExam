<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>导入用户</title>
    <link rel="stylesheet" type="text/css" href="${adminpath}/css/admin.css"/>
    <link href="${adminpath}/css/datagrid.css" rel="stylesheet" type="text/css"/>
    <link href="${adminpath}/css/tab.css" rel="stylesheet" type="text/css"/>
    <%plugin(jquery)%>
    <script type="text/javascript" src="../statics/js/jquery.form.js"></script>
    <script type="text/javascript" src="${adminpath}/js/admin.js"></script>
    <script type="text/javascript">
        $(function () {
            var h = $(window).height()-90;
            $("#table").height(h);
            var index = parent.layer.getFrameIndex(window.name);
            $("#btnclose").click(function () {
                parent.layer.close(index);
            });
            $("#btnuserok").click(function () {
                var options = {
                    url: 'userimportajax.aspx', 
                    type: 'POST',
                    dataType: "json",
                    success: function (data) {
                        parent.$("#examusername").html(data.uname);
                        parent.$('#examuser').val(data.examuser);
                        parent.layer.close(index);
                    } 
                };
                $('#frmpost').ajaxSubmit(options);
            })
        });
    </script>
</head>
<body>
    <form method="post" action="" name="frmpost" id="frmpost">
    <input id="examuser" name="examuser" value="${examuser}" type="hidden" />
    <div id="table" class="newslistabout">
      <table cellpadding="0" cellspacing="0" border="0" style="width: 397px;height:140px; margin: 0px;">
        <tr>
            <td  colspan="3" style="border: solid 1px #93C7D4; vertical-align:middle;height:100px;padding-left:10px;">
                <input id="uploadfile" name="uploadfile" type="file" />&nbsp<a href="images/examuser.xls">导入模板下载</a>
            </td>
        </tr>
        <tr>
            <td colspan="3" height="25" align="right">
               <input type="button" name="btnuserok" value="确定" id="btnuserok" class="adminsubmit_short"/>
               <input type="button" name="btnclose" value="关闭" id="btnclose" class="adminsubmit_short"/>
            </td>
        </tr>
      </table>
    </div>
    </form>
</body>
</html>
