<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Excel导入题目</title>
<link href="${adminpath}/css/admin.css" rel="stylesheet" type="text/css" />
<%plugin(jquery)%>
<script type="text/javascript" src="${adminpath}/js/admin.js"></script>
<script type="text/javascript">
    $(function () {
        $("#btncancle").click(function () {
            window.location.href = "questionmanage.aspx?sortid=${sortid}";
        })
        PageNav("考试题库管理,${rawpath}/questionmanage.aspx?sortid=${sortid}|${sortinfo.name},${rawpath}/questionmanage.aspx?sortid=${sortid}|Excel导入题目,${rawurl}");
    });
</script>
</head>
<body>
  <form name="frmpost" method="post" action="" id="frmpost" enctype="multipart/form-data">
    <div class="newslistabout">
      <table class="borderkuang" border="0" cellspacing="0" cellpadding="0" width="100%">
      <tbody>
        <tr>
          <td class="newstitle" bgcolor="#ffffff">Excel导入题目，所在题库：${sortinfo.name}</td>
        </tr>
      </tbody>
     </table>
      <table style="width:100%;" cellpadding="2" cellspacing="1" class="border">
        <tbody>
          <tr>
            <td class="tdbg">
             <table cellspacing="0" cellpadding="3" width="100%" border="0">
                <tbody>
                  <tr>
                    <td class="td_class">本地Excel题库文件： </td>
                    <td>
                       <input id="uploadfile" name="uploadfile" type="file" />
                       <a href="images/import.xls">下载题目录入格式表</a>
                    </td>
                  </tr>
                  <tr>
                    <td class="td_class"></td>
                    <td height="25">
                    <input type="submit" name="btnsave" value="确定" id="btnsave" class="adminsubmit_short"/>
                    <input id="btncancle" class="adminsubmit_short" name="btncancle" value="返回" type="button"/>
                    </td>
                  </tr>
                </tbody>
              </table>
              </td>
          </tr>
        </tbody>
      </table>
    </div>
</form>
</body>
</html>
