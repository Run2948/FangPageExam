<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>考试搜索</title>
<link href="${adminpath}/css/admin.css" rel="stylesheet" type="text/css" />
<%plugin(jquery)%>
<%plugin(calendar)%>
<script type="text/javascript" src="${adminpath}/js/admin.js"></script>
<script type="text/javascript">
    $(function () {
        PageNav("${GetSortNav(sortinfo,pagename)}|考试搜索,${rawurl}");
        $("#btncancle").click(function () {
            PageBack("exammanage.aspx?sortid=${sortid}");
        })
    });
</script>
</head>
<body>
  <form name="frmpost" method="get" action="exammanage.aspx" id="frmpost">
     <input id="sortid" name="sortid" value="${sortid}" type="hidden" />
     <div class="newslistabout">
      <table class="borderkuang" border="0" cellspacing="0" cellpadding="0" width="100%">
      <tbody>
        <tr>
          <td class="newstitle" bgcolor="#ffffff">考试搜索:${sortinfo.name}</td>
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
                  <td class="td_class">考试名称： </td>
                  <td>
                      <input name="keyword" type="text" value="" id="keyword" style="height:21px;width:265px;"/>
                  </td>
                </tr>
                <tr>
                  <td class="td_class">考试时间： </td>
                  <td>
                       <input name="starttime" type="text" value="" id="starttime" onfocus="WdatePicker({el:'starttime',dateFmt:'yyyy-MM-dd HH:mm'})" readonly="readonly" style="height:21px;width:120px;"/>&nbsp;至
                       <input name="endtime" type="text" value="" id="endtime" onfocus="WdatePicker({el:'endtime',dateFmt:'yyyy-MM-dd HH:mm'})" readonly="readonly" style="height:21px;width:120px;"/>
                  </td>
                </tr>
                <tr>
                <td class="td_class"></td>
                <td height="25">
                <input type="submit" name="btnsave" value="搜索" id="btnsave" class="adminsubmit_short"/>
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